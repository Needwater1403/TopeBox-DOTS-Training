using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public int num;
        public bool canSpawn;
        public float SpawnSpeed;
        public class EnemySpawnerComponentBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new EnemySpawnerComponent
                    {
                        prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                        num = authoring.num,
                        canSpawn = authoring.canSpawn,
                        spawnSpeed = authoring.SpawnSpeed,
                    });
            }
        }
    }
}
