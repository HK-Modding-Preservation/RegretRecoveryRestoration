using RandoSettingsManager;
using RandoSettingsManager.SettingsManagement;
using RandoSettingsManager.SettingsManagement.Versioning;

namespace RegretRecoveryRestoration {
    internal static class RSMInterop {
        public static void Hook() {
            RandoSettingsManagerMod.Instance.RegisterConnection(new RrrSettingsProxy());
        }
    }

    internal class RrrSettingsProxy: RandoSettingsProxy<GlobalSettings, string> {
        public override string ModKey => RegretRecoveryRestoration.instance.GetName();

        public override VersioningPolicy<string> VersioningPolicy { get; } = new EqualityVersioningPolicy<string>(RegretRecoveryRestoration.instance.GetName());

        public override void ReceiveSettings(GlobalSettings settings) {
            settings ??= new();
            RandoMenuPage.Instance.rrrMEF.SetMenuValues(settings);
        }

        public override bool TryProvideSettings(out GlobalSettings settings) {
            settings = RegretRecoveryRestoration.globalSettings;
            return settings.Enabled;
        }
    }
}
