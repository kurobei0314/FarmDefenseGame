using UnityEngine;
using Qitz.DataUtil;
using WolfVillage.ValueObject;

namespace WolfVillageBattle
{
    [CreateAssetMenu]
    public class SkillVODataStore : BaseDataStore<SkillVO>
    {
        [ContextMenu("サーバーからデータを読み込む")]
        protected override void LoadDataFromServer()
        {
            base.LoadDataFromServer();
        }
    }
}