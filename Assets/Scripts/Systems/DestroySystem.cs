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
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StartCommand>();
        }
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
            foreach (var (tf, e) in SystemAPI.Query<RefRO<DestroyComponent>>().WithAll<EnemyComponent>().WithNone<BossTagComponent>().WithEntityAccess())
            {
                entityCommandBuffer.DestroyEntity(e);
                foreach (var score in SystemAPI.Query<RefRW<ScoreComponent>>())
                {
                    score.ValueRW.score ++;
                }
            }
            foreach (var (tf, e) in SystemAPI.Query<RefRO<DestroyComponent>>().WithAll<BossTagComponent>().WithEntityAccess())
            {
                entityCommandBuffer.DestroyEntity(e);
                foreach (var w in SystemAPI.Query<RefRW<WinStatus>>())
                {
                    w.ValueRW.isWin = true;
                }
            }
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}