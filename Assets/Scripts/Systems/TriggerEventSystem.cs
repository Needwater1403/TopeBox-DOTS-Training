using Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.IO.LowLevel.Unsafe;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SimulationSystemGroup))]
    public partial struct TriggerEventsSystem : ISystem
    {
        struct ComponentDataHandles
        {
            public ComponentLookup<IsTakingDamage> isTakingDamage;
            public ComponentLookup<Damage> dmg;

            public ComponentDataHandles(ref SystemState systemState)
            {
                isTakingDamage = systemState.GetComponentLookup<IsTakingDamage>(false);
                dmg = systemState.GetComponentLookup<Damage>(false);
            }
            public void Update(ref SystemState systemState)
            {
                isTakingDamage.Update(ref systemState);
                dmg.Update(ref systemState);
            }
        }
        ComponentDataHandles data;
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<EnemyComponent>();
            state.RequireForUpdate<BulletComponent>();
            data = new ComponentDataHandles(ref state);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

            data.Update(ref state);
            state.Dependency = new BulletTriggerEvents
            {
                dmg = data.dmg,
                isTakingDamage = data.isTakingDamage,
                enemyList = SystemAPI.GetComponentLookup<EnemyComponent>(),
                playerBulletList = SystemAPI.GetComponentLookup<PlayerBulletComponent>(),
                player = SystemAPI.GetComponentLookup<ControlledMovingComponent>(),
                enemyBulletList = SystemAPI.GetComponentLookup<EnemyBulletComponent>(),

            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            state.Dependency.Complete();
        }
        public struct BulletTriggerEvents : ITriggerEventsJob
        {
            public ComponentLookup<IsTakingDamage> isTakingDamage;
            public ComponentLookup<Damage> dmg;
            public ComponentLookup<EnemyComponent> enemyList;
            public ComponentLookup<PlayerBulletComponent> playerBulletList;
            public ComponentLookup<ControlledMovingComponent> player;
            public ComponentLookup<EnemyBulletComponent> enemyBulletList;
            public void Execute(Unity.Physics.TriggerEvent collisionEvent)
            {
                Entity entB = collisionEvent.EntityB;
                Entity entA = collisionEvent.EntityA;
                var isEnemyA = enemyList.HasComponent(collisionEvent.EntityA);
                var isBulletA = playerBulletList.HasComponent(collisionEvent.EntityA);
                var isEnemyB = enemyList.HasComponent(collisionEvent.EntityB);
                var isBulletB = playerBulletList.HasComponent(collisionEvent.EntityB);

                var isPLayerA = player.HasComponent(collisionEvent.EntityA);
                var isEnemyBulletA = enemyBulletList.HasComponent(collisionEvent.EntityA);
                var isPLayerB = player.HasComponent(collisionEvent.EntityB);
                var isEnemyBulletB = enemyBulletList.HasComponent(collisionEvent.EntityB);

                if (((isBulletA && isEnemyB) || (isBulletB && isEnemyA)) || ((isEnemyBulletA && isPLayerB) || (isEnemyBulletB && isPLayerA)))
                {
                    var DamageAComponent = isTakingDamage[entA];
                    var entBInflictDMG = dmg[entB];
                    DamageAComponent.dmgTaken = entBInflictDMG.dmg;
                    DamageAComponent.takeDamage = true;
                    isTakingDamage[entA] = DamageAComponent;
                    dmg[entB] = entBInflictDMG;

                    var DamageBComponent = isTakingDamage[entB];
                    var entAInflictDMG = dmg[entA];
                    DamageBComponent.dmgTaken = entAInflictDMG.dmg;
                    DamageBComponent.takeDamage = true;
                    isTakingDamage[entB] = DamageBComponent;
                    dmg[entA] = entAInflictDMG;
                }
            }
        }
    }
}