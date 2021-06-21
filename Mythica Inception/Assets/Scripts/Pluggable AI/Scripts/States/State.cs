using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.States
{
    [CreateAssetMenu(menuName = "Pluggable AI/State")]
    public class State : ScriptableObject
    {
        public Actions.Action[] Actions;
        public Transition[] Transitions;
        public Color GizmoColor = Color.blue;

        public void UpdateState(StateController stateController)
        {
            ExecuteAction(stateController);
            CheckForTransitions(stateController);
        }
        
        private void ExecuteAction(StateController stateController)
        {
            foreach (var action in Actions)
            {
                action.Act(stateController);
            }
        }

        private void CheckForTransitions(StateController stateController)
        {
            foreach (var transition in Transitions)
            {
                bool decision = transition.Decision.Decide(stateController);
                
                if (decision)
                {
                    stateController.TransitionToState(transition.SuccessState);
                }
                else
                {
                    stateController.TransitionToState(transition.FailState);
                }
            }
        }
    }
}
