using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Plane))]
public class PlanTst : MonoBehaviour
{
    [SerializeField] S�ttingPlan� s�tting;
    Plane plane;
    private void Start()
    {
        plane = GetComponent<Plane>();
        //plane.Create(s�tting);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
