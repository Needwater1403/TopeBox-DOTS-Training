using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class DamageAuthor : MonoBehaviour
{
    public float dmg;
    public class DamageBaker : Baker<DamageAuthor>
    {
        public override void Bake(DamageAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new Damage
            {
                dmg = authoring.dmg,
            });
        }
    }
}
