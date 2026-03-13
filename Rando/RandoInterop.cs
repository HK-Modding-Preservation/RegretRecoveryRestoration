using Modding;
using System.IO;
using ItemChanger;
using RandomizerMod.Logging;
using RandomizerMod.RandomizerData;

namespace RegretRecoveryRestoration {
    internal static class RandoInterop {
        public static void Hook() {
            RandoMenuPage.Hook();
            RequestModifier.Hook();
            LogicAdder.Hook();

            RegretItem regretItem = new();
            Finder.DefineCustomItem(regretItem);

            SettingsLog.AfterLogSettings += LogRandoSettings;

            if(ModHooks.GetMod("RandoSettingsManager") is Mod) {
                RSMInterop.Hook();
            }
        }

        private static void LogRandoSettings(LogArguments args, TextWriter w) {
            w.WriteLine("Logging RegretRecoveryRestoration settings:");
            w.WriteLine(JsonUtil.Serialize(RegretRecoveryRestoration.globalSettings));
        }
    }
}
