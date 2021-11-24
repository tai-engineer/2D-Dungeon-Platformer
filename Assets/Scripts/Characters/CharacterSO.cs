using UnityEngine;
using System;
namespace DP2D
{
    [CreateAssetMenu(fileName = "New Character SO", menuName = "Character/Character Stats")]
    public class CharacterSO : ScriptableObject
    {

        #region Serialized Fields
        [SerializeField] SharedInt _startHealth, _currentHealth;
        [SerializeField] SharedInt _startEnergy, _currentEnergy;
        #endregion
        #region Events
        public event Action OnHealthChanged;
        #endregion

        #region Private Variables
        
        #endregion

        #region Getters/Setters
        public int Health
        {
            get
            {
                if (_currentHealth.Value <= 0)
                    _currentHealth.Value = 0;
                return _currentHealth.Value;
            }
        }

        public int Energy
        {
            get
            {
                if (_currentEnergy.Value <= 0)
                    _currentEnergy.Value = 0;
                return _currentEnergy.Value;
            }
        }
        #endregion
        public void TakeDamage(int damage)
        {
            _currentHealth.Value -= damage;
            OnHealthChanged?.Invoke();
        }
    }
}
