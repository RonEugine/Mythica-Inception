using Assets.Scripts.Pluggable_AI.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.Actions
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(StateController stateController);
    }
}
