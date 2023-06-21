using Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class PlayerBulletAuthoring : MonoBehaviour
    {
        public class PlayerBulletBaker : Baker<PlayerBulletAuthoring>
        {
            public override void Bake(PlayerBulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerBulletComponent { });
            }
        }
    }
}
