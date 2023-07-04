using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{   
    public struct EnPosition
    {
        public BlobArray<float3> value;
    }
    public struct EnPositionAsset : IComponentData
    {
        public BlobAssetReference<EnPosition> asset;
    }
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public bool canSpawn;
        public int num;
        public float SpawnSpeed;
        public List<float3> pos;
        public class EnemySpawnerComponentBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new EnemySpawnerComponent
                    {
                        prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                        num = authoring.num,
                        spawnSpeed = authoring.SpawnSpeed,
                        canSpawn = authoring.canSpawn,
                    });

                BlobAssetReference<EnPosition> bar;
                using (var bb = new BlobBuilder(Allocator.Temp))
                {
                    ref EnPosition enp = ref bb.ConstructRoot<EnPosition>();
                    BlobBuilderArray<float3> positions = bb.Allocate(ref enp.value, authoring.num);
                    for(int i = 0; i < authoring.num; i++)
                    {
                        positions[i] = authoring.pos.ElementAt(i);
                    }
                    bar = bb.CreateBlobAssetReference<EnPosition>(Allocator.Persistent);
                }
                AddBlobAsset(ref bar, out var hash);
                AddComponent(new EnPositionAsset() { asset = bar });
            }
        }
    }
}
