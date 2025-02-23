using UnityEngine;
using WolfVillage.Battle.Interface;
using System.Linq;
using WolfVillage.Entity.Interface;
using WolfVillage.ValueObject.Interface;
using WolfVillage.Entity;
using WolfVillage.MasterDataStore;

namespace WolfVillage.Battle.Interface
{
    public interface IMainGameRepository
    {
        IBattlePlayerEntity Player { get; }
        InitializeEnemyDTO[] Enemies { get; }
        void Initialize(PlayerEntity playerEntity);
    }
}

namespace WolfVillage.Battle
{
    public class InitializeEnemyDTO
    {
        private IEnemyEntity enemyEntity;
        public IEnemyEntity EnemyEntity => enemyEntity;
        private Vector3 pos;
        public Vector3 Pos => pos;
        private Vector3 rotation;
        public Vector3 Rotation => rotation;

        public InitializeEnemyDTO(IEnemyEntity enemyEntity, float posX, float posZ, float rotationY)
        {
            this.enemyEntity = enemyEntity;
            this.pos = new Vector3(posX, 0.0f, posZ);
            this.rotation = new Vector3(0.0f, rotationY, 0.0f);
        }
    }

    [CreateAssetMenu]
    public class MainGameRepository : ScriptableObject, IMainGameRepository
    {
        [SerializeField] private PlayerStatusVODataStore playerDataStore;
        [SerializeField] private EnemyVODataStore enemyVODataStore;
        [SerializeField] private FieldEnemyVODataStore fieldEnemyDataStore;
        [SerializeField] private FieldVODataStore fieldDataStore;
        [SerializeField] private WeaponVODataStore weaponVODataStore;
        [SerializeField] private SkillVODataStore skillVODataStore;
        [SerializeField] private ArmorVODataStore armorVODataStore;
        
        private IBattlePlayerEntity battlePlayer;
        public IBattlePlayerEntity Player => battlePlayer;

        private InitializeEnemyDTO[] enemies;
        public InitializeEnemyDTO[] Enemies => enemies;

        private IFieldEnemyVO fieldObjectVO;
        private IFieldVO fieldVO;
        public IFieldVO FieldVO => fieldVO;

        public void Initialize(PlayerEntity playerEntity)
        {
            // TODO: フィールドごとにちゃんと持って来れるようにする
            fieldVO = fieldDataStore.Items.FirstOrDefault();
            var fieldEnemies = fieldEnemyDataStore.Items.Where(fieldEnemy => fieldVO.Id == fieldEnemy.FieldId).ToArray();

            battlePlayer = new Entity.BattlePlayerEntity(playerEntity.PlayerStatusVO, playerEntity.CurrentAllRoleTypeSkills, playerEntity.CurrentWeapon, playerEntity.CurrentArmor);
            Debug.Log($"<color=red>プレイヤーの装備品 {battlePlayer.CurrentWeapon.WeaponVO.Name} {battlePlayer.CurrentArmor.ArmorVO.Name}</color>");
            enemies = new InitializeEnemyDTO[fieldEnemies.Length];
            for (int i = 0; i < fieldEnemies.Length ; i++)
            {
                var fieldEnemy = fieldEnemies[i];
                var enemy = enemyVODataStore.Items.FirstOrDefault(enemy => enemy.Id == fieldEnemy.EnemyId);
                enemies[i] = new InitializeEnemyDTO(new EnemyEntity(enemy), fieldEnemy.PosX, fieldEnemy.PosZ, fieldEnemy.RotationY);
            }
        }
    }
}
