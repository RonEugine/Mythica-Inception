using Assets.Scripts.Pluggable_AI.Scripts.Decisions;
using Assets.Scripts.Pluggable_AI.Scripts.States;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.General
{
    [System.Serializable]
    public class Transition
    {
        public Decision Decision;
        public State SuccessState;
        public State FailState;
    }
}
