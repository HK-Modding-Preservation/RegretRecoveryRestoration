using Modding;
using System.Collections.Generic;
using UnityEngine;

namespace RegretRecoveryRestoration {
    public class RegretRecoveryRestoration: Mod, IGlobalSettings<GlobalSettings> {
        new public string GetName() => "RegretRecoveryRestoration";
        public override string GetVersion() => "1.0.0.0";

        public static GlobalSettings globalSettings { get; set; } = new();
        public void OnLoadGlobal(GlobalSettings s) => globalSettings = s;
        public GlobalSettings OnSaveGlobal() => globalSettings;

        internal static RegretRecoveryRestoration instance;

        public RegretRecoveryRestoration(): base(null) {
            instance = this;
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            RandoInterop.Hook();
            RegretItem.shadeFormPrefab = preloadedObjects["Room_Ouiji"]["Jiji NPC/Shade Form"];
            RegretItem.blackWavePrefab = preloadedObjects["Room_Ouiji"]["Jiji NPC/Black Wave"];
        }

        public override List<(string, string)> GetPreloadNames() {
            return [
                ("Room_Ouiji", "Jiji NPC/Shade Form"),
                ("Room_Ouiji", "Jiji NPC/Black Wave")
            ];
        }
    }
}