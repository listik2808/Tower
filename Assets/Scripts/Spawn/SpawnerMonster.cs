using Scripts.Pull;
using System.Collections;
using UnityEngine;

namespace Scripts.Spawn
{
    public class SpawnerMonster : MonoBehaviour
    {
        [SerializeField] private float _interval = 3;
        [Range(1,100)]
        [SerializeField] private int _numberObjectsCreated;
        [SerializeField] private Monster _prefab;
        [SerializeField] private GameObject _moveTarget;
        [SerializeField] private Transform _container;
        [SerializeField] private CustomPoolMonster _customPool;
        private bool _isDead = false;

        private void Start()
        {
            _customPool.Constructor(_prefab, _numberObjectsCreated, _container);
            StartCoroutine(ActivateObject());
        }

        private IEnumerator ActivateObject()
        {
            while(_isDead == false)
            {
                Monster newMonster = _customPool.Get();
                if(newMonster == null)
                {
                    _isDead = true;
                }
                else
                {
                    newMonster.transform.position = transform.position;
                    newMonster.SetTarget(_moveTarget);
                    yield return new WaitForSeconds(_interval);
                }
            }
        }
    }
}