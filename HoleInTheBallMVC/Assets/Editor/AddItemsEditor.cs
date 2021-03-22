using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Hole
{
    [CustomEditor(typeof(AddItems))]
    public class AddItemsEditor : Editor
    {
        private AddItems _addItems;

        private void OnEnable()
        {
            _addItems = (AddItems)target;
        }

        private void OnSceneGUI2()
        {
            GameObject goMake = null;
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                var ray = Camera.current.ScreenPointToRay(
                    new Vector3(Event.current.mousePosition.x, SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y, 0));
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    goMake = _addItems.Make(hit.point);
                }
            }
            if (goMake != null) SetObjectDirty(goMake);
            //Selection.activeGameObject = _addItems.gameObject;
        }

        private void SetObjectDirty(GameObject obj)
        {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(obj.scene);
        }

    }
}
