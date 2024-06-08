using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(PolygonCollider2D))]
public class Plane : MonoBehaviour, IDamaget
{
    MeshFilter _filter;
    MeshRenderer _renderer;
    PolygonCollider2D _collider;

    List<Vector3> vertieces;
    
    List<Vector2> uvs;
    List<int> triangles;
    Dictionary<Vector2Int, Vector2> localPointBlocks;

    int valueOfX = 4;
    int valueOfY = 4;
    float _size = 0.25f;

    public void Damage(Vector2 pointDamage, Vector2 normal, int damage)
    {
        Vector2 k = pointDamage - normal * _size / 2 - (Vector2)transform.position;
        List<Vector2Int> v = new List<Vector2Int>();
        if (normal.x != 0)
        {
            foreach (var item in localPointBlocks)
            {
                if(Math.Abs( item.Value.x - k.x)< _size / 1.5f)
                {
                    if(Math.Abs(item.Value.y - k.y)< _size * 2)
                    {
                        v.Add(item.Key);
                    }
                }
            }

        }
        else
        {
            foreach (var item in localPointBlocks)
            {
                if (Math.Abs(item.Value.y - k.y) < _size / 1.5f)
                {
                    if (Math.Abs(item.Value.x - k.x) < _size * 2)
                    {
                        v.Add(item.Key);
                    }
                }
            }
        }

        
        foreach (var item in v)
        {
            Debug.Log(item);
            DeletePix(item.x, item.y);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<PolygonCollider2D>();

        PaintMesh(_size);
    }

    private void PaintMesh(float foot)
    {
        Mesh mesh = new Mesh();
        vertieces = new List<Vector3>();
        _collider.pathCount = (valueOfY * valueOfY + valueOfX);
        uvs = new List<Vector2>();
        triangles = new List<int>();
        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        var ix = valueOfX * foot / 2;
        var iy = valueOfY * foot / 2;

        for (int j = 0; j < valueOfY; j++)
        {
            for (int i = 0; i < valueOfX; i++)
            {
                var vertiecesColliders = new Vector2[4];

                vertieces.Add(new Vector3(i * foot - ix, j * foot - iy));
                vertieces.Add(new Vector3(i * foot - ix, foot + j * foot - iy));
                vertieces.Add(new Vector3(foot + i * foot - ix, foot + j * foot - iy));
                vertieces.Add(new Vector3(foot + i * foot - ix, j * foot - iy));

                vertiecesColliders[0]= new Vector2(i * foot - ix, j * foot - iy);
                vertiecesColliders[1] = new Vector2(i * foot - ix, foot + j * foot - iy);
                vertiecesColliders[2] = new Vector2(foot + i * foot - ix, foot + j * foot - iy);
                vertiecesColliders[3] = new Vector2(foot + i * foot - ix, j * foot - iy);

                uvs.Add(new Vector2(0.5f * (i % 2), 0.5f * (j % 2)));
                uvs.Add(new Vector2(0.5f * (i % 2), 0.5f * (j % 2 + 1)));
                uvs.Add(new Vector2(0.5f * (i % 2 + 1), 0.5f * (j % 2 + 1)));
                uvs.Add(new Vector2(0.5f * (i % 2 + 1), 0.5f * (j % 2)));

                int index = (j * valueOfY + i);

                triangles.Add(index * 4);
                triangles.Add(1 + index * 4);
                triangles.Add(2 + index * 4);

                triangles.Add(index * 4);
                triangles.Add(2 + index * 4);
                triangles.Add(3 + index * 4);

                localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2(i * foot - ix + foot / 2, j * foot - iy + foot / 2));

                _collider.SetPath(index, vertiecesColliders);
            }
        }
        mesh.SetVertices(vertieces);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);

        _filter.mesh = mesh;
        
    }

    private void DeletePix(int x, int y)
    {
        int i = ((y - 1) * valueOfY + (x - 1)) * 4;
        vertieces[i] = Vector2.zero;
        vertieces[i+1] = Vector2.zero;
        vertieces[i+2] = Vector2.zero;
        vertieces[i+3] = Vector2.zero;

        Vector2[] g = new Vector2[3] {new Vector2(), new Vector2(), new Vector2() };
        _collider.SetPath(i / 4, g);

        uvs[i] = Vector2.zero;
        uvs[i+1] = Vector2.zero;
        uvs[i+2] = Vector2.zero;
        uvs[i+3] = Vector2.zero;

        _filter.mesh.SetVertices(vertieces);
        _filter.mesh.SetUVs(0, uvs);
  
        localPointBlocks.Remove(new Vector2Int(x, y));
    }

    // Update is called once per frame
    void Update()
    {
        if(localPointBlocks.Count == 0)
        {
            gameObject.SetActive(false);
        }

    }


}
