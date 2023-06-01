using System.Diagnostics;
using System.IO;
using GDX;
using GDX.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Debug = UnityEngine.Debug;
#if UNITY_2022_2_OR_NEWER
using GDX.Editor.Windows.DataTables;
using GDX.DataTables;
#endif

namespace Dev.Editor
{
    public static class MenuItems
    {
#if GDX_TOOLS
        [MenuItem("Tools/GDX/INTERNAL/Capture Focused Window (PNG)", false)]
#endif
        public static void TestOutput()
        {
            Automation.CaptureFocusedEditorWindowToPNG(Path.Combine(Platform.GetOutputFolder("GDX_Automation"),
                "test.png"));
        }

#if GDX_TOOLS
        [MenuItem("Tools/GDX/INTERNAL/BVT/Editor", false)]
#endif
        public static void RunBuildVerificationTestInEditor()
        {
            if (Application.isPlaying)
            {
                EditorApplication.ExitPlaymode();
            }

            EditorSceneManager.OpenScene("Assets/GDX.unity", OpenSceneMode.Single);
            EditorApplication.EnterPlaymode();
        }

#if GDX_TOOLS
        [MenuItem("Tools/GDX/INTERNAL/BVT/Win64 - Mono", false)]
#endif
        public static void RunBuildVerificationTestInWin64Mono()
        {
            BuildFactory.BuildClassicWin64Mono();
            EvaluateBuild(Path.Combine(BuildFactory.BuildLocation, BuildFactory.WindowsExecutable));
        }


#if GDX_TOOLS
        [MenuItem("Tools/GDX/INTERNAL/BVT/Win64 - IL2CPP", false)]
#endif
        public static void RunBuildVerificationTestInWin64IL2CPP()
        {
            BuildFactory.BuildClassicWin64IL2CPP();
            EvaluateBuild(Path.Combine(BuildFactory.BuildLocation, BuildFactory.WindowsExecutable));
        }

        static void EvaluateBuild(string buildPath)
        {
            if (File.Exists(buildPath))
            {
                // Get tmp folder
                string outputFolder = Path.Combine(Path.GetTempPath(), "gdx-dev-bvt");
                string outputFile = Path.Combine(outputFolder, "BVT.xml");
                Platform.EnsureFileFolderHierarchyExists(outputFolder);
                Platform.ForceDeleteFile(outputFile);

                Process buildProcess = Process.Start(buildPath, $"--GDX_OUTPUT_FOLDER=\"{outputFolder}\"");
                buildProcess?.WaitForExit(10000);

                if (File.Exists(outputFile))
                {
                    string content = File.ReadAllText(outputFile);
                    if (content.Contains("Failed"))
                    {
                        Debug.LogError($"BVT had FAILURES\nResults:\n{content}");
                    }
                    else
                    {
                        Debug.Log($"BVT was successful.\nResults:\n{content}");
                    }
                }
                else
                {
                    Debug.LogError("BVT did not finish.");
                }
            }
        }

#if UNITY_2022_2_OR_NEWER
#if GDX_TOOLS
        [MenuItem("Tools/GDX/INTERNAL/Create StableTable Example", false)]
#endif
        public static void CreateStableTableExample()
        {
            StableDataTable asset = ScriptableObject.CreateInstance<StableDataTable>();
            AssetDatabase.CreateAsset(asset, "Assets/StableTableExample.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;

            asset.AddColumn(Serializable.SerializableTypes.String, "String");
            asset.AddRow("0");
            asset.AddColumn(Serializable.SerializableTypes.Char, "Char");
            asset.AddRow("1");
            asset.AddColumn(Serializable.SerializableTypes.Bool, "Bool");
            asset.AddRow("2");
            asset.AddColumn(Serializable.SerializableTypes.SByte, "SByte");
            asset.AddRow("3");
            asset.AddColumn(Serializable.SerializableTypes.Byte, "Byte");
            asset.AddRow("4");
            asset.AddColumn(Serializable.SerializableTypes.Short, "Short");
            asset.AddRow("5");
            asset.AddColumn(Serializable.SerializableTypes.UShort, "UShort");
            asset.AddRow("6");
            asset.AddColumn(Serializable.SerializableTypes.Int, "Int");
            asset.AddRow("7");
            asset.AddColumn(Serializable.SerializableTypes.UInt, "UInt");
            asset.AddRow("8");
            asset.AddColumn(Serializable.SerializableTypes.Long, "Long");
            asset.AddRow("9");
            asset.AddColumn(Serializable.SerializableTypes.ULong, "ULong");
            asset.AddRow("10");
            asset.AddColumn(Serializable.SerializableTypes.Float, "Float");
            asset.AddRow("11");
            asset.AddColumn(Serializable.SerializableTypes.Double, "Double");
            asset.AddRow("12");
            asset.AddColumn(Serializable.SerializableTypes.Vector2, "Vector2");
            asset.AddRow("13");
            asset.AddColumn(Serializable.SerializableTypes.Vector3, "Vector3");
            asset.AddRow("14");
            asset.AddColumn(Serializable.SerializableTypes.Vector4, "Vector4");
            asset.AddRow("15");
            asset.AddColumn(Serializable.SerializableTypes.Vector2Int, "Vector2Int");
            asset.AddRow("16");
            asset.AddColumn(Serializable.SerializableTypes.Vector3Int, "Vector3Int");
            asset.AddRow("17");
            asset.AddColumn(Serializable.SerializableTypes.Quaternion, "Quaternion");
            asset.AddRow("18");
            asset.AddColumn(Serializable.SerializableTypes.Rect, "Rect");
            asset.AddRow("19");
            asset.AddColumn(Serializable.SerializableTypes.RectInt, "RectInt");
            asset.AddRow("20");
            asset.AddColumn(Serializable.SerializableTypes.Color, "Color");
            asset.AddRow("21");
            asset.AddColumn(Serializable.SerializableTypes.LayerMask, "LayerMask");
            asset.AddRow("22");
            asset.AddColumn(Serializable.SerializableTypes.Bounds, "Bounds");
            asset.AddRow("23");
            asset.AddColumn(Serializable.SerializableTypes.BoundsInt, "BoundsInt");
            asset.AddRow("24");
            asset.AddColumn(Serializable.SerializableTypes.Hash128, "Hash128");
            asset.AddRow("25");
            asset.AddColumn(Serializable.SerializableTypes.Gradient, "Gradient");
            asset.AddRow("26");
            asset.AddColumn(Serializable.SerializableTypes.AnimationCurve, "AnimationCurve");
            asset.AddRow("27");
            asset.AddColumn(Serializable.SerializableTypes.Object, "Object");
            asset.AddRow("28");

            TableWindowProvider.OpenAsset(asset);
        }
#endif
    }
}