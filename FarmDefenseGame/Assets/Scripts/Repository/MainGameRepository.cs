using UnityEngine;
using WolfVillageBattle.Interface;
using System.Linq;

namespace WolfVillageBattle.Interface
{
    public interface IMainGameRepository
    {
        IPlayerEntity Player { get; }
        InitializeEnemyDTO[] Enemies { get; }
        void Initialize();
    }
}

namespace WolfVillageBattle
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
        
        private IPlayerEntity player;
        public IPlayerEntity Player => player;

        private InitializeEnemyDTO[] enemies;
        public InitializeEnemyDTO[] Enemies => enemies;

        private IFieldEnemyVO fieldObjectVO;
        private IFieldVO fieldVO;

        public void Initialize()
        {
            // TODO: フィールドごとにちゃんと持って来れるようにする
            fieldVO = fieldDataStore.Items.FirstOrDefault();
            var fieldEnemies = fieldEnemyDataStore.Items.Where(fieldEnemy => fieldVO.Id == fieldEnemy.FieldId).ToArray();

            // TODO: プレイヤーがセットしたスキルを取得できるようにする
            var setSkillVO = skillVODataStore.Items.Where(skillVO => skillVO.Id == 1).ToArray();
            var skillEntities = setSkillVO.Select(skillVO => new SkillEntity(skillVO)).ToArray();

            // TODO: プレイヤーが装備した武器を取得できるようにする
            var setWeaponVO = weaponVODataStore.Items.Where(vo => vo.Id == 1).ToArray();
            var weaponEntity = setWeaponVO.Select(vo => new WeaponEntity(vo)).FirstOrDefault();

            // TODO: クリアしたフィールドの情報からStatusを取得するようにする
            var playerStatusVO = playerDataStore.Items.FirstOrDefault();
            player = new PlayerEntity(playerStatusVO, skillEntities, weaponEntity);
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
