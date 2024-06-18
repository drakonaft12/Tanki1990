using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Units/PlanеSettings")]
public class SеttingPlanе : ScriptableObject
{
    public Material material;
    public int damage = 5;
    public Vector2Int sizeVox = new Vector2Int(4,4);
    public Y[] x;
    public float size = 0.25f;
    public LayerMask layer;

    public SettingPlaneWWW ToWWW()
    {
        SettingPlaneWWW setting = new SettingPlaneWWW();
        setting.material = material;
        setting.damage = damage;
        setting.sizeVox = sizeVox;
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
    public Material material;
    public int damage;
    public Vector2Int sizeVox;
    public Y[] x;
    public float size;
    public LayerMask layer;

}
