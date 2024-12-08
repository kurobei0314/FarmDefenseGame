namespace WolfVillage.Entity.Interface
{
    public enum Status
    {
        Idle,
        Attack,
        Damage,
        Die,
        Avoid,
        JustAvoid,
        JustAvoidAttack,
        Win, // player専用
        Notice, // Enemy専用
    }
    
    public interface ISetStatus
    {
        Status CurrentStatus { get; }
        void SetStatus(Status status);
    }
}