using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


class CameraRotationController
{

    private Transform _camera;
    private float _radius;
    private float _speed;

    private float _angle = 0f;

    public CameraRotationController(Transform camera, float radius, float rotationSpeed)
    {

        _camera = camera;
        _radius = radius;
        _speed = rotationSpeed;

    }

    public void RotateAround(Vector3 position, float angle)
    {

        _angle += angle * _speed;

        _angle %= Mathf.PI * 2;

        var x = position.x + Mathf.Cos(_angle) * _radius;
        var z = position.z + Mathf.Sin(_angle) * _radius;

        _camera.position = new Vector3(x, position.y, z);
        _camera.rotation = Quaternion.Euler(0, -(_angle * 180 / Mathf.PI) - 90, 0);

    }

}

