using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial struct DestroyedColliedSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (tf, ent) in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<EnemyComponent>().WithEntityAccess())
            {
                foreach (var (tf1, bullet, ent1) in SystemAPI.Query<RefRO<LocalTransform>, RefRW<BulletComponent>>().WithEntityAccess())
                {
                    ecb.SetComponent(ent1, new BulletComponent { minDistance = 1, speed = 3f, direction = new float3(0,0,1f)});
                    //find nearest enemy move to job
                    //var f1 = new float3();
                    //var f2 = new float3();
                    var dist = math.distancesq(tf.ValueRO.Position, tf1.ValueRO.Position);
                    if (dist <= bullet.ValueRO.minDistance)
                    {
                        ecb.AddComponent(ent, new Collied { });
                        ecb.AddComponent(ent1, new Collied { });
                    }
                }
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    //collideResutl{bullet, }
}
