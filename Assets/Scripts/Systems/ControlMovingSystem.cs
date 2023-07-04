using Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct ControlMovingSystem:ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StartCommand>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dirX = Input.GetAxisRaw("Horizontal");
            var dirY = Input.GetAxisRaw("Vertical");
            foreach (var (tf, moving, range,ent) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MovingComponent>, RefRO<MovingRange>>().WithAll<ControlledMovingComponent>().WithEntityAccess())
            {
                state.EntityManager.SetComponentData(ent, new PhysicsVelocity
                {
                    Linear = new Vector3(dirX * moving.ValueRO.moveSpeed, dirY * moving.ValueRO.moveSpeed, 0),
                });
                if (tf.ValueRW.Position.x < range.ValueRO.minX)
                {
                    tf.ValueRW.Position.x = range.ValueRO.minX; 
                }
                if (tf.ValueRW.Position.x > range.ValueRO.maxX)
                {
                    tf.ValueRW.Position.x = range.ValueRO.maxX;
                }
                if (tf.ValueRW.Position.y < range.ValueRO.minY)
                {
                    tf.ValueRW.Position.y = range.ValueRO.minY;
                }
            }
        }
        
    }
}