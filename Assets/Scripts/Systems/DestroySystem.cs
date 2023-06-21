using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public partial struct DestroySystem : ISystem
    {
        public int point;
        public Entity spawner;
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (tf, e)in SystemAPI.Query<RefRO<DestroyComponent>>().WithAll<BulletComponent>().WithEntityAccess())       
            {   
                entityCommandBuffer.DestroyEntity(e);
            }
            foreach (var (tf, e) in SystemAPI.Query<RefRO<DestroyComponent>>().WithAll<ControlledMovingComponent>().WithEntityAccess())
            {
                foreach (var b in SystemAPI.Query<RefRW<PlayerDeadStatus>>())
                {
                    b.ValueRW.isDead = true;
                }
                entityCommandBuffer.DestroyEntity(e);
            }
            foreach (var (tf, e) in SystemAPI.Query<RefRO<DestroyComponent>>().WithNone<BulletComponent>().WithNone<ControlledMovingComponent>().WithEntityAccess())
            {
                entityCommandBuffer.DestroyEntity(e);
                point++;
            }
            foreach (var score in SystemAPI.Query<RefRW<ScoreComponent>>())
            {
                score.ValueRW.score = point;
            }
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}