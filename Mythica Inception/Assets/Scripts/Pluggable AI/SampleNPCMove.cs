using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Pluggable_AI
{
    public class SampleNPCMove : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public Transform Target;
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();

            SetDestination();
        }

        private void SetDestination()
        {
            Vector3 target = Target.position;

            _agent.SetDestination(target);
        }
    }
}
