using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct EnemyBulletSpawnerSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (tf, spawner) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<EnemyBulletSpawnerComponent>>())
            {       
                foreach(var tf1 in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<ControlledMovingComponent>())
                {
                    if (spawner.ValueRO.lastSpawnedTime <= 0)
                    {
                        //Spawn Bullet
                        var newBulletE = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
                        state.EntityManager.SetComponentData(newBulletE, new LocalTransform
                        {
                            Position = tf.ValueRO.Position + spawner.ValueRO.offset,
                            Scale = 1f,
                            Rotation = Quaternion.identity,
                        });
                        
                        state.EntityManager.SetComponentData(newBulletE, new BulletComponent
                        {
                            speed = 10f,
                            range = 14f,
                            direction = math.normalize(tf1.ValueRO.Position - tf.ValueRO.Position),
                        });
                        spawner.ValueRW.lastSpawnedTime = spawner.ValueRO.spawnSpeed;
                    }
                    else
                    {
                        spawner.ValueRW.lastSpawnedTime -= SystemAPI.Time.DeltaTime;
                    }
                }
                    
            }
        }
        private float3 calculateDirection(RefRO<LocalTransform> tf)
        {
            //TODO:
            return new float3(0f, 0f, 1f);
        }
    }
}