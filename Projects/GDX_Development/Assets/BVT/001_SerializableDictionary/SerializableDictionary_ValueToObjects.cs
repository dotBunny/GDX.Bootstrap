#if !UNITY_DOTSRUNTIME

using GDX.Collections.Generic;
using GDX.Developer.Reports.NUnit;
using UnityEngine;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class SerializableDictionary_ValueToObjects : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public SerializableDictionary<int, CapsuleCollider> IntegerToCapsule = new SerializableDictionary<int, CapsuleCollider>();

        public override TestCase Check()
        {
            return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(),
                IntegerToCapsule.Count == 2,
                "Expected two entries in dictionary");
        }

        public override string GetIdentifier()
        {
            return "SerializableDictionary.ValueToObjects";
        }
    }
}
#endif