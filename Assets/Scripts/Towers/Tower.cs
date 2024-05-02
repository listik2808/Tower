using Scripts.Projectiles;
using System;
using UnityEngine;

namespace Scripts.Towers
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField] private float _range = 32f;
        [SerializeField] protected ZoneAttackTower _zoneAttackTower;
        [SerializeField] protected float SpeedAngel;
        protected float Distans;
        protected Monster Target;
        protected float ElepsedTime = 0;
        protected Vector3 ResultVector;
        private Projectile _projectile;

        public event Action LoadWeapon;
        public event Action ActivatedProjectile;

        public Projectile ProjectileBollet => _projectile;
        public float Range => _range;

        public void LoadWeaponProjectile( )
        {
            LoadWeapon?.Invoke();
        }

        public void EventActionProjectile()
        {
            ActivatedProjectile?.Invoke();
        }

        public void SetBullet(Projectile projectile)
        {
            _projectile = projectile;
        }

        protected bool CheckingDistance(Transform targetPosition)
        {
            if (Target == null)
            {
                return false;
            }

            Distans = Vector3.Distance(transform.position, targetPosition.position);
            if (Distans > _range)
            {
                return false;
            }
            else
            {
                LoadWeaponProjectile();
                return true;
            }
        }

        protected void RotationGun( )
        {
            transform.rotation =
            Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(Target.transform.position - transform.position), SpeedAngel * Time.deltaTime);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0,transform.rotation.w);
        }

        protected void NewTarget()
        {
            Target.IsDead -= NewTarget;
            Target = null;
            SetTartget();
        }

        protected void SetTartget()
        {
            if (Target == null)
            {
                foreach (var item in _zoneAttackTower.MonstersTargets)
                {
                    if(item.gameObject.activeInHierarchy == true && item.CurrentHealth > 0)
                    {
                        Target = item;
                        break;
                    }
                }
                if(Target == null)
                {
                    return;
                }
                Target.IsDead += NewTarget;
            }
        }

        protected Vector3 GetHitPoint(Vector3 targetPosition, Vector3 targetSpeed, Vector3 attackerPosition, float bulletSpeed, out float time)
        {
            Vector3 q = targetPosition - attackerPosition;
            q.y = 0;
            targetSpeed.y = 0;

            float a = Vector3.Dot(targetSpeed, targetSpeed) - (bulletSpeed * bulletSpeed);
            float b = 2 * Vector3.Dot(targetSpeed, q);
            float c = Vector3.Dot(q, q);

            float D = Mathf.Sqrt((b * b) - 4 * a * c);

            float t1 = (-b + D) / (2 * a);
            float t2 = (-b - D) / (2 * a);

            time = Mathf.Max(t1, t2);

            Vector3 ret = targetPosition + targetSpeed * time;
            return ret;
        }

        protected void Shot()
        {
            ElepsedTime = 0;

            float time = 0;
            Vector3 hitPoint = GetHitPoint(Target.EnemyAimPos, Target.lastSpeed, transform.position, ProjectileBollet.Speed, out time);
            Vector3 aim = hitPoint - transform.position;
            aim.y = 0;
            float antiGravity = -Physics.gravity.y * time / 2;
            float deltaY = (hitPoint.y - ProjectileBollet.transform.position.y) / time;
            Vector3 arrowSpeed = aim.normalized * ProjectileBollet.Speed;
            arrowSpeed.y = antiGravity + deltaY;
            ProjectileBollet.Go(arrowSpeed, hitPoint);
            EventActionProjectile();
        }
    }
}