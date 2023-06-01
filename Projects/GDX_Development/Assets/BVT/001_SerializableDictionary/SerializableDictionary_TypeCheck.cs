#if !UNITY_DOTSRUNTIME

using System.Collections.Generic;
using GDX.Collections.Generic;
using GDX.Developer.Reports.NUnit;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class SerializableDictionary_TypeCheck : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public SerializableDictionary<UnityEngine.Object, UnityEngine.Object> ObjectToObject = new SerializableDictionary<UnityEngine.Object, UnityEngine.Object>();


        public override TestCase Check()
        {
            bool foundUnityObjectNull = false;
            foreach (KeyValuePair<UnityEngine.Object, UnityEngine.Object> kvp in ObjectToObject)
            {
                if (kvp.Key == null || kvp.Value == null)
                {
                    foundUnityObjectNull = true;
                }
            }

            return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(),
                !foundUnityObjectNull, "Expected no null references.");
        }

        public override string GetIdentifier()
        {
            return "SerializableDictionary.Types";
        }
    }
}
#endif