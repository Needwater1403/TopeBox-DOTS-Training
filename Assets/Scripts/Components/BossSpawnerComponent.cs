using Unity.Entities;

public partial struct BossSpawnerComponent : IComponentData
{
    public Entity prefab;
    public bool canSpawn;
}