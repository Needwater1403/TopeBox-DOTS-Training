using Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ControlledMovingAuthoring : MonoBehaviour
{
    public class ColliedComponentBaker : Baker<ControlledMovingAuthoring>
    {
        public override void Bake(ControlledMovingAuthoring authoring)
        {
            
        }
    }
}
