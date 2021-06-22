using System.Collections;
using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Decisions
{
    [CreateAssetMenu(menuName = "Pluggable AI/Decisions/Has Done Fleeing")]
    public class HasDoneFleeing : Decision
    {
        private IEnumerator _destination;
        
        public override bool Decide(StateController stateController)
        {
            _destination = TravelOppositeToTarget(stateController);
            return DoneFleeing(stateController);
        }
        private bool DoneFleeing(StateController stateController)
        {
            stateController.StartCoroutine(_destination);
            
            return stateController.HasTimeElapsed(stateController.AIStats.FleeDuration);
        }

        IEnumerator TravelOppositeToTarget(StateController stateController)
        {
            Vector3 currentPosition = stateController.transform.position;
            Vector3 newDestination = currentPosition - stateController.LastKnownTargetPosition;
            newDestination += currentPosition;
            stateController.Agent.destination = newDestination;
            stateController.Destination = newDestination;
            yield return new WaitForSeconds(stateController.AIStats.FleeDuration);
        }
    }
}
