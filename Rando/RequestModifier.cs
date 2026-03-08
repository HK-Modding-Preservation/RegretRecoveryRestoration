using ItemChanger;
using RandomizerCore.Logic;
using RandomizerMod.RandomizerData;
using RandomizerMod.RC;

namespace RegretRecoveryRestoration {
    public class RequestModifier {
        public static void Hook() {
            RequestBuilder.OnUpdate.Subscribe(-499, SetupItems);
            RequestBuilder.OnUpdate.Subscribe(-499.5f, DefinePools);
            RequestBuilder.OnUpdate.Subscribe(-99, ApplyJijiRegretDef);
        }

        private static void SetupItems(RequestBuilder rb) {
            if(!RegretRecoveryRestoration.globalSettings.Enabled)
                return;
            rb.EditItemRequest(Consts.RegretItem, info => {
                info.getItemDef = () => new ItemDef() {
                    Name = Consts.RegretItem,
                    Pool = "Regrets",
                    MajorItem = false,
                    PriceCap = 1
                };
            });
            if(rb.gs.NoveltySettings.EggShop) {
                switch(RegretRecoveryRestoration.globalSettings.Location) {
                    case RecoveryLocation.EggShop:
                        rb.AddToPreplaced(Consts.RegretItem, Consts.JijiLocation);
                        break;
                    case RecoveryLocation.Random:
                    default:
                        rb.AddItemByName(Consts.RegretItem);
                        break;
                }
            }
        }

        private static void ApplyJijiRegretDef(RequestBuilder rb) {
            if(!RegretRecoveryRestoration.globalSettings.Enabled || !rb.gs.NoveltySettings.EggShop)
                return;
            rb.EditLocationRequest(Consts.JijiLocation, info => {
                info.randoLocationCreator += factory => factory.MakeLocation(LocationNames.Egg_Shop);
                info.onRandoLocationCreation += (factory, rl) => {
                    rl.costs.Clear();
                    rl.AddCost(new SimpleCost(factory.lm.GetTermStrict("RANCIDEGGS"), 1));
                };
                info.customPlacementFetch = (factory, placement) => factory.FetchOrMakePlacementWithEvents(LocationNames.Egg_Shop, placement);
            });
        }

        private static void DefinePools(RequestBuilder rb) {
            GlobalSettings gs = RegretRecoveryRestoration.globalSettings;
            if(!gs.Enabled || !rb.gs.NoveltySettings.EggShop)
                return;
            if(rb.gs.SplitGroupSettings.RandomizeOnStart) {
                if(gs.Group >= 0 && gs.Group <= 2) {
                    gs.Group = rb.rng.Next(3);
                }
            }
            ItemGroupBuilder myGroup = null;
            if(gs.Group > 0) {
                string label = RBConsts.SplitGroupPrefix + "Regrets";
                foreach(ItemGroupBuilder igb in rb.EnumerateItemGroups()) {
                    if(igb.label == label) {
                        myGroup = igb;
                        break;
                    }
                }
                myGroup ??= rb.MainItemStage.AddItemGroup(label);
            }
            rb.OnGetGroupFor.Subscribe(0.01f, ResolveRrrGroup);
            bool ResolveRrrGroup(RequestBuilder rb, string item, RequestBuilder.ElementType type, out GroupBuilder gb) {
                if(type == RequestBuilder.ElementType.Item && item == Consts.RegretItem) {
                    gb = myGroup;
                    return true;
                }
                gb = default;
                return false;
            }
        }
    }
}
