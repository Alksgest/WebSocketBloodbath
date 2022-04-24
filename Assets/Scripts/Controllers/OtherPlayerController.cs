using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class OtherPlayerController : PlayerControllerBase
    {
        [SerializeField] private Slider hpSlider;
        
        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);

            hpSlider.value -= 0.01f;
        }
    }
}