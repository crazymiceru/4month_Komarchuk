using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Hole
{
    public class AddItems : EditorWindow
    {
        public List<GameObject> _prefabItems=new List<GameObject>();
        public Transform _folderMaze;
        public int _folderMazeID; 
        public static AddItems inst;

        private void OnValidate()
        {
//            Debug.Log($"folderMazeSeed:{_folderMazeID}");
            if (_folderMazeID>0)
            {
                var m=GameObject.FindObjectsOfType<Transform>().Where(x=>x.gameObject.GetInstanceID()== _folderMazeID);

                foreach (var item in m)
                {
                    _folderMaze = item;
                }
            }

            inst = this;
        }

        private void OnGUI()
        {
            float win = Screen.width;
            float w1 = 25;
            float w5 = win - w1 - 5;
            float w2 = 0.3f * win;
            float w3 = 0.25f * win;
            float w4 = 0.45f * win;

            GUILayout.Label("Установка случайных ITEMS");
            var _folderMazeTmp = (Transform)EditorGUILayout.ObjectField("Folder:", _folderMaze, typeof(Transform), true);
            if (GUI.changed)
            {                
                _folderMaze = _folderMazeTmp;
                if (_folderMaze != null) _folderMazeID = _folderMaze.gameObject.GetInstanceID();
                else _folderMazeID = 0;
            }

            for (int i = 0; i < _prefabItems.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("X", GUILayout.Width(w1)))
                {
                    _prefabItems.RemoveAt(i);
                    break;
                }
                _prefabItems[i] = (GameObject)EditorGUILayout.ObjectField(_prefabItems[i], typeof(GameObject), true, GUILayout.Width(w5));
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add"))
            {
                _prefabItems.Add(null);
            }
        }
        
        public GameObject Make(Vector3 pos)
        {
            GameObject go = null;
       
            if (_prefabItems.Count > 0)
            {
                go = (GameObject)PrefabUtility.InstantiatePrefab(_prefabItems[Random.Range(0, _prefabItems.Count)], _folderMaze);
                go.transform.position = pos + new Vector3(0, go.transform.position.y, 0);
            }
            return go;
        }

        private void SetObjectDirty(GameObject obj)
        {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(obj.scene);
        }

    }
}
