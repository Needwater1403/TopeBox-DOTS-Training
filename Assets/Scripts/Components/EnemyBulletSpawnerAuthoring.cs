using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public struct EnBulletPosition
    {
        public BlobArray<float3> value;
    }
    public struct EnBulletPositionAsset : IComponentData
    {
        public BlobAssetReference<EnBulletPosition> asset;
    }
    public class EnemyBulletSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float SpawnSpeed;
        public float lastSpawnTime;
        public int num;
        public int max;
        public List<float3> offset;

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
                        lastSpawnedTime = 1,
                        num = authoring.num,
                        max = authoring.max,
                    });
                BlobAssetReference<EnBulletPosition> bar;
                using (var bb = new BlobBuilder(Allocator.Temp))
                {
                    ref EnBulletPosition enp = ref bb.ConstructRoot<EnBulletPosition>();
                    BlobBuilderArray<float3> positions = bb.Allocate(ref enp.value, authoring.max);
                    for (int i = 0; i < authoring.max; i++)
                    {
                        positions[i] = authoring.offset.ElementAt(i);
                    }
                    bar = bb.CreateBlobAssetReference<EnBulletPosition>(Allocator.Persistent);
                }
                AddBlobAsset(ref bar, out var hash);
                AddComponent(new EnBulletPositionAsset() { asset = bar });
            }
        }
    }
}