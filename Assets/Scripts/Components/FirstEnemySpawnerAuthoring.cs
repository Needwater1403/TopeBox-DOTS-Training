using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class FirstEnemySpawnerAuthoring : MonoBehaviour
    {
        public class FirstEnemySpawnerBaker : Baker<FirstEnemySpawnerAuthoring>
        {
            public override void Bake(FirstEnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new FirstEnemySpawnerComponent());
            }
        }
    }
}