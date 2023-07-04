using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public struct BulletPosition
    {
        public BlobArray<float3> value;
    }
    public struct BulletPositionAsset : IComponentData
    {
        public BlobAssetReference<BulletPosition> asset;
    }
    public class BulletSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float Speed = 4f;
        public float SpawnSpeed = 2f;
        public int num;
        public int max = 3;
        public List<float3> offset;

        public class BulletSpawnerComponentBaker : Baker<BulletSpawnerAuthoring>
        {
            public override void Bake(BulletSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new BulletSpawnerComponent
                    {
                        prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic), speed = authoring.Speed, spawnSpeed = authoring.SpawnSpeed
                        , num = authoring.num, max = authoring.max,
                    });
                BlobAssetReference<BulletPosition> bar;
                using (var bb = new BlobBuilder(Allocator.Temp))
                {
                    ref BulletPosition enp = ref bb.ConstructRoot<BulletPosition>();
                    BlobBuilderArray<float3> positions = bb.Allocate(ref enp.value, authoring.max);
                    for (int i = 0; i < authoring.max; i++)
                    {
                        positions[i] = authoring.offset.ElementAt(i);
                    }
                    bar = bb.CreateBlobAssetReference<BulletPosition>(Allocator.Persistent);
                }
                AddBlobAsset(ref bar, out var hash);
                AddComponent(new BulletPositionAsset() { asset = bar});
            }
        }
    }
}