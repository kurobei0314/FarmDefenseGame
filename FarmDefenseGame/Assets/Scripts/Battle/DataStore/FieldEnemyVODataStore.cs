using UnityEngine;
using Qitz.DataUtil;
using WolfVillage.ValueObject;

namespace WolfVillage.MasterDataStore
{
    [CreateAssetMenu]
    public class FieldEnemyVODataStore : BaseDataStore<FieldEnemyVO>
    {
        [ContextMenu("サーバーからデータを読み込む")]
        protected override void LoadDataFromServer()
        {
            base.LoadDataFromServer();
        }
    }
}
