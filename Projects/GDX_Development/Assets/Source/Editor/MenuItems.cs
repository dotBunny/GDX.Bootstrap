using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Dev.Editor
{
    public static class MenuItems
    {
#if GDX_TOOLS
        [MenuItem("Tools/GDX/INTERNAL/Capture Focused Window (PNG)", false)]
#endif
        public static void TestOutput()
        {
            GDX.Editor.Automation.CaptureFocusedEditorWindowToPNG(System.IO.Path.Combine(GDX.Platform.GetOutputFolder("GDX_Automation"), "test.png"));
        }

#if GDX_TOOLS
        [MenuItem("Tools/GDX/INTERNAL/Run BVT In Editor", false)]
#endif
        public static void RunBuildVerificationTest()
        {
            if (Application.isPlaying)
            {
                EditorApplication.ExitPlaymode();
            }
            EditorSceneManager.OpenScene("Assets/GDX.unity", OpenSceneMode.Single);
            EditorApplication.EnterPlaymode();
        }
    }
}

