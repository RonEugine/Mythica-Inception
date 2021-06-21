using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Decisions
{
    [CreateAssetMenu(menuName = "Pluggable AI/Decisions/ObjectIsHit")]
    public class IsHitDecision : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return IsAIHit(stateController);
        }

        private bool IsAIHit(StateController stateController)
        {
            bool isHit = stateController.GetComponent<SampleDamageObject>().PlayerDamaged;
            return isHit;
        }
    }
}
