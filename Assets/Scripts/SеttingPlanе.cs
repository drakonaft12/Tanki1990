using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Units/PlanеSettings")]
public class SеttingPlanе : ScriptableObject
{
    public Material material;
    public int damage = 5;
    public Vector2Int sizeVox = new Vector2Int(4, 4);
    public Y[] x;
    public float size = 0.25f;
    public LayerMask layer;

    [SerializeField] Diction<int, Material> materials;

    public Material GetMaterial(int id)
    {
        materials.returnDictionary().TryGetValue(id, out var material);
        return material;
    }
    public SettingPlaneWWW ToWWW(Vector2 position)
    {
        SettingPlaneWWW setting = new SettingPlaneWWW();
        setting.positionX = (int)position.x;
        setting.positionY = (int)position.y;
        foreach (var item in materials.returnDictionary())
        {
            if(item.Value == material)
            {
                setting.materialID = item.Key;
            }
        }

        setting.damage = damage;
        setting.sizeVoxX = sizeVox.x;
        setting.sizeVoxY = sizeVox.y;
        setting.layer = layer;
        setting.size = size;
        setting.x = new Y[sizeVox.x];
        for (int i = 0; i < sizeVox.x; i++)
        {
            setting.x[i].y = new bool[sizeVox.y];
            for (int j = 0; j < sizeVox.y; j++)
            {
                setting.x[i].y[j] = x[i].y[j];
            }
        }
        return setting;
    }

    int _x, _y;

    private void OnEnable()
    {
        x = new Y[sizeVox.x];
        for (int i = 0; i < sizeVox.x; i++)
        {
            x[i].y = new bool[sizeVox.y];
            for (int j = 0; j < sizeVox.y; j++)
            {
                x[i].y[j] = true;
            }
        }
        _x = sizeVox.x;
        _y = sizeVox.y;
    }
    private void OnValidate()
    {
        if ((_x != sizeVox.x || _y != sizeVox.y))
        {
            x = new Y[sizeVox.x];
            for (int i = 0; i < sizeVox.x; i++)
            {
                x[i].y = new bool[sizeVox.y];
                for (int j = 0; j < sizeVox.y; j++)
                {
                    x[i].y[j] = true;
                }
            }
            _x = sizeVox.x;
            _y = sizeVox.y;
        }
    }
}
[Serializable]
public struct Y
{
    public bool[] y;

}

[Serializable]
public struct SettingPlaneWWW
{
    public int positionX;
    public int positionY;
    public int materialID;
    public int damage;
    public int sizeVoxX;
    public int sizeVoxY;
    public Y[] x;
    public float size;
    public int layer;

}
