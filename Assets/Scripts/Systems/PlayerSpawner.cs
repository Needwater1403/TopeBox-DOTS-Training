using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<StartCommand>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        foreach(var spawner in SystemAPI.Query<RefRW<PlayerSpawnerComponent>>())
        {      
            if(spawner.ValueRW.canSpawn)
            {
                ecb.Instantiate(spawner.ValueRO.prefab);
                spawner.ValueRW.canSpawn = false;
            }
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
