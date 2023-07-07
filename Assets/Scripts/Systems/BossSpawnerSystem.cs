using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using static Systems.EnemySpawnerSystem;

public partial struct BossSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnBossCommand>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach (var (tf, e) in SystemAPI.Query<RefRO<HP>>().WithNone<ControlledMovingComponent>().WithNone<BossTagComponent>().WithEntityAccess())
        {
            ecb.DestroyEntity(e);
        }
        foreach (var spawner in SystemAPI.Query<RefRW<EnemySpawnerComponent>>())
        {
            spawner.ValueRW.canSpawn = false;
        }
        foreach (var spawner in SystemAPI.Query<RefRW<BossSpawnerComponent>>())
        {
            ecb.Instantiate(spawner.ValueRO.prefab);
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();       
    }
}
