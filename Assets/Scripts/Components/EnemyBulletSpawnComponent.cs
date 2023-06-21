using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public partial struct EnemyBulletSpawnerComponent : IComponentData
    {
        public Entity prefab;

        public float spawnSpeed;

        public float lastSpawnedTime;

        public float3 offset;

    }
}