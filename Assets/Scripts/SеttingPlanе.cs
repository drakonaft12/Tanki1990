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
    [Serializable]
    public struct Y
    {
        public bool[] y;
        
    }
}
