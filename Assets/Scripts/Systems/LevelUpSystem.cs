using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public partial struct LevelUpSystem : ISystem
    {
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
                if (score.ValueRO.score == 100)
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
        }
    }
}