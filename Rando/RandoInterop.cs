using Modding;
using ItemChanger;

namespace RegretRecoveryRestoration {
    internal static class RandoInterop {
        public static void Hook() {
            RandoMenuPage.Hook();
            RequestModifier.Hook();
            LogicAdder.Hook();

            RegretItem regretItem = new();
            Finder.DefineCustomItem(regretItem);

            if(ModHooks.GetMod("RandoSettingsManager") is Mod) {
                RSMInterop.Hook();
            }
        }
    }
}
