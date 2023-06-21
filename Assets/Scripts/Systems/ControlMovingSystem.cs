using Components;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct ControlMovingSystem:ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var dirX = Input.GetAxisRaw("Horizontal");
            var dirZ = Input.GetAxisRaw("Vertical");
            foreach (var (tf, moving) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<MovingComponent>>().WithAll<ControlledMovingComponent>())
            {
                tf.ValueRW.Linear = new Vector3(dirX * moving.ValueRO.moveSpeed, 0, dirZ * moving.ValueRO.moveSpeed);
            }
            foreach (var (tf, range) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<MovingRange>>().WithAll<ControlledMovingComponent>())
            {
                //tf.ValueRW.Position.x +=  horizontolInput*moving.ValueRO.moveSpeed * SystemAPI.Time.DeltaTime;
                //if (tf.ValueRW.Position.x < range.ValueRO.minX)
                //{
                //    tf.ValueRW.Position.x = range.ValueRO.minX;s
                //}
                //if (tf.ValueRW.Position.x > range.ValueRO.maxX)
                //{
                //    tf.ValueRW.Position.x = range.ValueRO.maxX;
                //}
            }
        }
        
    }
}