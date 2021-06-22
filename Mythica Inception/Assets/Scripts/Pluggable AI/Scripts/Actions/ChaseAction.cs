using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Actions
{
    [CreateAssetMenu(menuName = "Pluggable AI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController stateController)
        {
            Chase(stateController);
        }

        private void Chase(StateController stateController)
        {
            FieldOfView fieldOfView = stateController.GetComponent<FieldOfView>();

            if (fieldOfView.VisibleTargets.Count > 0)
            {
                stateController.Agent.destination = stateController.Target.position;
                stateController.Destination = stateController.Target.position;
                stateController.LastKnownTargetPosition = stateController.Target.position;
                stateController.Agent.isStopped = false;
            }
            else
            {
                stateController.Agent.destination = stateController.LastKnownTargetPosition;
                stateController.Destination = stateController.LastKnownTargetPosition;
            }
        }
    }
}
