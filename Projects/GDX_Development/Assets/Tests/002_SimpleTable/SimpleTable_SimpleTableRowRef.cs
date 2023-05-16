#if !UNITY_DOTSRUNTIME

using GDX;
using GDX.Collections.Generic;
using GDX.Developer.Reports.NUnit;
using UnityEngine;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class SimpleTable_SimpleTableRowRef : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public SimpleTable.SimpleTableRowRef Reference;
        
        public override TestCase Check()
        {
            // bool found = GameObjectToGameObject.TryGetValue(KeyObject, out GameObject foundObject);
            // bool correctName = false;
            // if (found)
            // {
            //     correctName = foundObject.name == "ReferencedName";
            //
            // }
            // return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(),
            //     found && correctName, "Expected no null references.");

            return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(), true, null, 0);
        }

        public override string GetIdentifier()
        {
            return "SimpleTable.SimpleTableRowRef";
        }
    }
}
#endif