using System;
using System.IO;
using GDX;
using GDX.Developer.Reports.BuildVerification;
using GDX.Experimental;
using GDX.Experimental.Logging;
using UnityEngine;

namespace BVT
{
    public class Bootstrap : MonoBehaviour
    {
        public const int LogCategory = 42;
        float m_runtimeSettleTimer = 5f;
        bool m_executing;

        public static readonly string[] ClassicBuildScenes =
        {
            "Assets/BVT/Bootstrap.unity",
            "Assets/BVT/000_Config/Config.unity",
            "Assets/BVT/001_SerializableDictionary/SerializableDictionary.unity",
            "Assets/BVT/002_StableDataTable/StableDataTable.unity"
        };

        void Awake()
        {
            ManagedLog.RegisterCategory(LogCategory, "Bootstrap");
            ManagedLog.Info(LogCategory, $"Tests will begin in ~{m_runtimeSettleTimer.ToString()} seconds.");
        }

#pragma warning disable IDE0051
        // ReSharper disable UnusedMember.Local
        void Update()
        {
            // Handle frame delay for things to settle
            if (m_runtimeSettleTimer > 0)
            {
                m_runtimeSettleTimer -= Time.deltaTime;
                return;
            }

            if (!m_executing)
            {
                ManagedLog.Info(LogCategory, "Start Async Execution ...");
                m_executing = true;
                Execute();
            }
        }

        async void Execute()
        {
            GDX.Developer.Reports.BuildVerificationReport.Reset();

            int testCount = ClassicBuildScenes.Length;
#if !UNITY_EDITOR
            ManagedLog.Info(LogCategory, $"Starting test ({testCount.ToString()}) run ...");
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
                    ManagedLog.Info(LogCategory, $"Build checks ({result}) written to {outputPath}.");
                }
                else
                {
                    ManagedLog.Info(LogCategory, $"Unable to write file to {outputPath}.");
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
