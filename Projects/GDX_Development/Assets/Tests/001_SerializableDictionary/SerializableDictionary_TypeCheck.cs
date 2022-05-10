#if !UNITY_DOTSRUNTIME

using GDX.Collections.Generic;
using GDX.Developer.Reports.NUnit;
using UnityEngine;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class SerializableDictionary_TypeCheck : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public struct TestStruct
        {
            public bool Hello;
            public bool Jello;
        }

        public GameObject CameraObject;
        public GameObject ColliderObject;
        public GameObject ContentObject;
        public GameObject CubeObject;
        public TestStruct StructOne;
        public TestStruct StructTwo;

        public SerializableDictionary<TestStruct, int> TestStructToInt = new SerializableDictionary<TestStruct, int>();
        public SerializableDictionary<int, TestStruct> IntToTestStruct = new SerializableDictionary<int, TestStruct>();

        public SerializableDictionary<int, GameObject> IntegerToGameObject =
            new SerializableDictionary<int, GameObject>();

        public SerializableDictionary<GameObject, int> GameObjectToInteger =
            new SerializableDictionary<GameObject, int>();

        public SerializableDictionary<string, GameObject> StringToGameObject =
            new SerializableDictionary<string, GameObject>();

        public SerializableDictionary<GameObject, string> GameObjectToString =
            new SerializableDictionary<GameObject, string>();

        public override TestCase Check()
        {
            // TODO: TESTS
            return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(),
                true, "Expected no null references.");
        }

        public override string GetIdentifier()
        {
            return "SerializableDictionary.Types";
        }
    }
}
#endif