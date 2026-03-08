using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerMod.RC;
using RandomizerMod.Settings;

namespace RegretRecoveryRestoration {
    public static class LogicAdder {
        public static void Hook() {
            RCData.RuntimeLogicOverride.Subscribe(50, ApplyLogic);
        }

        private static void ApplyLogic(GenerationSettings gs, LogicManagerBuilder lmb) {
            if(!RegretRecoveryRestoration.globalSettings.Enabled)
                return;
            lmb.AddItem(new EmptyItem(Consts.RegretItem));
        }
    }
}
