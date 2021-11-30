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
            raycast[0] = middle + Vector2.left * size.x * 0.4f;
            raycast[1] = middle;
            raycast[2] = middle + Vector2.right * size.x * 0.4f;

            RaycastHit2D[] hits = new RaycastHit2D[3];
            int hitCount = 0;
            for(int i = 0; i < raycast.Length; i++)
            {
                hits[i] = Physics2D.Raycast(raycast[i], direction, raycastDistance, _verticalCheckLayer);
                Debug.DrawRay(raycast[i], direction * raycastDistance);
                if (hits[i].collider != null)
                    hitCount++;
            }
            if (hitCount < 3)
                return false;

            float hitPoint = above ? edge.y + _verticalCheckDistance : edge.y - _verticalCheckDistance;
            return above ? hits[1].point.y < hitPoint : hits[1].point.y > hitPoint;
        }
        public bool HorizontalCollisionCheck()
        {
            bool left = FaceDirection.x < 0;
            Vector2 size = _boxCollider.size;
            Vector2 direction = left ? Vector2.left : Vector2.right;
            Vector2 center = (Vector2)_boxCollider.bounds.center;
            Vector2 middle = center + direction * (size.x * 0.5f);
            float raycastDistance = _horizontalCheckDistance;

            Vector2[] raycast = new Vector2[3];
            raycast[0] = middle + Vector2.up * size.y * 0.4f;
            raycast[1] = middle;
            raycast[2] = middle + Vector2.down * size.y * 0.4f;

            RaycastHit2D[] hits = new RaycastHit2D[3];
            int hitCount = 0;
            for (int i = 0; i < raycast.Length; i++)
            {
                hits[i] = Physics2D.Raycast(raycast[i], direction, raycastDistance, _horizontalCheckLayer);
                Debug.DrawRay(raycast[i], direction * raycastDistance);
                if (hits[i].collider != null)
                    hitCount++;
            }
            if (hitCount < 3)
                return false;

            float hitPoint = left ? middle.x - _horizontalCheckDistance : middle.x + _horizontalCheckLayer;
            return left ? hits[1].point.x > hitPoint : hits[1].point.x < hitPoint;
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
        #endregion
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
        void UpdateSpriteFacing()
        {
            FaceDirection = _moveVector.x < 0 ? Vector2.left : _moveVector.x > 0 ? Vector2.right : FaceDirection;
            _spriteRenderer.flipX = _originalSpriteFacingLeft ^ (FaceDirection.x < 0);
        }
        public void SpriteFlip(bool flip) => _spriteRenderer.flipX = flip;
    }
}
