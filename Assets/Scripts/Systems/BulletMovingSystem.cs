using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public partial struct BulletMovingSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            
            new MovingJob { deltaTime = SystemAPI.Time.DeltaTime }.ScheduleParallel();
        }
        public partial struct MovingJob : IJobEntity
        {
            public float deltaTime;

            void Execute(RefRW<LocalTransform> tf, RefRO<BulletComponent> bullet, RefRW<HP> hp)
            {
                tf.ValueRW.Position += bullet.ValueRO.direction * bullet.ValueRO.speed * deltaTime;
                if(tf.ValueRW.Position.y >= 14 || tf.ValueRW.Position.y <= -14)
                {
                    hp.ValueRW.hp = 0;
                }
            }
        }
    }
}