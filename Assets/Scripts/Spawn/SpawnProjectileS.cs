using Scripts.Projectiles;
using Scripts.Pull;
using Scripts.Towers;
using System;
using UnityEngine;

namespace Scripts.Spawn
{
    public class SpawnProjectileS : MonoBehaviour
    {
        [Range(1,20)]
        [SerializeField] private int _numberObjectsCreated;
        [SerializeField] private Projectile _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Tower _tower;
        private CustomPoolprojectile _customPool;

        private void OnEnable()
        {
            _tower.ActivatedProjectile += GetProjectile;
            _tower.LoadWeapon += PrepareProjectile;
        }

        private void OnDisable()
        {
            _tower.ActivatedProjectile -= GetProjectile;
            _tower.LoadWeapon -= PrepareProjectile;
        }

        private void Start()
        {
            _customPool = new CustomPoolprojectile(_prefab, _numberObjectsCreated, _container);
        }

        public void PrepareProjectile()
        {
            _tower.SetBullet(_customPool.Charge());
        }

        public void GetProjectile()
        {
            Projectile projectile = _customPool.Get();
            projectile.Move();
        }
    }
}