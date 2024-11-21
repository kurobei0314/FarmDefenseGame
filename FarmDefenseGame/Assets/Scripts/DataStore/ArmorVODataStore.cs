using UnityEngine;
using Qitz.DataUtil;

namespace WolfVillageBattle
{
    [CreateAssetMenu]
    public class ArmorVODataStore : BaseDataStore<ArmorVO>
    {
        [ContextMenu("サーバーからデータを読み込む")]
        protected override void LoadDataFromServer()
        {
            base.LoadDataFromServer();
        }
    }
}