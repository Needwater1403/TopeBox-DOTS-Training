using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using CortexDeveloper.ECSMessages.Components;

public struct ResetCommand : IComponentData, IMessageComponent
{
    public bool resetGame;
}
