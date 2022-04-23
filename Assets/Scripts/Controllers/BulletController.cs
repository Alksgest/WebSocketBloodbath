using System;
using UnityEngine;

namespace Controllers
{
    public class BulletController: MonoBehaviour
    {
        private void Start()
        {
            Destroy(this, 10f);
        }
    }
}