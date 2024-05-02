using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [Range(1, 100)]
        [SerializeField] protected float _damage = 10;
        protected Vector3 target;
        public float Speed => _speed;

        public abstract void Move();
        public void Go(Vector3 speed, Vector3 targetPos)
        {
            _rigidbody.velocity = speed;
            target = targetPos;
        }

    }
}
