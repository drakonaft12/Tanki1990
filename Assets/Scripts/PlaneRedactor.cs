using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class PlaneRedactor : MonoBehaviour
{
    SåttingPlanå _såtting;
    MeshFilter _filter;
    MeshRenderer _renderer;
    Vector3 _position;
    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();   
    }
    public void Create(SåttingPlanå såtting) 
    {
        _såtting = såtting;
        _renderer.material = såtting.material;
        transform.localScale = new Vector3(såtting.size*såtting.sizeVox.x, såtting.size * såtting.sizeVox.y, 1);
        _position = transform.position;
    }
}
