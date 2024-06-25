using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class PlaneRedactor : MonoBehaviour
{
    SettingPlaneWWW _s�tting;
    MeshFilter _filter;
    MeshRenderer _renderer;
    Vector3 _position;
    Dictionary<Vector2Int, Vector2> localPointBlocks;

    public SettingPlaneWWW Setting => _s�tting;
    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();   
    }
    public void Create(S�ttingPlan� s�tting) 
    {
        _position = transform.position;
        _s�tting = s�tting.ToWWW(_position);
        _renderer.material = s�tting.material;
        transform.localScale = new Vector3(s�tting.size*s�tting.sizeVox.x, s�tting.size * s�tting.sizeVox.y, 1);

        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        for (int j = 0; j < s�tting.sizeVox.y; j++)
        {
            for (int i = 0; i < s�tting.sizeVox.x; i++)
            {
                if (s�tting.x[i].y[j])
                    localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2((i - s�tting.sizeVox.x / 2 + 0.5f) * s�tting.size,
                                                                                   (j - s�tting.sizeVox.y / 2 + 0.5f) * s�tting.size));
            }
        }
        PaintMesh(s�tting.sizeVox.x, s�tting.sizeVox.y, s�tting.size);
    }

    public void CreateWWW(SettingPlaneWWW s�tting,Material material)
    {
        _position = transform.position;
        _s�tting = s�tting;
        _renderer.material = material;
        transform.localScale = new Vector3(s�tting.size * s�tting.sizeVoxX, s�tting.size * s�tting.sizeVoxY, 1);

        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        for (int j = 0; j < s�tting.sizeVoxY; j++)
        {
            for (int i = 0; i < s�tting.sizeVoxX; i++)
            {
                if (s�tting.x[i].y[j])
                    localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2((i - s�tting.sizeVoxX / 2 + 0.5f) * s�tting.size,
                                                                                   (j - s�tting.sizeVoxY / 2 + 0.5f) * s�tting.size));
            }
        }
        PaintMesh(s�tting.sizeVoxX, s�tting.sizeVoxY, s�tting.size);
    }
    private void Update()
    {
        /*_renderer.material = _s�tting.material;
        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        for (int j = 0; j < _s�tting.sizeVox.y; j++)
        {
            for (int i = 0; i < _s�tting.sizeVox.x; i++)
            {
                if (_s�tting.x[i].y[j])
                    localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2((i - _s�tting.sizeVox.x / 2 + 0.5f) * _s�tting.size,
                                                                                   (j - _s�tting.sizeVox.y / 2 + 0.5f) * _s�tting.size));
            }
        }
        PaintMesh(_s�tting.sizeVox.x, _s�tting.sizeVox.y, _s�tting.size);*/
    }
    private void PaintMesh(int valueOfX, int valueOfY,  float foot)
    {
        var vertieces = new List<Vector3>();
        var uvs = new List<Vector2>();
        Mesh mesh = new Mesh();
        var triangles = new List<int>();

        var ix = valueOfX * foot / 2;
        var iy = valueOfY * foot / 2;
        for (int j = 0; j < valueOfY; j++)
        {
            for (int i = 0; i < valueOfX; i++)
            {
                if (!localPointBlocks.TryGetValue(new Vector2Int(i + 1, j + 1), out var vector))
                {
                    var vertiecesColliders = new Vector2[3];

                    vertieces.Add(Vector2.zero);
                    vertieces.Add(Vector2.zero);
                    vertieces.Add(Vector2.zero);
                    vertieces.Add(Vector2.zero);

                    vertiecesColliders[0] = Vector2.zero;
                    vertiecesColliders[1] = Vector2.zero;
                    vertiecesColliders[2] = Vector2.zero;

                    uvs.Add(Vector2.zero);
                    uvs.Add(Vector2.zero);
                    uvs.Add(Vector2.zero);
                    uvs.Add(Vector2.zero);

                    int index = (j * valueOfY + i);

                    triangles.Add(index * 4);
                    triangles.Add(1 + index * 4);
                    triangles.Add(2 + index * 4);

                    triangles.Add(index * 4);
                    triangles.Add(2 + index * 4);
                    triangles.Add(3 + index * 4);

                   
                }
                else
                {
                    var vertiecesColliders = new Vector2[4];

                    vertieces.Add(new Vector3(i * foot - ix, j * foot - iy));
                    vertieces.Add(new Vector3(i * foot - ix, foot + j * foot - iy));
                    vertieces.Add(new Vector3(foot + i * foot - ix, foot + j * foot - iy));
                    vertieces.Add(new Vector3(foot + i * foot - ix, j * foot - iy));

                    vertiecesColliders[0] = new Vector2(i * foot - ix, j * foot - iy);
                    vertiecesColliders[1] = new Vector2(i * foot - ix, foot + j * foot - iy);
                    vertiecesColliders[2] = new Vector2(foot + i * foot - ix, foot + j * foot - iy);
                    vertiecesColliders[3] = new Vector2(foot + i * foot - ix, j * foot - iy);

                    uvs.Add(new Vector2(0.25f * (i % 4), 0.25f * (j % 4)));
                    uvs.Add(new Vector2(0.25f * (i % 4), 0.25f * (j % 4 + 1)));
                    uvs.Add(new Vector2(0.25f * (i % 4 + 1), 0.25f * (j % 4 + 1)));
                    uvs.Add(new Vector2(0.25f * (i % 4 + 1), 0.25f * (j % 4)));

                    int index = (j * valueOfY + i);

                    triangles.Add(index * 4);
                    triangles.Add(1 + index * 4);
                    triangles.Add(2 + index * 4);

                    triangles.Add(index * 4);
                    triangles.Add(2 + index * 4);
                    triangles.Add(3 + index * 4);

                    
                }

            }
        }
        mesh.SetVertices(vertieces);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);

        _filter.mesh = mesh;

    }
}
