using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public class MovingAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 3f;
        public float3 dir;
        public class MovingComponentBaker : Baker<MovingAuthoring>
        {
            public override void Bake(MovingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MovingComponent { moveSpeed = authoring.MoveSpeed, dir = authoring.dir });
            }
        }
    }
}