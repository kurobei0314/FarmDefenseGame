using WolfVillage.Entity.Interface;

namespace WolfVillage.Search.PlayerMenuUI.SkillMenu
{
    public interface ISetSkillUseCase
    {
        ISkillEntity[] CurrentSkillEntities { get; }
        ISkillEntity[] HasSkillEntities { get; }
    }
}
