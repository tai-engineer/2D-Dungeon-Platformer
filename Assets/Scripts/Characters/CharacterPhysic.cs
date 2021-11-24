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
        [Header("Collision")]
        [SerializeField, Tooltip("Raycast length starting from edge of box collider")]
        float _verticalCheckDistance;
        [SerializeField] bool _originalSpriteFacingLeft;

        [Header("Movement")]
        [SerializeField] float _maxGroundSpeed;
        [SerializeField] float _maxJumpSpeed;

        [Header("Shoot")]
        [SerializeField] Transform _shootPosition;

        //[Header("Grenade")]
        //[SerializeField] Grenade _grenadePF;
        //[SerializeField] GameObject _chargeBarRoot;
        //[SerializeField] Image _chargeBar;
        //[SerializeField] float _grenadeCooldown;
        //[SerializeField] float _maxRange = 5f;
        //[Range(30f, 80f)]
        //[SerializeField] float _angle = 45f;
        //[SerializeField] float _maxHoldTime = 2f;

        #endregion
        Vector2 _moveVector;
        public float Gravity { get; private set; }
        public Vector2 MoveVector { get { return _moveVector; } }
        public float MaxJumpSpeed { get { return _maxJumpSpeed; } }
        public bool IsLanded { get; private set; }
        public Vector2 FaceDirection { get; private set; }
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
        void Update()
        {
            // TODO: Use in player's states
            VerticalCollisionCheck(true);
            VerticalCollisionCheck(false);
        }
        public bool VerticalCollisionCheck(bool above)
        {
            Vector2 size = _boxCollider.size;
            Vector2 center = _boxCollider.bounds.center;
            Vector2 direction = above ? Vector2.up : Vector2.down;
            float offset = 0.1f;
            Vector2 middle = center + direction * (size.y * 0.5f + offset);

            Vector2[] raycast = new Vector2[3];
            raycast[0] = middle + Vector2.left * size.x * 0.5f;
            raycast[1] = middle;
            raycast[2] = middle + Vector2.right * size.x * 0.5f;
            RaycastHit2D[] hits = new RaycastHit2D[3];
            Vector2[] normals = new Vector2[3];
            Vector2 result = Vector2.zero;
            for(int i = 0; i < raycast.Length; i++)
            {
                hits[i] = Physics2D.Raycast(raycast[i], direction, _verticalCheckDistance);
                normals[i] = hits[i].collider ? hits[i].normal : Vector2.zero;
                result += normals[i];
            }
            return Mathf.Approximately(result.y, 3f);
        }
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
        public void HorizontalMove(float input)
        {
            SetHorizontalMovement(input * _maxGroundSpeed);
            UpdateSpriteFacing();
        }
        public void VerticalMove()
        {
            AddVerticalMovement(Gravity * Time.deltaTime);
        }
        public void LandingPrepare()
        {
            IsLanded = false;
        }
        public void LandingEnd()
        {
            IsLanded = true;
        }
        public void Shoot()
        {
            //BulletPool pool = PoolManager.GetPool<BulletPool>();

            //pool.Pop(_shootPosition.position, false);
        }
        #region Grenade
        //float _throwHoldTime;
        //bool _isHolding = false;
        //float fraction;
        //float _grenadeTime;
        //public void ThrowGrenade(bool throwInput, int animationHash, Animator animator)
        //{
        //    if ((Time.time - _grenadeTime) < _grenadeCooldown)
        //        return;

        //    if (throwInput == false && _isHolding == true)
        //    {
        //        float range = Mathf.Lerp(0.1f, _maxRange, fraction);
        //        Grenade grenade = Instantiate(_grenadePF, transform.position, Quaternion.identity);
        //        grenade.Launch(range, _angle, FaceDirection);

        //        _throwHoldTime = 0f;
        //        _isHolding = false;
        //        _chargeBar.fillAmount = 0f;
        //        _chargeBarRoot.SetActive(false);
        //        animator.SetTrigger(animationHash);
        //        _grenadeTime = Time.time;
        //        return;
        //    }
        //    if (throwInput == true)
        //    {
        //        if (_isHolding)
        //        {
        //            _throwHoldTime += Time.deltaTime;
        //            if (_throwHoldTime > _maxHoldTime)
        //            {
        //                _throwHoldTime = _maxHoldTime;
        //            }
        //            fraction = _throwHoldTime / _maxHoldTime;
        //            _chargeBar.fillAmount = fraction;
        //        }
        //        else
        //        {
        //            _isHolding = true;
        //            _chargeBarRoot.SetActive(true);
        //        }
        //    }
        //}
        #endregion
        void UpdateSpriteFacing()
        {
            FaceDirection = _moveVector.x < 0 ? Vector2.left : _moveVector.x > 0 ? Vector2.right : FaceDirection;
            _spriteRenderer.flipX = _originalSpriteFacingLeft ^ (FaceDirection.x < 0);
        }
    }
}
