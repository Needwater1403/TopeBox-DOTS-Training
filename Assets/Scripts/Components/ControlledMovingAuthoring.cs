using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class ControlledMovingAuthoring : MonoBehaviour
    {
        public class ControlledMovingComponentBaker : Baker<ControlledMovingAuthoring>
        {
            public override void Bake(ControlledMovingAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ControlledMovingComponent());
            }
        }
    }
}