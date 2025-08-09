namespace WolfVillage.Battle
{
    public class BattleGameInfo 
    {
        // ジャスト回避した際にどのくらいの時間を遅くするか
        public const float JUST_AVOID_TIME_SCALE = 0.5f;

        // 弱点属性で殴った時の増加倍率
        public const float ATTACK_WEAK_TYPE_RATIO = 2.0f;
        
        // 全ての属性で攻撃力が上がる時の増加倍率(今は白属性)
        public const float ATTACK_GLOBAL_TYPE_RATIO = 1.5f;
    }

    public class BattleGameInputActionName
    {
        public const string PlayerMove = "Move";
        public const string PlayerWalk = "Walk";
        public const string CameraMove = "CameraMove";
    }
}

namespace WolfVillage.Search
{
    public class SearchGameInputActionName
    {
        public const string StickInput = "StickInput";
        public const string Decide = "Decide";
        public const string Cancel = "Cancel";
    }
}

namespace WolfVillage
{
    public class GameInfo 
    {
        // プレイヤーがセットできるスキルの数
        public const int PLAYER_SET_SKILL_NUM = 3;
    }

    public class ActionMapName
    {
        public const string BattleMap = "Battle";
        public const string SearchMap = "Search";
        public const string PlayerMenuUI = "PlayerMenuUI";
    }
}
