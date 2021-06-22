using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        [HideInInspector] public bool TookHit;
        public void TakeDamage()
        {
            Debug.Log(this.name + " is hit.");
            StartCoroutine("DamageDelay", .01f);
        }

        IEnumerator DamageDelay(float duration)
        {
            TookHit = true;
            yield return new WaitForSeconds(duration);
            TookHit = false;
        }

    }
    [CustomEditor(typeof(Health))]
    public class DamageEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            Health myTarget = (Health)target;
            DrawDefaultInspector();
            if (GUILayout.Button("Damage Monster", GUILayout.Height(40)))
            {
                if(Application.isPlaying)
                    myTarget.TakeDamage();
                else
                {
                    Debug.LogWarning("DamageObject script Warning: The project is currently not in GameMode");
                }
            }
        }
    }
}
