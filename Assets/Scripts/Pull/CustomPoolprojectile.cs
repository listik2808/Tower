using Scripts.Projectiles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Pull
{
    public class CustomPoolprojectile : MonoBehaviour
    {
        private Projectile _prefab;
        private List<Projectile> _poll;
        private Transform _containerPoll;

        public CustomPoolprojectile(Projectile projectilePrefab, int prewarmObjects, Transform container)
        {
            _prefab = projectilePrefab;
            _poll = new List<Projectile>();
            _containerPoll = container;

            for (int i = 0; i < prewarmObjects; i++)
            {
                Projectile projectile = Create(_prefab, _containerPoll);
                projectile.gameObject.SetActive(false);
                _poll.Add(projectile);
            }
        }

        public Projectile Get()
        {
            Projectile projectile = _poll.FirstOrDefault(x => !x.isActiveAndEnabled);
            if(projectile == null)
            {
                projectile = Create(_prefab, _containerPoll);
                projectile.gameObject.SetActive(false);
                _poll.Add(projectile);
            }
            projectile.transform.localPosition = Vector3.zero;
            projectile.gameObject.SetActive(true);
            return projectile;
        }

        public Projectile Charge()
        {
            Projectile projectile = _poll.FirstOrDefault(x => !x.isActiveAndEnabled);
            if (projectile == null)
            {
                projectile = Create(_prefab, _containerPoll);
                projectile.gameObject.SetActive(false);
                _poll.Add(projectile);
            }
            projectile.transform.localPosition = Vector3.zero;
            return projectile;
        }

        private Projectile Create(Projectile projectilePrefab, Transform container)
        {
            Projectile objProjectile = GameObject.Instantiate(projectilePrefab, container);
            _poll.Add(objProjectile);
            return objProjectile;
        }
    }
}