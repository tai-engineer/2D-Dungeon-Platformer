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
        [SerializeField] SharedIntSO _currentHealth;
        [SerializeField] SharedIntSO _startingHealth;
        [SerializeField] SharedIntSO _currentEnergy;
        [SerializeField] SharedIntSO _startingEnergy;

        [Header("Animation")]
        [SerializeField] string _sprintParameter = "";
        [SerializeField] string _moveParameter = "";
        [SerializeField] string _throwParameter = "";
        [SerializeField] string _jumpParameter = "";
        [SerializeField] string _landParameter = "";
        [SerializeField] string _fallParameter = "";
        [SerializeField] string _shootParameter = "";
        [SerializeField] string _slideParameter = "";
        [SerializeField] string _wallSlideParameter = "";
        [SerializeField] string _wallHangParameter = "";
        [SerializeField] string _wallClimpParameter = "";
        [SerializeField] string _rollParameter = "";
        [SerializeField] string _attack1Parameter = "";
        [SerializeField] string _attack2Parameter = "";

        #region Animation Hash
        public int SprintHash { get; private set;}
        public int MoveHash { get; private set; }
        public int ThrowHash { get; private set; }
        public int JumpHash { get; private set; }
        public int LandHash { get; private set; }
        public int FallHash { get; private set; }
        public int ShootHash { get; private set; }
        public int SlideHash { get; private set; }
        public int WallSlideHash { get; private set; }
        public int WallHangHash { get; private set; }
        public int WallClimbHash { get; private set; }
        public int RollHash { get; private set; }
        public int Attack1Hash { get; private set; }
        public int Attack2Hash { get; private set; }
        #endregion
        #region Input
        public Vector3 MoveInput { get; private set; }
        public bool SprintInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool ShootInput { get; private set; }
        public bool ThrowInput { get; private set; }
        public bool SlideInput { get; private set; }
        public bool ClimbInput { get; private set; }
        public bool RollInput { get; private set; }

        /// <summary>
        /// Attack input will be reset when being read.
        /// This prevents character keeps attacking when user does not want to release the button.
        /// </summary>
        bool _attackInput = false;
        public bool AttackInput
        {
            get
            {
                if (_attackInput)
                {
                    _attackInput = false;
                    return true;
                }
                return false;
            }

            private set => _attackInput = value;
        }
        public bool AttackInputEnable { get; private set; } = true;
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
                _input.slideEvent   += OnSlide;
                _input.climbEvent   += OnClimb;
                _input.rollEvent   += OnRoll;
                _input.attackEvent   += OnAttack;
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
                _input.slideEvent -= OnSlide;
                _input.climbEvent -= OnClimb;
                _input.rollEvent -= OnRoll;
                _input.attackEvent -= OnAttack;
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
        void OnSlide(bool slideInput)
        {
            SlideInput = slideInput;
        }
        void OnClimb(bool climbInput)
        {
            ClimbInput = climbInput;
        }
        void OnRoll(bool rollInput)
        {
            RollInput = rollInput;
        }
        
        void OnAttack(bool attackInput)
        {
            AttackInput = AttackInputEnable ? attackInput : false;
        }
        #endregion
        #region Animation Methods
        void GetAnimationHash()
        {
            SprintHash = GetHash(_sprintParameter);
            MoveHash = GetHash(_moveParameter);
            ThrowHash = GetHash(_throwParameter);
            JumpHash = GetHash(_jumpParameter);
            LandHash = GetHash(_landParameter);
            FallHash = GetHash(_fallParameter);
            ShootHash = GetHash(_shootParameter);
            SlideHash = GetHash(_slideParameter);
            WallSlideHash = GetHash(_wallSlideParameter);
            WallHangHash = GetHash(_wallHangParameter);
            WallClimbHash = GetHash(_wallClimpParameter);
            RollHash = GetHash(_rollParameter);
            Attack1Hash = GetHash(_attack1Parameter);
            Attack2Hash = GetHash(_attack2Parameter);
        }

        int GetHash(string str)
        {
            return (str != "") ? Animator.StringToHash(str) : 0;
        }
        #endregion

        #region Combat
        public void EnableInputAttack() => AttackInputEnable = true;
        public void DisableInputAttack() => AttackInputEnable = false;
        #endregion
    }
}
