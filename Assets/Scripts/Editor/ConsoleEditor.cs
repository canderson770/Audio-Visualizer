using UnityEditor;

namespace CustomEditors
{
    public class ConsoleEditor
    {
        private const string control = "%", alt = "&", shift = "#", hotkey = "c";

        [MenuItem("Window/Clear Console " + alt + hotkey, false, 2200)]
        public static void ClearConsole()
        {
            var logEntries = System.Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");
            var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            clearMethod.Invoke(null, null);
        }
    }
}
