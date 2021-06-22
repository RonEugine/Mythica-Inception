using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Decisions
{
    [CreateAssetMenu(menuName = "Pluggable AI/Decisions/Took Damage")]
    public class IsHit : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return IsAIHit(stateController);
        }

        private bool IsAIHit(StateController stateController)
        {
            bool isHit = stateController.GetComponent<Health>().TookHit;
            return isHit;
        }
    }
}
