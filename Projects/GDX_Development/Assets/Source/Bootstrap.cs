
using System;
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

#pragma warning disable IDE0051
        // ReSharper disable UnusedMember.Local
        async void Start()
        {
            try
            {
                for (int testSceneIndex = 1; testSceneIndex < ClassicBuildScenes.Length; testSceneIndex++)
                {
                    await TestRunner.EvaluateTestScene(ClassicBuildScenes[testSceneIndex]);
                }
            }
            catch (Exception e)
            {
                GDX.Developer.Reports.BuildVerificationReport.Panic($"EXCEPTION: {e.Message}.\n{e.StackTrace}");
            }
            finally
            {
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
}
