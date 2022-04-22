using GDX.Developer.Reports.NUnit;

#if !UNITY_DOTSRUNTIME

namespace BVT
{
    // ReSharper disable once InconsistentNaming
    public class Config_InitInvoked : GDX.Developer.Reports.BuildVerification.SimpleTestBehaviour
    {
        public override TestCase Check()
        {
            return GDX.Developer.Reports.BuildVerificationReport.Assert(GetIdentifier(),
                GDX.Config.LocalizationSetDefaultCulture == false,
                "Expected value to be false.");
        }

        public override string GetIdentifier()
        {
            return "Config.InitInvoked";
        }
    }
}
#endif // !UNITY_DOTSRUNTIME