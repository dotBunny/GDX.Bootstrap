using System;
using System.IO;
using GDX;
using GDX.Developer.Reports.BuildVerification;
using UnityEngine;

namespace Dev
{
    public class Bootstrap : MonoBehaviour
    {
        int m_frameWait = 50;

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
        async void Update()
        {
            // Handle frame delay for things to settle
            if (m_frameWait > 0)
            {
                m_frameWait--;
                return;
            }
            if (m_frameWait < 0) return;

            m_frameWait--;

            GDX.Developer.Reports.BuildVerificationReport.Reset();

            int testCount = ClassicBuildScenes.Length;
#if !UNITY_EDITOR
            Debug.Log($"[BOOTSTRAP] Starting test ({testCount.ToString()}) run  ...");
#endif
            try
            {
                // Build out scene definitions
                TestScene[] scenes = new TestScene[testCount-1];
                for (int testSceneIndex = 1; testSceneIndex < testCount; testSceneIndex++)
                {
                    scenes[testSceneIndex-1] = new TestScene(testSceneIndex);
                }

                await TestRunner.Execute(scenes);
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
