using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.General
{
    [CreateAssetMenu(menuName = "Pluggable AI/AI Stats")]
    public class AIStats : ScriptableObject
    {
        public float WalkSpeed;
        public float RunSpeed;
        public float AttackRate;
        public int Damage;
        public int SearchDuration;
        public int SearchingTurnSpeed;
        
    }
}
