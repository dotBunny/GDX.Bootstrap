#if !UNITY_DOTSRUNTIME

using GDX.Collections.Generic;
using GDX.Developer.Reports.NUnit;
using UnityEngine;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class SerializableDictionary_UnityObjects : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public SerializableDictionary<CapsuleCollider, int> CapsuleToInteger = new SerializableDictionary<CapsuleCollider, int>();

        public override TestCase Check()
        {
            return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(),
                CapsuleToInteger.Count == 1,
                "Expected an element in dictionary");
        }

        public override string GetIdentifier()
        {
            return "SerializableDictionary.UnityObjects";
        }
    }
}
#endif