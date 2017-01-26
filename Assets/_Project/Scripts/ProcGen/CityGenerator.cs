using System;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public Material Material;
    public CityGenStrategy Strategy;

    public void Generate()
    {
        ClearMeshes();
        BuildMesh();
    }

    private void BuildMesh()
    {
        var child = new GameObject("city_part_1");

        try
        {
            var filter = child.AddComponent<MeshFilter>();
            var renderer = child.AddComponent<MeshRenderer>();
            var mesh = new Mesh();
            mesh.name = "mesh_1";
            var meshData = Strategy.Generate();
            meshData.ApplyTo(mesh);
            renderer.sharedMaterial = Material;
            filter.sharedMesh = mesh;
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }

        child.transform.parent = transform;
    }

    private void ClearMeshes()
    {
        for(var i = 0; i < transform.childCount; i++)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }
}