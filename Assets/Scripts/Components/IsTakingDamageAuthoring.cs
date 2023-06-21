using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class IsTakingDamageAuthoring : MonoBehaviour
    {
        public bool takeDamage;
        public float dmgTaken;
        public class IsTakingDamageBaker : Baker<IsTakingDamageAuthoring>
        {
            public override void Bake(IsTakingDamageAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new IsTakingDamage 
                {
                    takeDamage = authoring.takeDamage,
                    dmgTaken = authoring.dmgTaken,
                });
            }
        }
    }
}
