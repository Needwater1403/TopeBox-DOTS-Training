using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public partial struct EnemySpawnerComponent : IComponentData
    {
        public Entity prefab;
        public int num;
        public bool canSpawn;
        public float spawnSpeed;
        public float lastSpawnedTime;
    }
}
