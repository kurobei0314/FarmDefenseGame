using UnityEngine;

namespace WolfVillage.ValueObject.Interface
{
    public interface IFieldVO
    {
        string Name { get; }
        int Id { get; }
        string SceneName { get; }
        Vector3 PlayerInitPos { get; }
        Vector3 PlayerInitRot { get; }
    }
}

