using UnityEngine;
using Qitz.DataUtil;
using WolfVillage.ValueObject;

namespace WolfVillage.MasterDataStore
{
    [CreateAssetMenu]
    public class PlayerStatusVODataStore : BaseDataStore<PlayerStatusVO>
    {
        [ContextMenu("サーバーからデータを読み込む")]
        protected override void LoadDataFromServer()
        {
            base.LoadDataFromServer();
        }
    }
}