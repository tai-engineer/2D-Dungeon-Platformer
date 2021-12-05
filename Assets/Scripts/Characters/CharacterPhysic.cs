using UnityEngine;
using UnityEngine.UI;
using System;
namespace DP2D
{
    // Handle player physics
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterPhysic : MonoBehaviour
    {
        #region Components
        BoxCollider2D _boxCollider;
        Rigidbody2D _rb2D;
        SpriteRenderer _spriteRenderer;
        #endregion
        #region Serializable Fields
        [SerializeField] bool _originalSpriteFacingLeft;
        [Header("Collision")]
        [SerializeField, Tooltip("Raycast length starting from edge of box collider")]
        float _verticalCheckDistance;

        [SerializeField] float _horizontalCheckDistance;
        [SerializeField] LayerMask _verticalCheckLayer;
        [SerializeField] LayerMask _horizontalCheckLayer;

        [Header("Movement")]
        [SerializeField] float _maxGroundSpeed;
        [SerializeField] float _groundAcceleration;
        [SerializeField] float _maxJumpSpeed;
        [SerializeField] float _maxFallSpeed;
        [SerializeField] float _slideDuration;
        [SerializeField] float _slideCooldown;
        #endregion
        Vector2 _moveVector;
        public float Gravity { get; private set; }
        public Vector2 MoveVector { get { return _moveVector; } }
        public float MaxJumpSpeed { get { return _maxJumpSpeed; } }
        public float SlideDuration { get { return _slideDuration; } }
        public bool IsLanding { get; private set; }
        public bool CanSlide 
        {
            get => (Time.time - _slideEndTime) > _slideCooldown;
        }
        public bool IsSliding { get; set; }
        public Vector2 FaceDirection { get; private set; }
        public bool WallCollided { get; set; }
        public bool CanWallhang { get; set; }
        public bool IsClimbing { get; set; }
        public bool CanWallSlide { get; set; }
        public bool CurrentSpriteFlip { get => _spriteRenderer.flipX; }
        public Vector2 CurrentPosition { get => transform.position; }
        public BoxCollider2D Box2D { get => _boxCollider; }
        void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rb2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            Gravity = Physics2D.gravity.y;
            FaceDirection = _originalSpriteFacingLeft ? Vector2.left : Vector2.right;
        }
        void FixedUpdate()
        {
            Vector2 movement = _moveVector * Time.deltaTime;
            _rb2D.MovePosition(_rb2D.position + movement);
        }
        #region Collision Check
        public bool VerticalCollisionCheck(bool above)
        {
            Vector2 size = _boxCollider.size;
            Vector2 direction = above ? Vector2.up : Vector2.down;
            Vector2 center = (Vector2)_boxCollider.bounds.center;
            Vector2 middle = center + direction * (size.y * 0.4f);
            Vector2 edge = center + direction * (size.y * 0.5f);
            float raycastDistance = 0.5f + _verticalCheckDistance;

            Vector2[] raycast = new Vector2[3];
            raycast[0] = middle + Vector2.left * size.x * 0.3f;
            raycast[1] = middle;
            raycast[2] = middle + Vector2.right * size.x * 0.3f;

            RaycastHit2D[] hits = new RaycastHit2D[3];
            int hitCount = 0;
            for(int i = 0; i < raycast.Length; i++)
            {
                hits[i] = Physics2D.Raycast(raycast[i], direction, raycastDistance, _verticalCheckLayer);
                Debug.DrawRay(raycast[i], direction * raycastDistance);
                if (hits[i].collider != null)
                    hitCount++;
            }
            if (hitCount < 1)
                return false;

            float hitPoint = above ? edge.y + _verticalCheckDistance : edge.y - _verticalCheckDistance;
            return above ? hits[1].point.y < hitPoint : hits[1].point.y > hitPoint;
        }
        public bool HorizontalCollisionCheck()
        {
            Vector2 direction = FaceDirection.x < 0 ? Vector2.left : Vector2.right;
            Vector2 middle = (Vector2)_boxCollider.bounds.center + direction * (_boxCollider.size.x * 0.5f);

            WallCollided = WallCollisionCheck(middle, direction, _horizontalCheckDistance, _verticalCheckLayer);
            CanWallSlide = WallSlideCheck(middle, direction, _horizontalCheckDistance, _verticalCheckLayer);
            CanWallhang = WallLedgeCheck(middle, direction, _horizontalCheckDistance, _verticalCheckLayer);
            return WallCollided;
        }
        bool WallCollisionCheck(Vector2 middle, Vector2 direction, float distance, LayerMask layer)
        {
            Vector2[] wallCast = new Vector2[3];
            wallCast[0] = middle + Vector2.up * _boxCollider.size.y * 0.4f;
            wallCast[1] = middle;
            wallCast[2] = middle + Vector2.down * _boxCollider.size.y * 0.4f;
            RaycastHit2D[] hits = new RaycastHit2D[wallCast.Length];
            for (int i = 0; i < wallCast.Length; i++)
            {
                hits[i] = Physics2D.Raycast(wallCast[i], direction, distance, _horizontalCheckLayer);
                Debug.DrawRay(wallCast[i], direction * distance, Color.green);
                if (hits[i].collider != null)
                    return true;
            }
            return false;
        }
        bool WallSlideCheck(Vector2 middle, Vector2 direction, float distance, LayerMask layer)
        {
            Vector2[] wallSlideCast = new Vector2[3];
            wallSlideCast[0] = middle + Vector2.up * _boxCollider.size.y * 0.4f;
            wallSlideCast[1] = middle;
            wallSlideCast[2] = middle + Vector2.down * _boxCollider.size.y * 0.4f;
            RaycastHit2D[] hits = new RaycastHit2D[wallSlideCast.Length];
            for (int i = 0; i < wallSlideCast.Length; i++)
            {
                hits[i] = Physics2D.Raycast(wallSlideCast[i], direction, distance, _horizontalCheckLayer);
                if (hits[i].collider == null)
                    return false;
            }
            return true;
        }
        bool WallLedgeCheck(Vector2 middle, Vector2 direction, float distance, LayerMask layer)
        {
            if (!WallCollided) return false;

            Vector2[] wallLedgeCast = new Vector2[2];
            wallLedgeCast[0] = middle + Vector2.up * _boxCollider.size.y * 0.4f;
            wallLedgeCast[1] = middle + Vector2.up * _boxCollider.size.y * 0.2f;
            RaycastHit2D[] hits = new RaycastHit2D[wallLedgeCast.Length];
            for (int i = 0; i < wallLedgeCast.Length; i++)
            {
                hits[i] = Physics2D.Raycast(wallLedgeCast[i], direction, distance, _horizontalCheckLayer);
                Debug.DrawRay(wallLedgeCast[i], direction * distance, Color.red);
            }

            if (hits[1].collider != null)
                return hits[0].collider == null;
            return false;
        }
        #endregion
        #region Movement
        public void SetHorizontalMovement(float value)
        {
            _moveVector.x = value;
        }
        public void SetVerticalMovement(float value)
        {
            _moveVector.y = value;
        }
        public void AddVerticalMovement(float value)
        {
            _moveVector.y += value;
        }
        public void SetMoveVector(Vector2 value)
        {
            _moveVector = value;
        }
        public void ResetMoveVector()
        {
            SetMoveVector(Vector2.zero);
        }
        public void HorizontalMove(float moveInput)
        {
            float moveAmount = Mathf.MoveTowards(_moveVector.x, moveInput * _maxGroundSpeed, Time.deltaTime * _groundAcceleration);
            SetHorizontalMovement(moveAmount);
            UpdateSpriteFacing();
            if(WallCollided)
            {
                SetHorizontalMovement(0f);
            }
        }
        public void VerticalMove()
        {
            if (_moveVector.y <= -_maxFallSpeed)
            {
                SetVerticalMovement(-_maxFallSpeed);
                return;
            }
            AddVerticalMovement(Gravity * Time.deltaTime);
        }
        public void LandingPrepare()
        {
            IsLanding = true;
        }
        /// <summary>
        /// This function is called by Land animation event
        /// </summary>
        public void LandingEnd()
        {
            IsLanding = false;
        }
        float _slideEndTime = 0f;
        public void SlideEnd()
        {
            _slideEndTime = Time.time;
        }
        public void ClimbEnd()
        {
            IsClimbing = false;
        }

