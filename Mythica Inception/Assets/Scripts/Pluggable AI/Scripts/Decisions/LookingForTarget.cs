using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Decisions
{
    [CreateAssetMenu(menuName = "Pluggable AI/Decisions/Looking For Target")]
    public class LookingForTarget : Decision
    {
        public override bool Decide(StateController stateController)
        {
            return TargetNotVisible(stateController);
        }

        private bool TargetNotVisible(StateController stateController)
        {
            stateController.transform.Rotate(0, stateController.AIStats.SearchingTurnSpeed * Time.deltaTime, 0);
            return stateController.HasTimeElapsed(stateController.AIStats.SearchDuration);
        }
    }
}
