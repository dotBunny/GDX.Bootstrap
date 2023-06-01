#if !UNITY_DOTSRUNTIME

using GDX.Collections.Generic;
using GDX.Developer.Reports.NUnit;
using UnityEngine;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class SerializableDictionary_ReferencedName : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public GameObject KeyObject;
        public SerializableDictionary<GameObject, GameObject> GameObjectToGameObject = new SerializableDictionary<GameObject, GameObject>();

        public override TestCase Check()
        {
            bool found = GameObjectToGameObject.TryGetValue(KeyObject, out GameObject foundObject);
            bool correctName = false;
            if (found)
            {
                correctName = foundObject.name == "ReferencedName";

            }
            return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(),
                found && correctName, "Expected no null references.");
        }

        public override string GetIdentifier()
        {
            return "SerializableDictionary.ReferencedName";
        }
    }
}
#endif