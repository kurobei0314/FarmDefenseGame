using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject;
using WolfVillage.ValueObject.Interface;
using WolfVillage.Battle;

namespace WolfVillage.Entity
{
    public class PlayerEntity
    {
        public PlayerEntity(IPlayerStatusVO playerStatusVO,
                            ISkillEntity[] skillEntities,
                            IWeaponEntity weaponEntity,
                            IArmorEntity armorEntity)
        {
            setCurrentSkills = new ISkillEntity[BattleGameInfo.PLAYER_SET_SKILL_NUM];
            for (var i = 0 ; i < setCurrentSkills.Length; i++)
            {
                setCurrentSkills[i] = (skillEntities.Length > i) ? skillEntities[i] : null;
            }
            // TODO: いつか消す
            setCurrentWeapon = weaponEntity;
            setCurrentArmor = armorEntity;
            this.playerStatusVO = (PlayerStatusVO) playerStatusVO; 
            // TODO: 武器の特性も見てMaxHPを加算する処理を作る
            current_max_hp = playerStatusVO.MaxHP;
        }

        private PlayerStatusVO playerStatusVO;
        public IPlayerStatusVO PlayerStatusVO => playerStatusVO;

        // MEMO: 武器によって最大HPが変化する可能性もあるため、一旦Entityに持ってる
        protected int current_max_hp;
        public int CurrentMaxHP => current_max_hp;

        // TODO: 武器をセットできるようにする(今は適当)
        protected IWeaponEntity setCurrentWeapon;
        public IWeaponEntity CurrentWeapon => setCurrentWeapon;

        // TODO: 防具をセットできるようにする(今は適当)
        protected IArmorEntity setCurrentArmor;
        public IArmorEntity CurrentArmor => setCurrentArmor;

        // TODO: スキルをセットできるようにする(今は適当)
        protected ISkillEntity[] setCurrentSkills;
        public ISkillEntity[] CurrentSkills => setCurrentSkills;
    }
}