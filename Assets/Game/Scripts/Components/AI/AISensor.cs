using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

[ExecuteInEditMode]
public class AISensor : MonoBehaviour
{
    [SerializeField] private float _distance = 10;
    [SerializeField] private float _angle = 30;
    [SerializeField] private float _height = 1.0f;
    [SerializeField] private Color _meshColor = Color.red;

    [SerializeField] private int _scanFrequency = 30;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private LayerMask _occlusionLayers;

    public List<GameObject> Objects
    {
        get
        {
            _objects.RemoveAll(obj => !obj);
            return _objects;
        }
        private set
        {
        }
    }
    private List<GameObject> _objects = new List<GameObject>();

    private Collider[] _colliders = new Collider[50];
    private int _count;
    private float _scanInterval;
    private float _scanTimer;
    private Mesh _mesh;

    private void Start()
    {
        _scanInterval = 1.0f / _scanFrequency;
    }
    private void Update()
    {
        _scanTimer -= Time.deltaTime;
        if (_scanTimer < 0)
        {
            _scanTimer += _scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        _count = Physics.OverlapSphereNonAlloc
            (transform.position, _distance, _colliders, _layers, QueryTriggerInteraction.Collide);

        _objects.Clear();
        for (int i = 0; i < _count; i++)
        {
            var obj = _colliders[i].gameObject;
            if (IsInSight(obj))
            {
                _objects.Add(obj);
            }
        }
    }
    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if (direction.y < 0 || direction.y > _height)
            return false;

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > _angle)
            return false;

        origin.y += _height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, _occlusionLayers))
            return false;
        return true;
    }

    private Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int trianglesCount = (segments * 4) + 4;
        int verticesCount = trianglesCount * 3;

        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[verticesCount];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -_angle, 0) * Vector3.forward * _distance;
        Vector3 bottomRight = Quaternion.Euler(0, _angle, 0) * Vector3.forward * _distance;

        Vector3 topCenter = bottomCenter + Vector3.up * _height;
        Vector3 topRight = bottomRight + Vector3.up * _height;
        Vector3 topLeft = bottomLeft + Vector3.up * _height;

        int vertCounter = 0;
        //left
        vertices[vertCounter++] = bottomCenter;
        vertices[vertCounter++] = bottomLeft;
        vertices[vertCounter++] = topLeft;

        vertices[vertCounter++] = topLeft;
        vertices[vertCounter++] = topCenter;
        vertices[vertCounter++] = bottomCenter;

        //right
        vertices[vertCounter++] = bottomCenter;
        vertices[vertCounter++] = topCenter;
        vertices[vertCounter++] = topRight;

        vertices[vertCounter++] = topRight;
        vertices[vertCounter++] = bottomRight;
        vertices[vertCounter++] = bottomCenter;

        float currentAngle = -_angle;
        float deltaAngle = (_angle * 2) / segments;
        for (int i = 0; i < segments; ++i)
        {

            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * _distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * _distance;

            topRight = bottomRight + Vector3.up * _height;
            topLeft = bottomLeft + Vector3.up * _height;

            //far side
            vertices[vertCounter++] = bottomLeft;
            vertices[vertCounter++] = bottomRight;
            vertices[vertCounter++] = topRight;

            vertices[vertCounter++] = topRight;
            vertices[vertCounter++] = topLeft;
            vertices[vertCounter++] = bottomLeft;

            //top
            vertices[vertCounter++] = topCenter;
            vertices[vertCounter++] = topLeft;
            vertices[vertCounter++] = topRight;
            //bottom
            vertices[vertCounter++] = bottomCenter;
            vertices[vertCounter++] = bottomRight;
            vertices[vertCounter++] = bottomLeft;

            currentAngle += deltaAngle;
        }


        for (int i = 0; i < verticesCount; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
    private void OnValidate()
    {
        _mesh = CreateWedgeMesh();
        _scanInterval = 1.0f / _scanFrequency;
    }
    private void OnDrawGizmos()
    {
        if (_mesh)
        {
            Gizmos.color = _meshColor;
            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
        }

        Gizmos.color = Color.green;
        foreach (var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }

    public int Filter(GameObject[] buffer, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        int count = 0;
        foreach (var obj in Objects)
        {
            if (obj.layer == layer)
            {
                buffer[count++] = obj;
            }
            if (buffer.Length == count)
            {
                break;
            }
        }
        return count;
    }
}

