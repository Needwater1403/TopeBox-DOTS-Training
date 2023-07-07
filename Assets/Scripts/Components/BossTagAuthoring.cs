using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class BossTagAuthoring : MonoBehaviour
    {
        public class BossTagComponentBaker : Baker<BossTagAuthoring>
        {
            public override void Bake(BossTagAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BossTagComponent());
            }
        }
    }
}