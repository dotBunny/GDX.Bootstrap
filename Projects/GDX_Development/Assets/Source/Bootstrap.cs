using System.Collections;
using System.IO;
using GDX;
using GDX.Developer.Reports.BuildVerification;
using UnityEngine;

namespace Dev
{
    public class Bootstrap : MonoBehaviour
    {
        public static readonly string[] ClassicBuildScenes =
        {
            "Assets/GDX.unity",
            "Assets/Tests/000_Config/Config.unity",
            "Assets/Tests/001_SerializableDictionary/SerializableDictionary.unity"
        };

        void Awake()
        {
            Debug.Log("[BOOTSTRAP] Awake!");
        }

#pragma warning disable IDE0051
        // ReSharper disable UnusedMember.Local
        IEnumerator Start()
        {
            int testCount = ClassicBuildScenes.Length;
#if !UNITY_EDITOR
            Debug.Log($"[BOOTSTRAP] Starting test ({testCount.ToString()}) run  ...");
#endif

            // Build out scene definitions
            TestRunner.TestScene[] scenes = new TestRunner.TestScene[testCount-1];
            for (int testSceneIndex = 1; testSceneIndex < testCount; testSceneIndex++)
            {
                scenes[testSceneIndex-1] = new TestRunner.TestScene(testSceneIndex);
            }

            yield return TestRunner.Process(scenes);

#if UNITY_EDITOR
            string outputPath = Path.GetFullPath(Path.Combine(Platform.GetOutputFolder("GDX_Automation"), "BVT.xml"));
#else
            string outputPath = Path.GetFullPath(Path.Combine(Platform.GetOutputFolder(), "BVT.xml"));
#endif
            string result = GDX.Developer.Reports.BuildVerificationReport.OutputReport(outputPath);
            if (File.Exists(outputPath))
            {
                Debug.Log($"[BOOTSTRAP] Build checks ({result}) written to {outputPath}.");
            }
            else
            {
                Debug.LogError($"[BOOTSTRAP] Unable to write file to {outputPath}.");
            }

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
