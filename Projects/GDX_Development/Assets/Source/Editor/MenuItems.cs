using System;
using GDX;
using GDX.Editor.Inspectors;
#if UNITY_2022_2_OR_NEWER
using GDX.Editor.Windows.Tables;
using GDX.DataTables;
#endif
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

