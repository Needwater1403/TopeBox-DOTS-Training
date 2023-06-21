using Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    public partial struct DamagingSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (hp, itd,ent) in SystemAPI.Query<RefRW<HP>,RefRW<IsTakingDamage>>().WithEntityAccess())     
            {
                if(itd.ValueRO.takeDamage)
                {
                    hp.ValueRW.hp -= itd.ValueRO.dmgTaken;
                    itd.ValueRW.takeDamage = false;
                }
                if (hp.ValueRW.hp <= 0)
                {
                    ecb.AddComponent(ent, new DestroyComponent { });
                }
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}
