using UnityEditor;
using UnityEngine;

namespace Hole
{
    public class AddItems2 : EditorWindow
    {
        public Transform _test;
        public string _sTest;


        private void OnGUI()
        {
            _test = (Transform)EditorGUILayout.ObjectField("Folder:", _test, typeof(Transform), true);
            _sTest = EditorGUILayout.TextField("Text:", _sTest);
        }
    }
}
