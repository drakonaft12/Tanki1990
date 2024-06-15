using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redactor : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    [SerializeField] SåttingPlanå _såtting;
    PlaneRedactor[][] planes;
    [SerializeField] Vector2Int size;
    // Start is called before the first frame update
    void Start()
    {
        planes = new PlaneRedactor[size.x][];
        for (int i = 0; i < size.x; i++)
        {
            planes[i] = new PlaneRedactor[size.y];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {

            var v = Vector3Int.CeilToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition)- Vector3.one*0.5f);
            v -= Vector3Int.forward * v.z;
            Vector3Int vector = Vector3Int.right * (size.x / 2) + Vector3Int.up * (size.y / 2) + v;
            if (vector.x >= 0 && vector.y>=0&& vector.x<size.x && vector.y< size.y&& planes[vector.x][vector.y]==null) 
            {
                
                var itm = _spawner.Spawn(0, v);
                planes[vector.x][vector.y]=itm.GetComponent<PlaneRedactor>();
                planes[vector.x][vector.y].Create(_såtting); 
            }
        }
    }
}
