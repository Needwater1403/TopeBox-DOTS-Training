using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HPAuthor : MonoBehaviour
{
    public float hp;
    public class HPBaker : Baker<HPAuthor>
    {
        public override void Bake(HPAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new HP
            {
                hp = authoring.hp,
            });
        }
    }
}
