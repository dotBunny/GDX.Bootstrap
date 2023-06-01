using System;
using System.Diagnostics;
using System.IO;
using GDX;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Debug = UnityEngine.Debug;

#pragma warning disable IDE1006
// ReSharper disable InconsistentNaming

namespace BVT.Editor
{
    public static class BuildFactory
    {
        public const string WindowsExecutable = "GDX.exe";
        public const string k_MacOSExecutable = "GDX.app";

        public static readonly string BuildLocation = Path.Combine(Application.dataPath, "..", "Builds", "TestBuild");

        [MenuItem("GDX_Development/BVT/Editor", false)]
        public static void RunBuildVerificationTestInEditor()
        {
            if (Application.isPlaying) EditorApplication.ExitPlaymode();

            EditorSceneManager.OpenScene("Assets/BVT/Bootstrap.unity", OpenSceneMode.Single);
            EditorApplication.EnterPlaymode();
        }
        
        [MenuItem("GDX_Development/BVT/Classic - Win64 - Mono", false)]
        public static void RunBuildVerificationTestInWin64Mono()
        {
            BuildClassicWin64Mono();
            EvaluateBuild(Path.Combine(BuildLocation, WindowsExecutable));
        }

        [MenuItem("GDX_Development/BVT/Classic - Win64 - IL2CPP", false)]
        public static void RunBuildVerificationTestInWin64IL2CPP()
        {
            BuildClassicWin64IL2CPP();
            EvaluateBuild(Path.Combine(BuildLocation, WindowsExecutable));
        }

        [MenuItem("GDX_Development/Build/Classic - Win64 - Mono", false)]
        public static void BuildClassicWin64Mono()
        {
            ClassicBuildPlayer(
                WindowsExecutable,
                BuildTarget.StandaloneWindows64,
                BuildTargetGroup.Standalone,
                ScriptingImplementation.Mono2x);
        }
        
        [MenuItem("GDX_Development/Build/Classic - Win64 - IL2CPP", false)]
        public static void BuildClassicWin64IL2CPP()
        {
            ClassicBuildPlayer(
                WindowsExecutable,
                BuildTarget.StandaloneWindows64,
                BuildTargetGroup.Standalone,
                ScriptingImplementation.IL2CPP);
        }

        [MenuItem("GDX_Development/Build/Classic - OSX - Mono", false)]
        public static void BuildClassicOSXMono()
        {
            ClassicBuildPlayer(
                k_MacOSExecutable,
                BuildTarget.StandaloneOSX,
                BuildTargetGroup.Standalone,
                ScriptingImplementation.Mono2x);
        }
        
        [MenuItem("GDX_Development/Build/Classic - OSX - IL2CPP", false)]
        // ReSharper disable once IdentifierTypo
        public static void BuildClassicOSXIL2CPP()
        {
            ClassicBuildPlayer(
                k_MacOSExecutable,
                BuildTarget.StandaloneOSX,
                BuildTargetGroup.Standalone,
                ScriptingImplementation.IL2CPP);
        }

        private static void ClassicBuildPlayer(string executableName, BuildTarget buildTarget,
            BuildTargetGroup buildTargetGroup,
            ScriptingImplementation scriptingImplementation)
        {
            var buildFailed = false;
            var previousScriptingImplementation =
                PlayerSettings.GetScriptingBackend(buildTargetGroup);
            var shouldRestoreScriptingImplementation = false;

            if (previousScriptingImplementation != scriptingImplementation)
            {
                PlayerSettings.SetScriptingBackend(buildTargetGroup, scriptingImplementation);
                shouldRestoreScriptingImplementation = true;
            }

            try
            {
                // Remove previous build entirely please, we do not want any sort of stale data
                if (Directory.Exists(BuildLocation)) Directory.Delete(BuildLocation, true);

                // Execute classic pipeline
                BuildPipeline.BuildPlayer(Bootstrap.ClassicBuildScenes, Path.Combine(BuildLocation, executableName),
                    buildTarget, BuildOptions.None);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                buildFailed = true;
            }
            finally
            {
                if (shouldRestoreScriptingImplementation)
                    PlayerSettings.SetScriptingBackend(buildTargetGroup, previousScriptingImplementation);

                if (Application.isBatchMode && buildFailed) EditorApplication.Exit(1);
            }
        }
        
        static void EvaluateBuild(string buildPath)
        {
            if (File.Exists(buildPath))
            {
                // Get tmp folder
                var outputFolder = Path.Combine(Path.GetTempPath(), "gdx-bvt");
                var outputFile = Path.Combine(outputFolder, "BVT.xml");
                
                Platform.EnsureFileFolderHierarchyExists(outputFolder);
                Platform.ForceDeleteFile(outputFile);

                var buildProcess = Process.Start(buildPath, $"--GDX_OUTPUT_FOLDER=\"{outputFolder}\"");
                buildProcess?.WaitForExit(10000);

                if (File.Exists(outputFile))
                {
                    var content = File.ReadAllText(outputFile);
                    if (content.Contains("Failed"))
                        Debug.LogError($"BVT had FAILURES\n{content}");
                    else
                        Debug.Log($"BVT was successful.\n{content}");
                }
                else
                {
                    Debug.LogError("BVT did not finish.");
                }

                // Remove output content
                Directory.Delete(outputFolder, true);
            }
            else
            {
                Debug.LogError("BVT did not find a build.");
            }
        }
    }
}