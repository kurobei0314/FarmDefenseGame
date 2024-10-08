using UnityEngine;
using Qitz.DataUtil;

namespace WolfVillageBattle
{
    [CreateAssetMenu]
    public class FieldVODataStore : BaseDataStore<FieldVO>
    {
        [ContextMenu("サーバーからデータを読み込む")]
        protected override void LoadDataFromServer()
        {
            base.LoadDataFromServer();
        }
    }
}
