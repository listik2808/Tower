﻿using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Scripts.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonProjectile : Projectile
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Monster monster))
            {
                monster.Hit(_damage);
                gameObject.SetActive(false);
            }
            else if (other.TryGetComponent(out Ground ground))
            {
                gameObject.SetActive(false);
            }
            
        } 

        public override void Move()
        {
            transform.forward = _rigidbody.velocity;
        }
    }
}