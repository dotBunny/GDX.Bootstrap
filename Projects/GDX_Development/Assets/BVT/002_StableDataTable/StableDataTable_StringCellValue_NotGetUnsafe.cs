#if !UNITY_DOTSRUNTIME

using GDX.Developer.Reports.NUnit;
using GDX.DataTables.CellValues;

namespace BVT
{
#pragma warning disable IDE1006
    // ReSharper disable once InconsistentNaming
    public class StableDataTable_StringCellValue_NotGetUnsafe : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
#pragma warning restore IDE1006
    {
        public StringCellValue First;
        public StringCellValue Second;

        public override TestCase Check()
        {
            return GDX.Developer.Reports.BuildVerificationReport.Assert(
                GetIdentifier(),
                First.GetUnsafe() == null && Second.GetUnsafe() == null,
                $"Expected null ({First.GetUnsafe()}) and null ({Second.GetUnsafe()})");
        }

        public override string GetIdentifier()
        {
            return "StableDataTable.StringCellValue.NotGetUnsafe";
        }
    }
}
#endif