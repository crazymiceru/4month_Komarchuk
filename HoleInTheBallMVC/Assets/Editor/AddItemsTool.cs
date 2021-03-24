using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Hole
{
    [EditorTool("Editor Items")]
    public class AddItemsTool : EditorTool
    {
        public Texture2D ico;
        public override GUIContent toolbarIcon
        {
            get
            {
                return new GUIContent
                {
                    image = ico,
                    text = "Editor Items",
                    tooltip = "Выбирает случайный предмет и устанавливает по клику"
                };
            }
        }

        public override void OnToolGUI(EditorWindow window)
        {
            GameObject goMake = null;
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                var ray = Camera.current.ScreenPointToRay(
                    new Vector3(Event.current.mousePosition.x, SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y, 0));
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log($"Попали в {hit.transform.gameObject.name}");
                    goMake = AddItems.inst.Make(hit.point);                    
                    Undo.RegisterCreatedObjectUndo(goMake, "Add Object !!!!!!!!!!!!!!");
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
