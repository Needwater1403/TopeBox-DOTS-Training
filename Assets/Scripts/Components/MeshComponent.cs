using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Physics;
using UnityEngine.Rendering;

public struct MeshComponent : IComponentData
{
    public BatchMaterialID materialID;
    public BatchMeshID meshID;
}
