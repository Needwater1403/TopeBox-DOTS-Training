using Unity.Entities;
using Components;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;

namespace Systems
{
    public partial struct MovingSystem:ISystem
    {   
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (tf, moving, range, e) in SystemAPI.Query<RefRW<LocalTransform>
                         , RefRW<MovingComponent>, RefRO<MovingRange>>().WithNone<ControlledMovingComponent>().WithEntityAccess())
            {
                tf.ValueRW.Position.x += moving.ValueRO.moveSpeed * moving.ValueRW.dir.x * SystemAPI.Time.DeltaTime;
                if (tf.ValueRW.Position.x <= range.ValueRO.minX)
                {
                    moving.ValueRW.dir.x = -moving.ValueRW.dir.x;
                    tf.ValueRW.Position.x += moving.ValueRO.moveSpeed * moving.ValueRW.dir.x * SystemAPI.Time.DeltaTime; //WILL HAVE BUG IF REMOVE (STILL DONT KNOW THE REASON)
                    tf.ValueRW.Position.y -= 2;
                }
                 if (tf.ValueRW.Position.x >= range.ValueRO.maxX)
                {
                    moving.ValueRW.dir.x = -moving.ValueRW.dir.x;
                    tf.ValueRW.Position.x += moving.ValueRO.moveSpeed * moving.ValueRW.dir.x * SystemAPI.Time.DeltaTime;//                           //
                    tf.ValueRW.Position.y -= 2;
                }
            }
        }
    }
}