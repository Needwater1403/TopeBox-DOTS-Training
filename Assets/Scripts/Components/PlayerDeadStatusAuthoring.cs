using Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerDeadStatusAuthoring : MonoBehaviour
{
    public bool isDead;
    public class PlayerDeadStatusBaker : Baker<PlayerDeadStatusAuthoring>
    {
        public override void Bake(PlayerDeadStatusAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new PlayerDeadStatus
            {
                isDead = authoring.isDead,
            });
        }
    }
}
