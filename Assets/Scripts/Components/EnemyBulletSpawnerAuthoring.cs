using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class EnemyBulletSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float SpawnSpeed;

        public Vector3 Offset;

        public class EnemyBulletSpawnerComponentBaker : Baker<EnemyBulletSpawnerAuthoring>
        {
            public override void Bake(EnemyBulletSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new EnemyBulletSpawnerComponent
                    {
                        prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                        spawnSpeed = authoring.SpawnSpeed,
                        offset = authoring.Offset
                    });
            }
        }
    }
}