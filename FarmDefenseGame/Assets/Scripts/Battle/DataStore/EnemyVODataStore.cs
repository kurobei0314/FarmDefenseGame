using UnityEngine;
using Qitz.DataUtil;

namespace WolfVillageBattle
{
    [CreateAssetMenu]
    public class EnemyVODataStore : BaseDataStore<EnemyVO>
    {
        [ContextMenu("サーバーからデータを読み込む")]
        protected override void LoadDataFromServer()
        {
            base.LoadDataFromServer();
        }
    }
}
