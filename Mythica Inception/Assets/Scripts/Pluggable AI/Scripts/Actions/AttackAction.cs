using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Actions
{
    [CreateAssetMenu(menuName = "Pluggable AI/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(StateController stateController)
        {
            Attack(stateController);
        }

        private void Attack(StateController stateController)
        {
            FieldOfView fieldOfView = stateController.GetComponent<FieldOfView>();

            if (!stateController.StateBoolVariable)
            {
                stateController.StateTimeElapsed = stateController.AIStats.AttackRate;
                stateController.StateBoolVariable = true;
            }

            if (fieldOfView.VisibleTargets.Count > 0)
            {
                if (stateController.HasTimeElapsed(stateController.AIStats.AttackRate))
                {
                    //attack here
                    Debug.Log("Attacking " + stateController.Target.name + ".");
                }
            }
        }
    }
}
