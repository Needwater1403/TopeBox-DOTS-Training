using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public partial struct PlayerSpawnerComponent : IComponentData
    {
        public Entity prefab;
        public bool canSpawn;
    }
}
