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
            state.RequireForUpdate<ResetCommand>();
        }
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var (tf, e) in SystemAPI.Query<RefRO<HP>>().WithNone<ControlledMovingComponent>().WithEntityAccess())
            {
                entityCommandBuffer.DestroyEntity(e);
            }
            foreach (var score in SystemAPI.Query<RefRW<ScoreComponent>>())
            {
                score.ValueRW.score = 0;
            }
            foreach (var spawner in SystemAPI.Query<RefRW<BulletSpawnerComponent>>())
            {
                spawner.ValueRW.num = 1;
            }
            foreach(var spawner in SystemAPI.Query<RefRW<EnemySpawnerComponent>>().WithNone<FirstEnemySpawnerComponent>())
            {
                spawner.ValueRW.canSpawn = false;
            }
            foreach (var spawner in SystemAPI.Query<RefRW<EnemySpawnerComponent>>().WithAll<FirstEnemySpawnerComponent>())
            {
                spawner.ValueRW.canSpawn = true;
            }
            foreach (var spawner in SystemAPI.Query<RefRW<BossSpawnerComponent>>())
            {
                spawner.ValueRW.canSpawn = false;
            }
            foreach (var dt in SystemAPI.Query<RefRW<PlayerDeadStatus>>())
            {   
                if(dt.ValueRW.isDead)
                {
                    foreach (var spawner in SystemAPI.Query<RefRW<PlayerSpawnerComponent>>())
                    {
                        spawner.ValueRW.canSpawn = true;
                    }
                }
                dt.ValueRW.isDead = false;
            }
            foreach (var w in SystemAPI.Query<RefRW<WinStatus>>())
            {
                w.ValueRW.isWin = false;
            }
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}