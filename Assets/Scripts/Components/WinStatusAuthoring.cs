using Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class WinStatusAuthoring : MonoBehaviour
{
    public bool isWin;
    public class WinStatusBaker : Baker<WinStatusAuthoring>
    {
        public override void Bake(WinStatusAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(new WinStatus
            {
                isWin = authoring.isWin,
            });
        }
    }
}
