namespace WolfVillageBattle.Interface
{
    public interface IFieldEnemyVO
    {
        int Id { get; }
        int FieldId { get; }
        int EnemyId { get; }
        int PosX { get; }
        int PosZ { get; }
        int RotationY { get; }
    }
}
