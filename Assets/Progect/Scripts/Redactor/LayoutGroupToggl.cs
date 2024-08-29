using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LayoutGroupToggl : MonoBehaviour
{
    private GridLayoutGroup layout;
    [SerializeField] private Spawner spawner;
    [SerializeField] private SettingPlane _s�tting;
    [SerializeField] private Button buttonRewers;
    private RectTransform rectTransform;
    private List<ToggleForm> toggleForms = new List<ToggleForm>();
    private Vector2Int vector;
    private Vector2 sizeDeltaR;

    private void Awake()
    {
        layout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        buttonRewers.onClick.AddListener(ReversToggle);
        sizeDeltaR = rectTransform.sizeDelta;
    }
    void Start()
    {
        vector = _s�tting.sizeVox;
        for (int i = 0; i < vector.y; i++)
        {
            for (int j = 0; j < vector.x; j++)
            {
                AddToggle(Vector2Int.right * i + Vector2Int.up * j);
            }
        }
        layout.cellSize = new Vector2(sizeDeltaR.x / vector.x, sizeDeltaR.y / vector.y);
        layout.constraintCount = vector.x;
    }
    private void DeleteAllToggle()
    {
        var y = toggleForms.Count;
        for (int i = 0; i < y; i++)
        {
            toggleForms[0].gameObject.SetActive(false);
            toggleForms.RemoveAt(0);
        }
    }
    private void AddToggle(Vector2Int position)
    {
        var tog = spawner.Spawn<ToggleForm>(1, transform.position);
        tog.transform.SetParent(transform);
        tog.Vector = position;
        tog._s�tting = _s�tting;
        toggleForms.Add(tog);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (vector != _s�tting.sizeVox)
        {
            DeleteAllToggle();
            vector = _s�tting.sizeVox;
            _s�tting.UpdateValid();
            layout.cellSize = new Vector2(sizeDeltaR.x / vector.x, sizeDeltaR.y / vector.y);
            layout.constraintCount = vector.x;
            for (int i = 0; i < vector.y; i++)
            {
                for (int j = 0; j < vector.x; j++)
                {
                    AddToggle(Vector2Int.right * i + Vector2Int.up * j);
                }
            }
        }
    }

    public void ReversToggle()
    {
        foreach (var item in toggleForms)
        {
            item.IsOn = !item.IsOn;
        }
    }
}
