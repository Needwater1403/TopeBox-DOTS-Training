using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public class PlayerSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public bool canSpawn;
        public class PlayerSpawnerComponentBaker : Baker<PlayerSpawnerAuthoring>
        {
            public override void Bake(PlayerSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new PlayerSpawnerComponent
                    {
                        prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                        canSpawn = authoring.canSpawn,
                    });
            }
        }
    }
}