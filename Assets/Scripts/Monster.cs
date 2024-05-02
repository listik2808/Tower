using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Monster : MonoBehaviour
    {
        public const float ReachDistance = 0.3f;
        [SerializeField] private GameObject _moveTarget;
        [SerializeField] private float _speed = 0.1f;
        [SerializeField] private float _maxHealth = 30;
        [SerializeField] private GameObject _targetPoint;
        public Vector3 lastSpeed = new Vector3();
        private float _currentHealth;

        public float CurrentHealth => _currentHealth;

        public event Action IsDead;

        public void Construct()
        {
            _currentHealth = _maxHealth;
        }

        private void Update()
        {
            if (_moveTarget == null)
                return;

            Vector3 aim = _moveTarget.transform.position - transform.position;
            lastSpeed = aim.normalized * _speed;
            transform.position += lastSpeed * Time.deltaTime;
            transform.forward = lastSpeed;
        }

        public void SetTarget(GameObject target)
        {
            _moveTarget = target;
        }

        public void Hit(float damage)
        {
            if(damage > _currentHealth)
            {
                _currentHealth = 0;
                IsDead?.Invoke();
                enabled = false;
                gameObject.SetActive(false);
            }
            else
            {
                _currentHealth -= damage;
            }

            if(_currentHealth == 0)
            {
                IsDead?.Invoke();
                enabled = false;
                gameObject.SetActive(false);
            }
            
        }

        public Vector3 EnemyAimPos
        {
            get { return transform.position; }
        }
    }
}