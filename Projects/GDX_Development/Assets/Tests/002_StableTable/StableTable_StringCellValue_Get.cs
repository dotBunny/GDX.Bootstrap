#if !UNITY_DOTSRUNTIME

using GDX.Developer.Reports.NUnit;
using GDX.Tables.CellValues;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class StableTable_StringCellValue_Get : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public StringCellValue First;
        public StringCellValue Second;

        public override TestCase Check()
        {
            return GDX.Developer.Reports.BuildVerificationReport.Assert(
                GetIdentifier(),
                First.Get() == "A1" && Second.Get() == "B2", 
                $"Expected A1 ({First.Get()}) and B2 ({Second.Get()})");
        }

        public override string GetIdentifier()
        {
            return "StableTable.StringCellValue.Get";
        }
    }
}
#endif