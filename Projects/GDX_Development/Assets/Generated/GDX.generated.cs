// Generated file of difference from default config.
#pragma warning disable
// ReSharper disable All
namespace GDX
{
    public class CustomConfig
    {
        public const int FormatVersion = 1;
        
        [UnityEngine.Scripting.Preserve]
        public static void Init()
        {
            GDX.Config.EnvironmentScriptingDefineSymbol = true;
            GDX.Config.EnvironmentToolsMenu = true;
            GDX.Config.LocalizationSetDefaultCulture = false;
            GDX.Config.TraceDebugLevels = Trace.TraceLevel.Info | Trace.TraceLevel.Log | Trace.TraceLevel.Warning | Trace.TraceLevel.Error | Trace.TraceLevel.Exception | Trace.TraceLevel.Assertion | Trace.TraceLevel.Fatal;
            GDX.Config.TraceReleaseLevels = Trace.TraceLevel.Info | Trace.TraceLevel.Log | Trace.TraceLevel.Warning | Trace.TraceLevel.Error | Trace.TraceLevel.Exception | Trace.TraceLevel.Assertion | Trace.TraceLevel.Fatal;
        }
    }
}