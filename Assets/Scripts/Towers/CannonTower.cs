using UnityEngine;
using System.Collections;
using Scripts.Projectiles;
using Scripts.Spawn;
using static UnityEngine.GraphicsBuffer;

namespace Scripts.Towers
{
    public class CannonTower : Tower
    {
        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private GameObject _gun;

        private void OnEnable()
        {
            _zoneAttackTower.AttackZone += SetTartget;
        }

        private void OnDisable()
        {
            _zoneAttackTower.AttackZone -= SetTartget;
        }


        void Update()
        {
            if (Target != null)
            {
                ElepsedTime += Time.deltaTime;
                RotationGun();
                AngelGunForward();
                if (CheckingDistance(Target.transform))
                {
                    Vector3 dirFromAtoB = ((Target.transform.position + ResultVector) - transform.position).normalized;
                    float dotProd = Vector3.Dot(dirFromAtoB, transform.forward);
                    if (dotProd > 0.9)
                    {
                        Vector3 aim = Target.transform.position - transform.position;
                        aim.y = 0;
                        transform.forward = aim;
                        if (ElepsedTime > _shootInterval && aim.magnitude < Range)
                        {
                            Shot();
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void AngelGunForward()
        {
            Quaternion rawRoation = Quaternion.Slerp(_gun.transform.rotation,
            Quaternion.LookRotation(Target.transform.position - _gun.transform.position), SpeedAngel * Time.deltaTime);
            _gun.transform.rotation = new Quaternion(rawRoation.x, rawRoation.y, 0, rawRoation.w);
        }
    }
}