using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Actions
{
    [CreateAssetMenu(menuName = "Pluggable AI/Actions/Patrol")]
    public class PatrolAction : Actions.Action
    {
        public override void Act(StateController stateController)
        {
            Patrol(stateController);
        }

        private void Patrol(StateController stateController)
        {
            Vector3 nextDestination = stateController.Waypoints[stateController.NextWaypoint].position;
            stateController.Agent.destination = nextDestination;
            stateController.Destination = nextDestination;
            stateController.Agent.isStopped = false; //stateController.Agent.Resume() is obsolete
            if (stateController.Agent.remainingDistance <= stateController.Agent.stoppingDistance &&
                !stateController.Agent.pathPending)
            {
                stateController.NextWaypoint = (stateController.NextWaypoint + 1) % stateController.Waypoints.Count;
            }
        }
    }
}
