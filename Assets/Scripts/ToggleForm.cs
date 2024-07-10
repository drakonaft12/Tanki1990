using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(Toggle))]
public class ToggleForm : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] Vector2Int vector;
    [SerializeField] Redactor redactor;

    public Redactor Redactor {  get { return redactor; } set {  redactor = value; } }

    public Vector2Int Vector { get => vector; set => vector = value; }

    public void Click()
    {
        redactor.ButtonForm(vector, toggle.isOn);
    }

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        
    }
    private void Start()
    {
        redactor.ButtonForm(vector, toggle.isOn);
        transform.localScale = Vector3.one;
    }


}
