using Components;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class MeshAuthoring : MonoBehaviour
{
    public Material material;
    public Mesh mesh;
    public class MeshComponentBaker : Baker<MeshAuthoring>
    {
        public override void Bake(MeshAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var hybridRender = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<EntitiesGraphicsSystem>();
            AddComponent(entity, new MeshComponent
            {
                materialID = hybridRender.RegisterMaterial(authoring.material),
                meshID = hybridRender.RegisterMesh(authoring.mesh),
            });
        }
    }
}
