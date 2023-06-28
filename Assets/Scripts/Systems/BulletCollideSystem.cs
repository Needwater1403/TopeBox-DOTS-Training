using Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public partial struct BulletCollideSystem:ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            //return;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (collied, entity) in SystemAPI.Query<RefRO<Collied>>().WithEntityAccess())
            {
                ecb.DestroyEntity(entity);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
        
    }
    
    //collideResutl{bullet, }
}