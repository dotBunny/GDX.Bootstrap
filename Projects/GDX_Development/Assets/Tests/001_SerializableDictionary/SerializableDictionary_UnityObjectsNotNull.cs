#if !UNITY_DOTSRUNTIME

using GDX.Collections.Generic;
using GDX.Developer.Reports.NUnit;
using UnityEngine;

namespace BVT
{
    // ReSharper disable once InconsistentNaming
    public class SerializableDictionary_UnityObjectsNotNull : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
    {
        public SerializableDictionary<CapsuleCollider, int> CapsuleToInteger = new SerializableDictionary<CapsuleCollider, int>();

        public override TestCase Check()
        {
            bool nullReferences = false;
            foreach (CapsuleCollider c in CapsuleToInteger.Keys)
            {
                if (c == null)
                {
                    nullReferences = true;
                }
            }
            return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(),
                !nullReferences, "Expected no null references.");
        }

        public override string GetIdentifier()
        {
            return "SerializableDictionary.UnityObjectsNotNull";
        }
    }
}
#endif