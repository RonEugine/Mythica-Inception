using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Pluggable_AI.Scripts.General
{
    public class FieldOfView : MonoBehaviour
    {
        public float ViewRadius;
        [Range(0,360)]
        public float ViewAngle;

        public float ReactionTime = .2f;

        public LayerMask targetLayer;
        public LayerMask obstaclesLayer;
        
        [HideInInspector]
        public List<Transform> VisibleTargets = new List<Transform>();

        void Start()
        {
            StartCoroutine("FindTargetsWithDelay", ReactionTime);
        }
        

        IEnumerator FindTargetsWithDelay(float reactionTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(reactionTime);
                FindVisibleTargets();
            }
        }
        
        private void FindVisibleTargets()
        {
            VisibleTargets.Clear();
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, targetLayer);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < ViewAngle/2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstaclesLayer))
                    {
                        VisibleTargets.Add(target);
                    }
                }
            }
        }

        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
            
        }
    }
}
