using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct BulletSpawnerSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var isPressedSpace = Input.GetAxisRaw("Fire1"); //CHANGE FROM GetKeyDown(Keycode.Space)
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);    
            foreach (var (tf, spawner, offset) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<BulletSpawnerComponent>, RefRO<BulletPositionAsset>>())
            {
                if (isPressedSpace==0)
                {
                    spawner.ValueRW.lastSpawnedTime = 0;
                }
                else
                {
                    if (spawner.ValueRO.lastSpawnedTime <= 0)
                    {
                        //Spawn Bullet
                        for(int i = 0; i < spawner.ValueRW.num;i++)
                        {
                            var newBulletE = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
                            ecb.SetComponent(newBulletE, new LocalTransform
                            {
                                Position = tf.ValueRO.Position + offset.ValueRO.asset.Value.value[i],
                                Scale = 1f,                              
                                Rotation = Quaternion.identity,
                            });
                            ecb.SetComponent(newBulletE, new BulletComponent
                            {
                                speed = 20f,
                                range = 14f,
                                direction = new float3(0, 1, 0),
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

        private float3 calculateDirection(RefRO<LocalTransform> tf)
        {
            //TODO:
            return new float3(0f, 0f, 1f);
        }
    }
}