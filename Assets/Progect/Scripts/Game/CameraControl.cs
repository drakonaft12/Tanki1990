using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _size = 7;
    private static CameraControl _instance;
    private Vector2 _sizeMap;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _camera = Camera.main;
            _size = PlayerPrefs.GetInt("CameraSize",7);
        }
    }

    private void OnEnable()
    {
        if (_camera?.orthographicSize > _size)
        {
            _camera.orthographicSize = _size;
            _sizeMap = _camera.GetComponent<SpawnMap>().Size / 2;
        }
    }

    private void Update()
    {
        float XV = transform.position.x;
        float YV = transform.position.y;

        if (transform.position.x > _sizeMap.x - _size)
            XV = _sizeMap.x - _size;
        if (transform.position.x < -_sizeMap.x + _size)
            XV = -_sizeMap.x + _size;

        if (transform.position.y > _sizeMap.y - _size)
            YV = _sizeMap.y - _size;
        if (transform.position.y < -_sizeMap.y + _size)
            YV = -_sizeMap.y + _size;

        _camera.transform.position = new Vector3(XV, YV, _camera.transform.position.z);
    }
}
