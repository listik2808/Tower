using UnityEngine;

namespace Scripts
{
    public class MonsterTregget : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Monster monster))
            {
                monster.gameObject.SetActive(false);
            }
        }
    }
}