using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class SampleDamageObject : MonoBehaviour
    {
        [HideInInspector] public bool PlayerDamaged;
        public void PlayerDamage()
        {
            Debug.Log(this.name + " is hit.");
            StartCoroutine("DamageDelay", .01f);
        }

        IEnumerator DamageDelay(float duration)
        {
            PlayerDamaged = true;
            yield return new WaitForSeconds(duration);
            PlayerDamaged = false;
        }

    }
    [CustomEditor(typeof(SampleDamageObject))]
    public class DamageEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            SampleDamageObject myTarget = (SampleDamageObject)target;
            DrawDefaultInspector();
            if (GUILayout.Button("Damage Monster", GUILayout.Height(40)))
            {
                if(Application.isPlaying)
                    myTarget.PlayerDamage();
                else
                {
                    Debug.LogWarning("DamageObject script Warning: The project is currently not in GameMode");
                }
            }
        }
    }
}
