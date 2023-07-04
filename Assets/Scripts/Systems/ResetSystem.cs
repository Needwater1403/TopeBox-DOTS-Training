using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public partial struct ResetSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StartCommand>();
        }
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            //foreach (var (tf, e) in SystemAPI.Query<RefRO<DestroyComponent>>().WithNone<ControlledMovingComponent>().WithEntityAccess())
            //{
            //    entityCommandBuffer.DestroyEntity(e);
            //}
            //foreach (var score in SystemAPI.Query<RefRW<ScoreComponent>>())
            //{
            //    score.ValueRW.score = 0;
            //}
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}