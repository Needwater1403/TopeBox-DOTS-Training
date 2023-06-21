using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Components;

public class DestroyAuthor : MonoBehaviour
{
    public class DestroyBaker : Baker<DestroyAuthor>
    {
        public override void Bake(DestroyAuthor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new DestroyComponent
            {
            });
        }
    }
}
