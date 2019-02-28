using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace CustomEditors
{
    public class ButtonEditor : Editor
    {
        [MenuItem("CONTEXT/Button/Copy Color Block", false, 1100)]
        private static void CopyColorBlock(MenuCommand command)
        {
            Button button = (Button)command.context;
            ColorBlock colors = button.colors;
            EditorGUIUtility.systemCopyBuffer = colors.ToString();
            Debug.Log(colors.ToString());
        }

        [MenuItem("CONTEXT/Button/Paste Color Block", false, 1100)]
        private static void PasteColorBlock(MenuCommand command)
        {
            //Button button = (Button)command.context;
            //ColorBlock colors = (ColorBlock)EditorGUIUtility.systemCopyBuffer;
            //button.colors = colors;
        }

    }
}
