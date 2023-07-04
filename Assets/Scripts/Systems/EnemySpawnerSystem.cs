using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct EnemySpawnerSystem : ISystem
    {
        readonly RefRO<EnPositionAsset> asset;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StartCommand>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            state.Dependency = new SpawnJob { deltaTime = SystemAPI.Time.DeltaTime, ecb = ecb.AsParallelWriter()}.ScheduleParallel(state.Dependency);
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
        public partial struct SpawnJob : IJobEntity
        {
            public float deltaTime;
            public EntityCommandBuffer.ParallelWriter ecb;
            void Execute(RefRW<EnemySpawnerComponent> spawner, RefRW<LocalTransform> tf, RefRO<MeshComponent> m, RefRO<EnPositionAsset> pos)
            {
                if(spawner.ValueRW.canSpawn)
                {
                    if (spawner.ValueRW.lastSpawnedTime <= 0)
                    {
                        for (int i = 0; i < spawner.ValueRO.num; i++)
                        {
                            var newEnemyE = ecb.Instantiate(i, spawner.ValueRO.prefab);
                            ecb.SetComponent(i, newEnemyE, new LocalTransform
                            {
                                Position = pos.ValueRO.asset.Value.value[i],
                                Rotation = quaternion.identity,
                                Scale = 1,
                            });
                            if (i % 2 == 0)
                            {
                                ecb.SetComponent(i, newEnemyE, new MaterialMeshInfo
                                {
                                    MaterialID = m.ValueRO.materialID,
                                    MeshID = m.ValueRO.meshID,
                                });
                            }
                            spawner.ValueRW.lastSpawnedTime = spawner.ValueRO.spawnSpeed;
                        }
                    }
                    else
                    {
                        spawner.ValueRW.lastSpawnedTime -= deltaTime;
                    }
                }
            }
        }
    }
}