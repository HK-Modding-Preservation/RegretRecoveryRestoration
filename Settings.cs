namespace RegretRecoveryRestoration {
    public class GlobalSettings {
        public bool Enabled = false;
        [MenuChanger.Attributes.MenuRange(-1, 99)]
        public int Group = -1;
        public RecoveryLocation Location = RecoveryLocation.Random;
    }

    public enum RecoveryLocation {
        Random,
        EggShop
    }
}
