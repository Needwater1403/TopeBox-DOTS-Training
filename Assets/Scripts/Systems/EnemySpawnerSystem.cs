using Components;
using System.Drawing;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct EnemySpawnerSystem : ISystem
    {
        private int count;
        private int max;
        private bool a;
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (spawner, tf) in SystemAPI.Query<RefRW<EnemySpawnerComponent>, RefRW<LocalTransform>>())
            {
                max = spawner.ValueRO.num * 2;
                if(spawner.ValueRW.lastSpawnedTime <= 0)
                {
                    spawner.ValueRW.canSpawn = false;
                    for (int i = 0; i < spawner.ValueRO.num * 2; i += 2)
                    {
                        var newEnemyE = ecb.Instantiate(spawner.ValueRO.prefab);
                        ecb.SetComponent(newEnemyE, new LocalTransform
                        {
                            Position = tf.ValueRO.Position + new float3(i, 0, 0),
                            Rotation = quaternion.identity,
                            Scale = 1,
                        });
                        count = i;
                        spawner.ValueRW.lastSpawnedTime = spawner.ValueRO.spawnSpeed;
                    }
                }
                else
                {
                    spawner.ValueRW.lastSpawnedTime -= SystemAPI.Time.DeltaTime;
                }
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}