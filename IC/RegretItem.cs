using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using ItemChanger;
using ItemChanger.Tags;
using ItemChanger.UIDefs;

namespace RegretRecoveryRestoration {
    public class RegretItem: AbstractItem {
        public static GameObject shadeFormPrefab;
        public static GameObject blackWavePrefab;

        public RegretItem() {
            name = Consts.RegretItem;
            InteropTag tag = GetOrAddTag<InteropTag>();
            tag.Message = "RandoSupplementalMetadata";
            tag.Properties["ModSource"] = RegretRecoveryRestoration.instance.GetName();
            tag.Properties["PinSprite"] = new EmbeddedSprite("regret_pin");
            AddTag<PersistentItemTag>().Persistence = Persistence.SemiPersistent;
            UIDef = new MsgUIDef {
                name = new BoxedString("Regret Recovery"),
                shopDesc = new BoxedString("As a service, I can return your regrets to you, so they can be dealt with. You want that, yes?"),
                sprite = new EmbeddedSprite("regret_pin")
            };
        }

        public override void GiveImmediate(GiveInfo info) {
            PlayerData pd = PlayerData.instance;
            if(pd.shadeScene == "None")
                return;
            Vector3 position = info.Transform ? info.Transform.position : HeroController.instance.transform.position;
            pd.shadePositionX = position.x;
            pd.shadePositionY = position.y + 1;
            pd.shadeSpecialType = 0;
            pd.shadeScene = GameManager.instance.sceneName;
            doAsyncSpawn();
        }

        private async void doAsyncSpawn() {
            (GameObject, float, float, float)[] instantiations = [
                (shadeFormPrefab, -0.5f, -1.13f, 0.009f),
                (blackWavePrefab, -0.4f, 1.27f, 0.1f),
                (GameManager.instance.sm.hollowShadeObject, -0.5f, 1.08f, 0.009f)
            ];
            InstantiateFromTuple(instantiations[0]);
            await Task.Delay(2000);
            IEnumerable<GameObject> shades = GameObject.FindObjectsOfType<GameObject>().Where(go => go.name.StartsWith("Hollow Shade"));
            if(shades.Any()) {
                shades.First().SetActive(false);
            }
            InstantiateFromTuple(instantiations[1]);
            InstantiateFromTuple(instantiations[2]);
            
        }

        private void InstantiateFromTuple((GameObject go, float x, float y, float z) tuple) {
            PlayerData pd = PlayerData.instance;
            GameObject.Instantiate(tuple.go, new Vector3(pd.shadePositionX + tuple.x, pd.shadePositionY + tuple.y, tuple.z), Quaternion.identity).SetActive(true);
        }
    }
}
