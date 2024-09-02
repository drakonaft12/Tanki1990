using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class PlaneRedactor : MonoBehaviour
{
    SettingPlaneWWW _setting;
    MeshFilter _filter;
    MeshRenderer _renderer;
    Vector3 _position;
    Dictionary<Vector2Int, Vector2> localPointBlocks;

    public SettingPlaneWWW Setting  { get => _setting; }
    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();   
    }
    public void Create(SettingPlane setting) 
    {
        _position = transform.position;
        _setting = setting.ToWWW(_position);
        _renderer.material = setting.material;
        transform.localScale = new Vector3(setting.size*setting.sizeVox.x, setting.size * setting.sizeVox.y, 1);

        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        for (int j = 0; j < setting.sizeVox.y; j++)
        {
            for (int i = 0; i < setting.sizeVox.x; i++)
            {
                if (setting.x[i].y[j])
                    localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2((i - setting.sizeVox.x / 2 + 0.5f) * setting.size,
                                                                                   (j - setting.sizeVox.y / 2 + 0.5f) * setting.size));
            }
        }
        PaintMesh(setting.sizeVox.x, setting.sizeVox.y, setting.size);
    }

    public void UpdatePosition()
    {
        _setting.positionX = (int)transform.position.x;
        _setting.positionY = (int)transform.position.y;
    }

    public void CreateWWW(SettingPlaneWWW setting,Material material)
    {
        _position = transform.position;
        _setting = setting;
        _renderer.material = material;
        transform.localScale = new Vector3(setting.size * setting.sizeVoxX, setting.size * setting.sizeVoxY, 1);

        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        for (int j = 0; j < setting.sizeVoxY; j++)
        {
            for (int i = 0; i < setting.sizeVoxX; i++)
            {
                if (setting.x[i].y[j])
                    localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2((i - setting.sizeVoxX / 2 + 0.5f) * setting.size,
                                                                                   (j - setting.sizeVoxY / 2 + 0.5f) * setting.size));
            }
        }
        PaintMesh(setting.sizeVoxX, setting.sizeVoxY, setting.size);
    }
    private void Update()
    {
        /*_renderer.material = _såtting.material;
        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        for (int j = 0; j < _såtting.sizeVox.y; j++)
        {
            for (int i = 0; i < _såtting.sizeVox.x; i++)
            {
                if (_såtting.x[i].y[j])
                    localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2((i - _såtting.sizeVox.x / 2 + 0.5f) * _såtting.size,
                                                                                   (j - _såtting.sizeVox.y / 2 + 0.5f) * _såtting.size));
            }
        }
        PaintMesh(_såtting.sizeVox.x, _såtting.sizeVox.y, _såtting.size);*/
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

                    //uvs.Add(new Vector2(0.25f * (i % 4), 0.25f * (j % 4)));
                    //uvs.Add(new Vector2(0.25f * (i % 4), 0.25f * (j % 4 + 1)));
                    //uvs.Add(new Vector2(0.25f * (i % 4 + 1), 0.25f * (j % 4 + 1)));
                    //uvs.Add(new Vector2(0.25f * (i % 4 + 1), 0.25f * (j % 4)));

                    uvs.Add(new Vector2(foot * (i % valueOfX), foot * (j % valueOfY)));
                    uvs.Add(new Vector2(foot * (i % valueOfX), foot * (j % valueOfY + 1)));
                    uvs.Add(new Vector2(foot * (i % valueOfX + 1), foot * (j % valueOfY + 1)));
                    uvs.Add(new Vector2(foot * (i % valueOfX + 1), foot * (j % valueOfY)));

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
