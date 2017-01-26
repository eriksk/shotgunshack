using System;
using Assets._Project.Scripts.Meshes;
using UnityEngine;

[CreateAssetMenu(menuName="Cutom/ProcGen/City Strategy")]
public class CityGenStrategy : ScriptableObject
{
    public int Buildings = 3;

    public float UnitSize = 1f;

    public Vector2 Width;
    public Vector2 Height;

    public Rect Area;

    public virtual MeshData Generate()
    {
        var meshData = new MeshData();

        var index = 0;

        for(var i = 0; i < Buildings; i++)
        {
            var width = (int)UnityEngine.Random.Range(Width.x, Width.y);
            var height = (int)UnityEngine.Random.Range(Height.x, Height.y);

            var position = new Vector3(
                UnityEngine.Random.Range(Area.xMin, Area.xMax),
                0,
                UnityEngine.Random.Range(Area.yMin, Area.yMax)
            );
            index = AddBuilding(meshData, position, new Vector2(width, height), index);
        }

        return meshData;
    }

    private int AddBuilding(MeshData meshData, Vector3 position, Vector2 size, int index)
    {
        var halfSize = size * 0.5f;
        var segmentSize = UnitSize;
        var segments = (int)(size.y / segmentSize);

        var partSize = new Vector2(size.x, segmentSize);

        for(var i = 0; i < segments; i++)
        {
            var offset = new Vector3(
                0f, 
                (segmentSize * i) + (segmentSize / 2f), 
                0f);

            index = AddQuad(meshData, position + offset + (Vector3.forward * halfSize.x), partSize, Vector3.forward, index);
            index = AddQuad(meshData, position + offset + (Vector3.back * halfSize.x), partSize, Vector3.back, index);
            index = AddQuad(meshData, position + offset + (Vector3.left * halfSize.x), partSize, Vector3.left, index);
            index = AddQuad(meshData, position + offset + (Vector3.right * halfSize.x), partSize, Vector3.right, index);
        }

        position.y += halfSize.y;

        // Roof
        index = AddQuad(meshData, position + (Vector3.up * halfSize.y), new Vector3(size.x, size.x, 0f), Vector3.up, index);


        return index;
    }

    private int AddQuad(MeshData meshData, Vector3 position, Vector2 size, Vector3 direction, int index)
    {
        var halfSize = size * 0.5f;
        var topLeft = new Vector3(-halfSize.x, halfSize.y, 0f);
        var bottomLeft = new Vector3(-halfSize.x, -halfSize.y, 0f);
        var bottomRight = new Vector3(halfSize.x, -halfSize.y, 0f);
        var topRight = new Vector3(halfSize.x, halfSize.y, 0f);

        var rotation = Quaternion.LookRotation(direction, Vector3.up);

        var vertices = new Vector3[]
        {
            position + (rotation * topLeft),
            position + (rotation * bottomLeft),
            position + (rotation * bottomRight),
            position + (rotation * topRight),
        };

        meshData.Vertices.AddRange(vertices);

        meshData.Indices.Add(index + 0);
        meshData.Indices.Add(index + 1);
        meshData.Indices.Add(index + 2);
        meshData.Indices.Add(index + 2);
        meshData.Indices.Add(index + 3);
        meshData.Indices.Add(index + 0);

        meshData.Uvs.Add(new Vector2(0, 0));
        meshData.Uvs.Add(new Vector2(0, 1));
        meshData.Uvs.Add(new Vector2(1, 1));
        meshData.Uvs.Add(new Vector2(1, 0));

        return index + 4;
    }
}