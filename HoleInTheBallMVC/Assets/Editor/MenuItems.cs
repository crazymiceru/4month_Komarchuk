using UnityEditor;
using UnityEngine;

namespace Hole
{
    public sealed class MenuItems 
    {
        [MenuItem("My/EditorItems")]
        private static void MenuOption()
        {
            EditorWindow.GetWindow(typeof(AddItems), false, "Editor Items");
        }
        [MenuItem("My/EditorItems2")]
        private static void MenuOption2()
        {
            EditorWindow.GetWindow(typeof(AddItems2), false, "Editor Items Test");
        }
    }
}
