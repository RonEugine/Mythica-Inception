using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.General
{
    [System.Serializable]
    public class WaypointGroupManager : MonoBehaviour
    {
        [HideInInspector]
        public List<Transform> Waypoints;
        [Tooltip("If unchecked, this will find all GameObjects with StateController Component in the Scene")]
        public bool GiveSpecificStateControllers;
        [ConditionalField(nameof(GiveSpecificStateControllers))] public CollectionWrapper<StateController> StateControllers;

        private StateController[] _stateControllers;
        

        void Start()
        {
            
            if (!GiveSpecificStateControllers)
            {
                _stateControllers = FindObjectsOfType<StateController>();
            }
            else
            {
                _stateControllers = StateControllers.Value;
            }
            
            foreach (Transform child in transform)
                Waypoints.Add(child);
            
            if (Waypoints.Count == 0) Debug.LogError("You have to put Waypoint children in " + this.name + " GameObject.");
            
            if (_stateControllers.Length == 0) Debug.LogError("No StateController found in " + this.name + " GameObject.");
            
            if (_stateControllers.Length == 0 || Waypoints.Count == 0) return;
            
            foreach (var stateController in _stateControllers)
            {
                stateController.InitializeAI(true, Waypoints);
            }
        }
    }
}
