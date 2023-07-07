using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using CortexDeveloper.ECSMessages.Components;

public struct SpawnBossCommand : IComponentData, IMessageComponent
{
    public bool spawn;
}
