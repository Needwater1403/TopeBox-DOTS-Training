using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using CortexDeveloper.ECSMessages.Service;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct LevelUpSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach(var score in SystemAPI.Query<RefRO<ScoreComponent>>())
            {   
                if(score.ValueRO.score == 50)
                {
                    foreach (var bulletNum in SystemAPI.Query<RefRW<BulletSpawnerComponent>>())
                    {
                        bulletNum.ValueRW.num = 2;
                    }
                }
                else if (score.ValueRO.score == 75)
                {
                    foreach (var spawner in SystemAPI.Query<RefRW<EnemySpawnerComponent>>())
                    {
                        if(!spawner.ValueRW.canSpawn)
                        {
                            spawner.ValueRW.canSpawn = true;
                        }
                        spawner.ValueRW.spawnSpeed = 5;
                    }
                    foreach (var bulletNum in SystemAPI.Query<RefRW<BulletSpawnerComponent>>())
                    {
                       bulletNum.ValueRW.num = 3;
                       state.Enabled = false;
                    } 
                }
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}