﻿using UnityEngine;

namespace Kevin_Iglesias.Caster_Mage_Animations.Scripts {

    public class ThrowHealing : StateMachineBehaviour {

        CastSpells cS;

        public CastHand castHand;
        
        public float spawnDelay;
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            
            if(cS == null)
            {
                cS = animator.GetComponent<CastSpells>();
            }
            
            if(cS != null)
            {
               cS.ThrowHealing(castHand, spawnDelay);
            }
        }
    }
}
