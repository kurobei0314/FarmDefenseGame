using UnityEngine;
using Qitz.DataUtil;
using WolfVillage.ValueObject;

namespace WolfVillageBattle
{
    [CreateAssetMenu]
    public class ItemVODataStore : BaseDataStore<ItemVO>
    {
        [ContextMenu("サーバーからデータを読み込む")]
        protected override void LoadDataFromServer()
        {
            base.LoadDataFromServer();
        }
    }
}
