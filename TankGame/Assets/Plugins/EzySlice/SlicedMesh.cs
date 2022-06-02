using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedMesh
{
    public Material Material { get; }
    public Mesh Mesh { get; }
    public SlicedMesh(Material material, Mesh mesh)
    {
        Material = material;
        Mesh = mesh;
    }
}
