namespace WolfVillageBattle.Interface
{
    public interface IFieldEnemyVO
    {
        int Id { get; }
        int FieldId { get; }
        int EnemyId { get; }
        float PosX { get; }
        float PosZ { get; }
        float RotationY { get; }
    }
}
