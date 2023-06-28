using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Components;
using Unity.Transforms;

public partial struct EnemyFormationSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //foreach (var (tf,f) in SystemAPI.Query<RefRW<LocalTransform>,RefRW<EnemyFormationComponent>>())
        //{
        //    for(int i =0; i<2; i++)
        //    {
        //        for(int j = 0; j<4; j++)
        //        {   
        //            tf.ValueRW.Position += new float3(j,i,0);
        //            f.ValueRW.formation.Add(tf.ValueRW.Position);
        //        }
        //    }
        //}
    }
}
