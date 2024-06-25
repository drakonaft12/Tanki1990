using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutGroup))]
public class LayoutGroupToggl : MonoBehaviour
{
    LayoutGroup layout;
    [SerializeField] Spawner spawner;
    [SerializeField] Redactor redactor;
    List<ToggleForm> toggleForms = new List<ToggleForm>();
    Vector2Int vector;

    private void Awake()
    {
        layout = GetComponent<LayoutGroup>();
    }
    void Start()
    {
        vector = redactor.SåtSåttingPlanå.sizeVox;
        for (int i = 0; i < vector.y; i++)
        {
            for (int j = 0; j < vector.x; j++)
            {
                AddToggle(Vector2Int.right * i + Vector2Int.up * j);
            }
        }
        (layout as GridLayoutGroup).cellSize = new Vector2((transform as RectTransform).sizeDelta.x / vector.x, (transform as RectTransform).sizeDelta.y / vector.y);
        (layout as GridLayoutGroup).constraintCount = vector.x;
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
        tog.Redactor = redactor;
        toggleForms.Add(tog);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (vector != redactor.SåtSåttingPlanå.sizeVox)
        {
            DeleteAllToggle();
            vector = redactor.SåtSåttingPlanå.sizeVox;
            (layout as GridLayoutGroup).cellSize = new Vector2(280 / vector.x - 20, 280 / vector.y - 20);
            (layout as GridLayoutGroup).constraintCount = vector.x;
            for (int i = 0; i < vector.y; i++)
            {
                for (int j = 0; j < vector.x; j++)
                {
                    AddToggle(Vector2Int.right * i + Vector2Int.up * j);
                }
            }
        }
    }
}
