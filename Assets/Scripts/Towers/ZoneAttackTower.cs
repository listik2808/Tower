using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Towers
{
    [RequireComponent(typeof(SphereCollider))]
    public class ZoneAttackTower : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;
        private List<Monster> _monstersTargets = new List<Monster>();

        public List<Monster> MonstersTargets => _monstersTargets;

        public event Action AttackZone;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Monster monster ))
            {
                _monstersTargets.Add(monster);
                AttackZone?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            if(!_collider) 
                return;

            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawSphere(transform.position + _collider.center, _collider.radius);
        }
    }
}