namespace WolfVillageBattle
{
    public class GameInfo 
    {
        // ジャスト回避した際にどのくらいの時間を遅くするか
        public const float JUST_AVOID_TIME_SCALE = 0.5f;

        // プレイヤーがセットできるスキルの数
        public const int PLAYER_SET_SKILL_NUM = 3;

        // 弱点属性で殴った時の増加倍率
        public const float ATTACK_WEAK_TYPE_RATIO = 2.0f;
        
        // 全ての属性で攻撃力が上がる時の増加倍率(今は白属性)
        public const float ATTACK_GLOBAL_TYPE_RATIO = 1.5f;
    }
}
