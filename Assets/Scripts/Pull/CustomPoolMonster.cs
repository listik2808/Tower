using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;

namespace Scripts.Pull
{
    public class CustomPoolMonster : MonoBehaviour
    {
        private Monster _prefab;
        private List<Monster> _poll =new List<Monster>();
        private Transform _containerPoll;
        private List<Monster> _reservPoll = new List<Monster>();
        private int _count = 0;
        private Monster _monster;

        public List<Monster> Poll => _poll;


        public void Constructor(Monster monsterPrefab, int prewarmObjects, Transform container)
        {
            _prefab = monsterPrefab;
            _containerPoll = container;

            for (int i = 0; i < prewarmObjects; i++)
            {
                Monster monster = Create(_prefab, _containerPoll);
                monster.Construct();
                monster.gameObject.SetActive(false);
                _poll.Add(monster);
            }
        }

        public Monster Get()
        {
            _monster = null;
            foreach (var item in _poll)
            {
                if(item.gameObject.activeInHierarchy == false && item.CurrentHealth > 0)
                {
                    _monster = item;
                    _monster.transform.position = Vector3.zero;
                    _monster.gameObject.SetActive(true);
                    break;
                }
            }
            
            return _monster;
        }

        private Monster Create(Monster monsterPrefab,Transform container)
        {
            Monster objMonster = GameObject.Instantiate(monsterPrefab, container);
            return objMonster;
        }
    }
}