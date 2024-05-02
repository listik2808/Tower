using UnityEngine;
using Scripts.Projectiles;

namespace Scripts.Towers
{
    public class SimpleTower : Tower
    {
        [SerializeField] private float _shootInterval = 0.5f;

        private void OnEnable()
        {
            _zoneAttackTower.AttackZone += SetTartget;
        }

        private void OnDisable()
        {
            _zoneAttackTower.AttackZone -= SetTartget;
        }

        private void Update()
        {
            if(Target != null)
            {
                ElepsedTime += Time.deltaTime;
                RotationGun();
                if (CheckingDistance(Target.transform))
                {
                    Vector3 aim = Target.transform.position - transform.position;
                    aim.y = 0;
                    //transform.forward = aim;
                    if (ElepsedTime > _shootInterval && aim.magnitude < Range)
                    {
                        Shot();
                    }
                }
            }
        }
    }
}