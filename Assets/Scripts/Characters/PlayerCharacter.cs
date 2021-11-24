using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Singleton;
namespace DP2D
{
    public class PlayerCharacter : Singleton<PlayerCharacter>
    {
        [Header("Input")]
        [SerializeField] InputReaderSO _input;

        [Header("Stats")]
        [SerializeField] SharedInt _currentHealth;
        [SerializeField] SharedInt _startingHealth;
        [SerializeField] SharedInt _currentEnergy;
        [SerializeField] SharedInt _startingEnergy;

        [Header("Animation")]
        [SerializeField] string _sprintParameter;
        [SerializeField] string _moveParameter;
        [SerializeField] string _throwParameter;
        [SerializeField] string _jumpParameter;
        [SerializeField] string _landParameter;
        [SerializeField] string _fallParameter;
        [SerializeField] string _shootParameter;

        #region Input
        public Vector3 MoveInput { get; private set; }
        public bool SprintInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool ShootInput { get; private set; }
        public bool ThrowInput { get; private set; }
        #endregion

        #region Stats
        public int Gold { get; set; }
        public int Health
        {
            get
            {
                return _currentHealth.Value;
            }
            set
            {
                _currentHealth.Value = value;
            }
        }
        public int Energy
        {
            get
            {
                return _currentEnergy.Value;
            }
            set
            {
                _currentEnergy.Value = value;
            }
        }
        #endregion

        #region Animation Hash
        public int SprintHash { get; private set;}
        public int MoveHash { get; private set; }
        public int ThrowHash { get; private set; }
        public int JumpHash { get; private set; }
        public int LandHash { get; private set; }
        public int FallHash { get; private set; }
        public int ShootHash { get; private set; }
        #endregion
        #region Unity Executions
        protected override void Awake()
        {
            base.Awake();
            GetAnimationHash();
        }
        void OnEnable()
        {
            if(_input)
            {
                _input.shootEvent  += OnShoot;
                _input.crouchEvent += OnCrouch;
                _input.jumpEvent   += OnJump;
                _input.sprintEvent += OnSprint;
                _input.moveEvent   += OnMove;
                _input.throwEvent   += OnThrow;
            }

            Health = _startingHealth.Value;
            Energy = _startingEnergy.Value;
        }
        void OnDisable()
        {
            if (_input)
            {
                _input.shootEvent  -= OnShoot;
                _input.crouchEvent -= OnCrouch;
                _input.jumpEvent   -= OnJump;
                _input.sprintEvent -= OnSprint;
                _input.moveEvent   -= OnMove;
                _input.throwEvent -= OnThrow;
            }
        }
        #endregion

        #region Stats Method
        public void IncreaseGold(int amount)
        {
            Gold += amount;
        }
        public void IncreaseHealth(int amount)
        {
            Health += amount;
        }
        public void DecreaseHealth(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;
        }
        public void IncreaseEnergy(int amount)
        {
            Energy += amount;
        }
        public void DecreaseEnergy(int amount)
        {
            Energy -= amount;
            if (Energy < 0)
                Energy = 0;
        }
        #endregion
        #region Damage Methods
        public void TakeDamage(Damager damager, Damageable damageable)
        {
            DecreaseHealth(damager.damage);
        }
        #endregion
        #region Input Methods
        void OnShoot(bool shootInput)
        {
            ShootInput = shootInput;
        }
        void OnCrouch()
        {
            Debug.Log("Crouching");
        }
        void OnJump(bool jumpInput)
        {
            JumpInput = jumpInput;
        }
        void OnSprint(bool sprintInput)
        {
            SprintInput = sprintInput;
        }
        void OnMove(Vector2 moveInput)
        {
            MoveInput = moveInput;
        }
        void OnThrow(bool throwInput)
        {
            ThrowInput = throwInput;
        }
        #endregion
        #region Animation Methods
        void GetAnimationHash()
        {
            SprintHash = Animator.StringToHash(_sprintParameter);
            MoveHash = Animator.StringToHash(_moveParameter);
            ThrowHash = Animator.StringToHash(_throwParameter);
            JumpHash = Animator.StringToHash(_jumpParameter);
            LandHash = Animator.StringToHash(_landParameter);
            FallHash = Animator.StringToHash(_fallParameter);
            ShootHash = Animator.StringToHash(_shootParameter);
        }
        #endregion
    }
}
