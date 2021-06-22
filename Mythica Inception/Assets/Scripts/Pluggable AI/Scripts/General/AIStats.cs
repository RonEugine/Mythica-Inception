using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.General
{
    [CreateAssetMenu(menuName = "Pluggable AI/AI Stats")]
    public class AIStats : ScriptableObject
    {
        public float AttackDecisionEvery;
        public int SearchDuration;
        public int SearchingTurnSpeed;
        public float FleeDuration;
    }
}
