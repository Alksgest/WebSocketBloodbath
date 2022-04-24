using System.Collections;
using Models;
using Unity.Collections;
using UnityEngine;

namespace Controllers
{

    public class BulletController : MonoBehaviour
    {
        [SerializeField] public Bullet bullet;

        [ReadOnly] [SerializeField] private int secondsTillDestroying;
        [SerializeField] private int bulletTotalLife = 10;

        private void Start()
        {
            secondsTillDestroying = bulletTotalLife;
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            for (var i = 0; i < bulletTotalLife; i++)
            {
                secondsTillDestroying -= - i - 1;
                yield return new WaitForSeconds(1);
            }

            Destroy(gameObject);
        }

        public void Init(Bullet b)
        {
            bullet = b;
        }
    }
}