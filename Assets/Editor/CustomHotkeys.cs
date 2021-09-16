using UnityEngine;
using UnityEditor;

namespace Editor
{
    public static class CustomHotkeys
    {
        [MenuItem("Tools/Toggle Inspector Lock %l")]
        private static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}
