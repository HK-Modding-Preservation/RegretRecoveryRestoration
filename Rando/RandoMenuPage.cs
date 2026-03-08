using MenuChanger;
using MenuChanger.Extensions;
using MenuChanger.MenuElements;
using MenuChanger.MenuPanels;
using RandomizerMod.Menu;
using static RandomizerMod.Localization;

namespace RegretRecoveryRestoration {
    public class RandoMenuPage {
        internal MenuPage RegretRandoPage;
        internal MenuElementFactory<GlobalSettings> rrrMEF;
        internal VerticalItemPanel rrrVIP;

        internal SmallButton JumpmToRrrButton;

        internal static RandoMenuPage Instance { get; private set; }

        public static void OnExitMenu() {
            Instance = null;
        }

        public static void Hook() {
            RandomizerMenuAPI.AddMenuPage(ConstructMenu, HandleButton);
            MenuChangerMod.OnExitMainMenu += OnExitMenu;
        }

        private static bool HandleButton(MenuPage landingPage, out SmallButton button) {
            button = Instance.JumpmToRrrButton;
            return true;
        }

        private void SetTopLevelButtonColor() {
            if(JumpmToRrrButton != null) {
                JumpmToRrrButton.Text.color = RegretRecoveryRestoration.globalSettings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
            }
        }

        private static void ConstructMenu(MenuPage landingPage) => Instance = new(landingPage);

        private RandoMenuPage(MenuPage landingPage) {
            RegretRandoPage = new MenuPage(Localize("RegretRecovery"), landingPage);
            rrrMEF = new(RegretRandoPage, RegretRecoveryRestoration.globalSettings);
            rrrVIP = new(RegretRandoPage, new(0, 300), 75f, true, rrrMEF.Elements);
            Localize(rrrMEF);
            foreach(IValueElement e in rrrMEF.Elements) {
                e.SelfChanged += obj => SetTopLevelButtonColor();
            }

            JumpmToRrrButton = new(landingPage, Localize("RegretRecovery"));
            JumpmToRrrButton.AddHideAndShowEvent(landingPage, RegretRandoPage);
            SetTopLevelButtonColor();
        }
    }
}
