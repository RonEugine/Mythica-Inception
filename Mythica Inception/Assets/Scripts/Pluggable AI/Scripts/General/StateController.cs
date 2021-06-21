using System.Collections.Generic;
using Assets.Scripts.Pluggable_AI.Scripts.States;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Pluggable_AI.Scripts.General
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FieldOfView))]
    public class StateController : MonoBehaviour
    {
        public AIStats AIStats;
        public State CurrentState;
        public State RemainState;
        public Transform Eyes;
        
        [HideInInspector] public NavMeshAgent Agent;
        [HideInInspector] public List<Transform> Waypoints;
        [HideInInspector] public int NextWaypoint;
        [HideInInspector] public Transform Target;
        [HideInInspector] public Vector3 LastKnownTargetPosition;
        [HideInInspector] public bool StateBoolVariable;
        [HideInInspector] public float StateTimeElapsed;

        private bool _isActive;

        void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if(!_isActive) return;
            
            CurrentState.UpdateState(this);
        }
        
        public void InitializeAI(bool activate, List<Transform> waypointList)
        {
            Waypoints = waypointList;
            _isActive = activate;

            Agent.enabled = _isActive;
        }

        public void TransitionToState(State nextState)
        {
            if (nextState == RemainState) return;
            
            CurrentState = nextState;
            OnExitState();
        }

        public bool HasTimeElapsed(float duration)
        {
            StateTimeElapsed += Time.deltaTime;
            if (StateTimeElapsed >= duration)
            {
                StateTimeElapsed = 0;
                return true;
            }

            return false;
        }

        private void OnExitState()
        {
            StateBoolVariable = false;
            StateTimeElapsed = 0;
        }

        void OnDrawGizmos()
        {
            if (CurrentState == null)
            {
                Debug.LogError("Current State of StateController " + this.name + " is not initialized.");
                return;
            }
            
            Gizmos.color = CurrentState.GizmoColor;
            if (Eyes == null)
            {
                Eyes = transform.Find("Eyes");
            }
            Gizmos.DrawWireSphere(Eyes.position, 1.5f);
        }
        
    }
}
