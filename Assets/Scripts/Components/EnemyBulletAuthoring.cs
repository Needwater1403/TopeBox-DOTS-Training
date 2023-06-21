using Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class EnemyBulletAuthoring : MonoBehaviour
    {
        public class EnemyBulletBaker : Baker<EnemyBulletAuthoring>
        {
            public override void Bake(EnemyBulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyBulletComponent { });
            }
        }
    }
}
