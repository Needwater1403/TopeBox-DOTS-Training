using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct EnemyBulletSpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (tf, spawner, offset) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<EnemyBulletSpawnerComponent>, RefRO<EnBulletPositionAsset>>())
            {       
                foreach(var tf1 in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<ControlledMovingComponent>())
                {
                    if (spawner.ValueRO.lastSpawnedTime <= 0)
                    {
                        //Spawn Bullet
                        for(int i = 0; i < spawner.ValueRW.num; i++)
                        {
                            var newBulletE = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
                            ecb.SetComponent(newBulletE, new LocalTransform
                            {
                                Position = tf.ValueRO.Position + offset.ValueRO.asset.Value.value[i],
                                Scale = 1f,
                                Rotation = Quaternion.identity,
                            });
                            ecb.AddComponent(newBulletE, new BulletComponent
                            {
                                speed = 15f,
                                range = 14f,
                                direction = math.normalize(tf1.ValueRO.Position - tf.ValueRO.Position - offset.ValueRO.asset.Value.value[i]),
                            });
                            spawner.ValueRW.lastSpawnedTime = spawner.ValueRO.spawnSpeed;
                        }
                    }
                    else
                    {
                        spawner.ValueRW.lastSpawnedTime -= SystemAPI.Time.DeltaTime;
                    }
                }
                    
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}