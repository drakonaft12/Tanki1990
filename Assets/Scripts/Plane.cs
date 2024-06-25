using System;
using System.Collections.Generic;
using UnityEngine;
using static SåttingPlanå;

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

    public SettingPlaneWWW settingPlaneWWW;

    int valueOfX = 4;
    int valueOfY = 4;
    float _size = 0.25f;
    int _damageArmor = 5;
    bool _isUpdat = false;

    public void Damage(Vector2 pointDamage, Vector2 normal, int damage)
    {
        if (damage >= _damageArmor)
        {
            Vector2 k = pointDamage - normal * _size / 2 - (Vector2)transform.position;
            List<Vector2Int> v = new List<Vector2Int>();
            if (normal.x != 0)
            {
                foreach (var item in localPointBlocks)
                {
                    if (Math.Abs(item.Value.x - k.x) < _size / 1.5f)
                    {
                        if (Math.Abs(item.Value.y - k.y) < _size * 2)
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
                DeleteVox(item.x, item.y);
            }
        }
    }

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<PolygonCollider2D>();


        transform.position = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
    }

    public void Create(SåttingPlanå såtting)
    {
        valueOfX = såtting.sizeVox.x;
        valueOfY = såtting.sizeVox.y;
        _size = såtting.size;
        _renderer.material = såtting.material;
        _damageArmor = såtting.damage;
        var layerValue = såtting.layer.value;
        int layerID = 0;
        for (int i = 0; i < 32; i++)
        {
            if(layerValue%2==1) { layerID = i; break; }
            layerValue = layerValue >> 1;
        }
        gameObject.layer = layerID;
        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        for (int j = 0; j < valueOfY; j++)
        {
            for (int i = 0; i < valueOfX; i++)
            {
                if (såtting.x[i].y[j])
                localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2((i - valueOfX / 2 + 0.5f) * _size, 
                                                                               (j - valueOfY / 2 + 0.5f) * _size));
            }
        }

                PaintMesh(_size);
    }

    public void Create(SettingPlaneWWW såtting)
    {
        settingPlaneWWW = såtting;
        valueOfX = såtting.sizeVoxX;
        valueOfY = såtting.sizeVoxY;
        _size = såtting.size;
        _renderer.material = SåttingPlanå.GetMaterial(såtting.materialID);
        _damageArmor = såtting.damage;
        var layerValue = såtting.layer;
        int layerID = 0;
        for (int i = 0; i < 32; i++)
        {
            if (layerValue % 2 == 1) { layerID = i; break; }
            layerValue = layerValue >> 1;
        }
        gameObject.layer = layerID;
        localPointBlocks = new Dictionary<Vector2Int, Vector2>();

        for (int j = 0; j < valueOfY; j++)
        {
            for (int i = 0; i < valueOfX; i++)
            {
                if (såtting.x[i].y[j])
                    localPointBlocks.Add(new Vector2Int(i + 1, j + 1), new Vector2((i - valueOfX / 2 + 0.5f) * _size,
                                                                                   (j - valueOfY / 2 + 0.5f) * _size));
            }
        }

        PaintMesh(_size);
    }

    private void PaintMesh(float foot)
    {
        _isUpdat = true;
        Mesh mesh = new Mesh();
        vertieces = new List<Vector3>();
        _collider.pathCount = (valueOfY * valueOfY + valueOfX);
        uvs = new List<Vector2>();
        triangles = new List<int>();

        var ix = valueOfX * foot / 2;
        var iy = valueOfY * foot / 2;
        for (int j = 0; j < valueOfY; j++)
        {
            for (int i = 0; i < valueOfX; i++)
            {
                if (!localPointBlocks.TryGetValue(new Vector2Int(i+1, j+1), out var vector))
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

                    _collider.SetPath(index, vertiecesColliders);
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

                    _collider.SetPath(index, vertiecesColliders);
                }
                
            }
        }
        mesh.SetVertices(vertieces);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);

        _filter.mesh = mesh;
        
    }

    private void DeleteVox(int x, int y)
    {
        int i = ((y - 1) * valueOfY + (x - 1)) * 4;
        vertieces[i] = Vector2.zero;
        vertieces[i+1] = Vector2.zero;
        vertieces[i+2] = Vector2.zero;
        vertieces[i+3] = Vector2.zero;

        Vector2[] g = new Vector2[3] { Vector2.zero, Vector2.zero, Vector2.zero };
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
        if(_isUpdat)
        if(localPointBlocks.Count == 0)
        {
                _isUpdat = false;
            gameObject.SetActive(false);
        }

    }


}
