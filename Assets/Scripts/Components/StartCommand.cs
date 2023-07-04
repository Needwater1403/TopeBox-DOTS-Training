using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using CortexDeveloper.ECSMessages.Components;

public struct StartCommand : IComponentData, IMessageComponent
{
    public bool startGame { get; set; }
}