        Vector2 _climbCheckOffset = new Vector2(0.1f, 0.2f);
        Vector2 _climbStandOffset = new Vector2(0f, 0.02f);
        public Vector2 GetClimbStandPosition()
        {
            float distance = 0.5f;
            Vector2 center = (Vector2)_boxCollider.bounds.center;
            Vector2 middleTop = center + Vector2.up * (_boxCollider.size.y * 0.5f);

            Vector2 climbCheckPosition = middleTop + new Vector2(_climbCheckOffset.x * FaceDirection.x, _climbCheckOffset.y);

            RaycastHit2D hit = Physics2D.Raycast(climbCheckPosition, Vector2.down, distance, _verticalCheckLayer);
            Debug.DrawRay(climbCheckPosition, Vector2.down * distance, Color.green);
            if (hit.collider == null)
            {
                Debug.LogWarning("Failed to get climb position");
                return Vector2.zero;
            }

            return hit.point + _climbStandOffset;

        }
        #endregion
        public void UpdateSpriteFacing()
        {
            FaceDirection = _moveVector.x < 0 ? Vector2.left : _moveVector.x > 0 ? Vector2.right : FaceDirection;
            _spriteRenderer.flipX = _originalSpriteFacingLeft ^ (FaceDirection.x < 0);
        }
        public void SpriteFlip(bool flip) => _spriteRenderer.flipX = flip;
    }
}
