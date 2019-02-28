using UnityEngine.UI;
using UnityEditor;

namespace CustomEditors
{
    public class SelectableEditor : Editor
    {
        public static ColorBlock colors;
        public static bool canPaste = false;

        [MenuItem("CONTEXT/Selectable/Copy Color Block", false, 1100)]
        private static void CopyColorBlock(MenuCommand command)
        {
            Selectable selectable = (Selectable)command.context;
            canPaste = true;
            colors = selectable.colors;
        }

        [MenuItem("CONTEXT/Selectable/Paste Color Block", false, 1110)]
        private static void PasteColorBlock(MenuCommand command)
        {
            Selectable selectable = (Selectable)command.context;
            Undo.RecordObject(selectable, "Paste Color Block");
            selectable.colors = colors;
        }

        [MenuItem("CONTEXT/Selectable/Paste Color Block", true, 1110)]
        private static bool ValidatePasteColorBlock()
        {
            return canPaste;
        }
    }
}
