#if !UNITY_DOTSRUNTIME

using GDX.Developer.Reports.NUnit;
using GDX.DataTables.CellValues;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class StableDataTable_StringCellValue_GetUnsafe : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public StringCellValue First;
        public StringCellValue Second;

        public override TestCase Check()
        {
            First.Get();
            Second.Get();

            return GDX.Developer.Reports.BuildVerificationReport.Assert(
                GetIdentifier(),
                First.GetUnsafe() == "A2" && Second.GetUnsafe() == "B1",
                $"Expected A2 ({First.GetUnsafe()}) and B1 ({Second.GetUnsafe()})");
        }

        public override string GetIdentifier()
        {
            return "StableDataTable.StringCellValue.GetUnsafe";
        }
    }
}
#endif