
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Dev.Editor
{
    public static class BuildFactory
    {
        const string k_WindowsExecutable = "GDX.exe";
        const string k_MacOSExecutable = "GDX.app";

        static readonly string k_BuildLocation = Path.Combine(Application.dataPath, "..", "Builds", "TestBuild");

        [MenuItem("Tools/GDX/INTERNAL/Build/Classic - Win64 - Mono", false)]
        public static void BuildClassicWin64Mono()
        {
            ClassicBuildPlayer(
                k_WindowsExecutable,
                BuildTarget.StandaloneWindows64,
                BuildTargetGroup.Standalone,
                ScriptingImplementation.Mono2x);
        }

        [MenuItem("Tools/GDX/INTERNAL/Build/Classic - Win64 - IL2CPP", false)]
        public static void BuildClassicWin64IL2CPP()
        {
            ClassicBuildPlayer(
                k_WindowsExecutable,
                BuildTarget.StandaloneWindows64,
                BuildTargetGroup.Standalone,
                ScriptingImplementation.IL2CPP);
        }

        [MenuItem("Tools/GDX/INTERNAL/Build/Classic - OSX - Mono", false)]
        public static void BuildClassicOSXMono()
        {
            ClassicBuildPlayer(
                k_MacOSExecutable,
                BuildTarget.StandaloneOSX,
                BuildTargetGroup.Standalone,
                ScriptingImplementation.Mono2x);
        }

        [MenuItem("Tools/GDX/INTERNAL/Build/Classic - OSX - IL2CPP", false)]
        public static void BuildClassicOSXIL2CPP()
        {
            ClassicBuildPlayer(
                k_MacOSExecutable,
                BuildTarget.StandaloneOSX,
                BuildTargetGroup.Standalone,
                ScriptingImplementation.IL2CPP);
        }



        static void ClassicBuildPlayer(string executableName, BuildTarget buildTarget, BuildTargetGroup buildTargetGroup,
            ScriptingImplementation scriptingImplementation)
        {
            bool buildFailed = false;
            ScriptingImplementation previousScriptingImplementation =
                PlayerSettings.GetScriptingBackend(buildTargetGroup);
            bool shouldRestoreScriptingImplementation = false;

            if (previousScriptingImplementation != scriptingImplementation)
            {
                PlayerSettings.SetScriptingBackend(buildTargetGroup, scriptingImplementation);
                shouldRestoreScriptingImplementation = true;
            }

            try
            {
                // Remove previous build entirely please, we do not want any sort of stale data
                if(Directory.Exists(k_BuildLocation))
                {
                    Directory.Delete(k_BuildLocation, true);
                }

                // Execute classic pipeline
                BuildPipeline.BuildPlayer(Bootstrap.ClassicBuildScenes, Path.Combine(k_BuildLocation, executableName),
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
                {
                    PlayerSettings.SetScriptingBackend(buildTargetGroup, previousScriptingImplementation);
                }

                if (Application.isBatchMode && buildFailed)
                {
                    EditorApplication.Exit(1);
                }
            }
        }


    }
}

