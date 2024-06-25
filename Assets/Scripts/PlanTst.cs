using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Plane))]
public class PlanTst : MonoBehaviour
{
    [SerializeField] SåttingPlanå såtting;
    Plane plane;
    private void Start()
    {
        plane = GetComponent<Plane>();
        plane.Create(såtting);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
