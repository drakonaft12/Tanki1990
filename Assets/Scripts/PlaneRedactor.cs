using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class PlaneRedactor : MonoBehaviour
{
    S�ttingPlan� _s�tting;
    MeshFilter _filter;
    MeshRenderer _renderer;
    Vector3 _position;
    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();   
    }
    public void Create(S�ttingPlan� s�tting) 
    {
        _s�tting = s�tting;
        _renderer.material = s�tting.material;
        transform.localScale = new Vector3(s�tting.size*s�tting.sizeVox.x, s�tting.size * s�tting.sizeVox.y, 1);
        _position = transform.position;
    }
}
