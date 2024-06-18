using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redactor : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    [SerializeField] SåttingPlanå _såtting;
    PlaneRedactor[][] planes;
    [SerializeField] Vector2Int size;
    public SåttingPlanå SåtSåttingPlanå { get => _såtting; }
    

    void Start()
    {
        planes = new PlaneRedactor[size.x][];
        for (int i = 0; i < size.x; i++)
        {
            planes[i] = new PlaneRedactor[size.y];
        }
        transform.localScale = (Vector3Int)size;
    }

    public void ButtonForm(Vector2Int vector, bool b)
    {
            _såtting.x[vector.x].y[vector.y] = b;
    }

    public void SetSettingToPaint(Material material, int damage, LayerMask layer)
    {
        _såtting.material = material;
        _såtting.damage = damage;
        _såtting.layer = layer;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            var v = Vector3Int.CeilToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition) - Vector3.one * 0.5f);
            v -= Vector3Int.forward * v.z;
            Vector3Int vector = Vector3Int.right * (size.x / 2) + Vector3Int.up * (size.y / 2) + v;
            if (vector.x >= 0 && vector.y >= 0 && vector.x < size.x && vector.y < size.y)
            {
                if (planes[vector.x][vector.y] == null)
                {
                    planes[vector.x][vector.y] = _spawner.Spawn<PlaneRedactor>(0, v);
                    planes[vector.x][vector.y].Create(_såtting);
                }
                else
                {
                    planes[vector.x][vector.y].gameObject.SetActive(false);
                    planes[vector.x][vector.y] = _spawner.Spawn<PlaneRedactor>(0, v);
                    planes[vector.x][vector.y].Create(_såtting);
                }
            }
        }
    }
}
