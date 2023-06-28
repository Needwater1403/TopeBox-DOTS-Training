using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    public partial struct BulletComponent:IComponentData
    {
        public float speed;
        public float range;

        public float3 direction;

    }
}