using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    public class GroupEditor
    {
        private const string control = "%", alt = "&", shift = "#", hotkey = "g";
        private const string groupName = "Group";

        [MenuItem("GameObject/Group GameObjects " + control + hotkey, false, 1)]
        public static void GroupSelection()
        {
            //  if no selection
            if (!Selection.activeTransform) return;

            //  create group
            GameObject group = new GameObject(GetName(Selection.activeTransform.parent));
            Undo.RegisterCreatedObjectUndo(group, "Group Selected");
            group.transform.SetParent(Selection.activeTransform.parent);

            //  parent selection under group
            foreach (Transform transform in Selection.transforms)
                Undo.SetTransformParent(transform, group.transform, "Group Selected");

            //  select group
            Selection.activeGameObject = group;
        }

        /// <summary>
        /// Makes sure siblings do not have same name, Returns unique name
        /// </summary>
        private static string GetName(Transform transform)
        {
            //  if no parent, search all root gameobjects
            if (transform == null)
            {
                GameObject[] gameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
                foreach (GameObject go in gameObjects)
                {
                    if (go.name == groupName)
                        return GetNameRecursiveGO(gameObjects, 1);
                }
                return groupName;
            }
            //  else search children of parent
            else
            {
                Transform sibling = transform.Find(groupName);
                if (sibling != null)
                    return GetNameRecursive(transform, 1);
                else
                    return groupName;
            }
        }
        /// <summary>
        /// Searches parent for siblings with same name, Returns unique name
        /// </summary>
        private static string GetNameRecursive(Transform transform, int value)
        {
            Transform sibling = transform.Find(string.Format("{0} ({1})", groupName, value));
            if (sibling != null)
                return GetNameRecursive(transform, value + 1);
            else
                return string.Format("{0} ({1})", groupName, value);
        }
        /// <summary>
        /// Searches root gameobjects for siblings with same name, Returns unique name
        /// </summary>
        private static string GetNameRecursiveGO(GameObject[] gameObjects, int value)
        {
            foreach (GameObject go in gameObjects)
            {
                if (go.name == string.Format("{0} ({1})", groupName, value))
                    return GetNameRecursiveGO(gameObjects, value + 1);
            }
            return string.Format("{0} ({1})", groupName, value);
        }
    }
}
