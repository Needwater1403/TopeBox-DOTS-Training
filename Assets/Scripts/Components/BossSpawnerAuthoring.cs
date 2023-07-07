using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public class BossSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public bool canSpawn = true;
        public class BossSpawnerComponentBaker : Baker<BossSpawnerAuthoring>
        {
            public override void Bake(BossSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new BossSpawnerComponent
                    {
                        prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                        canSpawn = authoring.canSpawn,
                    });
            }
        }
    }
}
