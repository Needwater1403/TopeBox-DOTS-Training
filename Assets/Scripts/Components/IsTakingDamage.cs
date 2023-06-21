using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    public partial struct IsTakingDamage : IComponentData
    {
        public bool takeDamage;
        public float dmgTaken;
    }
}