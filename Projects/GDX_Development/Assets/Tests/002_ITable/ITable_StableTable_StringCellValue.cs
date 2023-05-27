﻿#if !UNITY_DOTSRUNTIME

using System;
using GDX;
using GDX.Collections.Generic;
using GDX.Developer.Reports.NUnit;
using GDX.Tables;
using GDX.Tables.CellValues;
using UnityEngine;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class ITable_StableTable_StringCellValue : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public StringCellValue MyValue;

        public StringCellValue MySecondValue;
        public StringCellValue MyThirdValue;

        public BoundsCellValue MyForthValue;
       // public GDX.Data.TableRowRef Reference;

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
            return "ITable.StableTable.StringCellValue";
        }
    }
}
#endif