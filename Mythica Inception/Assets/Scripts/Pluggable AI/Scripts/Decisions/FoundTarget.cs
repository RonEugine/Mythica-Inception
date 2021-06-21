using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Decisions
{
    [CreateAssetMenu(menuName = "Pluggable AI/Decisions/FoundTargets")]
    public class FoundTarget : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return Look(stateController);
        }

        private bool Look(StateController stateController)
        {
            FieldOfView fieldOfView = stateController.GetComponent<FieldOfView>();
            if (fieldOfView == null) return false;

            if (fieldOfView.VisibleTargets.Count > 0)
            {
                stateController.Target = fieldOfView.VisibleTargets[0];
                return true;
            }
            
            return false;
        }
    }
}
