using UnityEngine;
using UnityEditor;

namespace CustomEditors
{
    public class RectTransformEditor
    {
        [MenuItem("CONTEXT/RectTransform/Reset Scale of Children", false, 2000)]
        public static void ResetScaleChildren()
        {
            if (Selection.activeGameObject == null) return;
            RectTransform[] transforms = Selection.activeGameObject.GetComponentsInChildren<RectTransform>();

            Undo.RecordObjects(transforms, "Reset Scale of Children");
            foreach (RectTransform rect in transforms)
            {
                if (rect == Selection.activeGameObject.transform) continue;
                rect.localScale = Vector3.one;
            }
        }

        [MenuItem("CONTEXT/RectTransform/Reset Pos Z of Children", false, 2010)]
        public static void ResetValues()
        {
            if (Selection.activeGameObject == null) return;
            RectTransform[] transforms = Selection.activeGameObject.GetComponentsInChildren<RectTransform>();

            Undo.RecordObjects(transforms, "Reset Pos Z of Children");
            foreach (RectTransform rect in transforms)
            {
                if (rect == Selection.activeGameObject.transform) continue;
                rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0);
            }
        }

        [MenuItem("CONTEXT/RectTransform/Horizontal Layout of Children", false, 2100)]
        public static void HorizontalLayout()
        {
            if (Selection.activeGameObject == null) return;

            //  get all direct children transforms
            RectTransform[] transforms = System.Array.FindAll(Selection.activeGameObject.GetComponentsInChildren<RectTransform>(false),
                child => child.parent == Selection.activeGameObject.transform);
            float increment = 1f / transforms.Length;
            float min = 0, max = 0;

            Undo.RecordObjects(transforms, "Horizontal Layout of Children");
            foreach (RectTransform rect in transforms)
            {
                if (rect.gameObject == Selection.activeGameObject) continue;

                max += increment;

                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;

                rect.anchorMin = new Vector2(min, 0);
                rect.anchorMax = new Vector2(max, 1);
                rect.pivot = new Vector2(0.5f, 0.5f);

                min += increment;
            }
        }

        [MenuItem("CONTEXT/RectTransform/Vertical Layout of Children", false, 2110)]
        public static void VerticalLayout()
        {
            if (Selection.activeGameObject == null) return;

            //  get all direct children transforms
            RectTransform[] transforms = System.Array.FindAll(Selection.activeGameObject.GetComponentsInChildren<RectTransform>(false),
                child => child.parent == Selection.activeGameObject.transform);
            System.Array.Reverse(transforms);
            float increment = 1f / transforms.Length;
            float min = 0, max = 0;

            Undo.RecordObjects(transforms, "Vertical Layout of Children");
            foreach (RectTransform rect in transforms)
            {
                if (rect.gameObject == Selection.activeGameObject) continue;

                max += increment;

                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;

                rect.anchorMin = new Vector2(0, min);
                rect.anchorMax = new Vector2(1, max);
                rect.pivot = new Vector2(0.5f, 0.5f);

                min += increment;
            }
        }
    }
}
