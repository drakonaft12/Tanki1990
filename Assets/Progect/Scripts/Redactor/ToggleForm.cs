using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(Toggle))]
public class ToggleForm : MonoBehaviour
{
    private Toggle toggle;
    private Vector2Int vector;
    public SettingPlane _såtting;

    public Vector2Int Vector { get => vector; set => vector = value; }

    public bool IsOn
    {
        get
        {
            return toggle.isOn;
        }
        set
        {
            toggle.isOn = value;
        }
    }

    public void Click()
    {
        _såtting.x[vector.x].y[vector.y] = toggle.isOn;
    }

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        
    }
    private void Start()
    {
        _såtting.x[vector.x].y[vector.y] = toggle.isOn;
        transform.localScale = Vector3.one;
    }


}
