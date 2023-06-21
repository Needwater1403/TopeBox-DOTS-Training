using System.Collections;
using System.Collections.Generic;
using Unity.Entities;

public partial struct PlayerDeadStatus : IComponentData
{
    public bool isDead;
}
