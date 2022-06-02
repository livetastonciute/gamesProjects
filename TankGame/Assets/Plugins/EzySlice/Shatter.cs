using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Shatter : MonoBehaviour
{
    [Header("Shattering")]
    [Min(0)]
    [Range(1, 5)]
    [SerializeField]
    private Vector2 sliceCount = new Vector2Int(2, 3);

    [Min(0f)]
    [SerializeField]
    private float shatterForceRadius = 5f;

    [Min(0f)]
    [SerializeField]
    private float shatterForce = 300f;

    [SerializeField]
    private bool destroyOnShatter;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onShatter;

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void ShatterGameObject()
    {
        var meshes = SliceMeshes();
        if(meshes.Count == 0)
        {
            return;
        }

        CreateGameObjects(meshes);

        if (destroyOnShatter)
        {
            DestroyCurrentGameObject();
        }

        onShatter.Invoke();
    }
    
    private List<SlicedMesh> SliceMeshes()
    {
        var randomSliceCount = Random.Range(sliceCount.x, sliceCount.y);
        return gameObject.Slice((int)randomSliceCount);
    }

    private void CreateGameObjects(IReadOnlyList<SlicedMesh> meshes)
    {
        var currentTransform = gameObject.transform;
        var currentPosition = currentTransform.position;

        for(var meshIndex = 0; meshIndex < meshes.Count; meshIndex++)
        {
            var newGameObject = CreateGameObject(currentTransform, meshIndex);
            var slicedMesh = meshes[meshIndex];

            AddMeshFilter(newGameObject, slicedMesh.Mesh);
            AddMeshRenderer(newGameObject, slicedMesh.Material);
            AddMeshCollider(newGameObject);

            var newRigidbody = AddRigidbody(newGameObject, meshes.Count);
            AddShatterForce(newRigidbody, currentPosition);
        }
    }

    private GameObject CreateGameObject(Transform currentTransform, int index)
    {
        return new GameObject
        {
            transform =
            {
                localScale = currentTransform.localScale,
                position = currentTransform.position,
                rotation = currentTransform.rotation,
                parent = currentTransform.parent
        },
            name = $"{gameObject.name} ({index})"
        };
    }
    private void AddMeshFilter(GameObject newGameObject, Mesh mesh)
    {
        var newMeshFilter = newGameObject.AddComponent<MeshFilter>();
        newMeshFilter.sharedMesh = mesh;
    }
    private void AddMeshRenderer(GameObject newGameObject, Material material)
    {
        var newMeshRenderer = newGameObject.AddComponent<MeshRenderer>();
        newMeshRenderer.sharedMaterial = material;
    }
    private void AddMeshCollider(GameObject newGameObject)
    {
        var newMeshCollider = newGameObject.AddComponent<MeshCollider>();
        newMeshCollider.convex = true;
    }
    private Rigidbody AddRigidbody(GameObject newGameObject, int meshCount)
    {
        var newRigidbody = newGameObject.AddComponent<Rigidbody>();
        newRigidbody.angularDrag = rigidbody.angularDrag;
        newRigidbody.mass = rigidbody.mass / meshCount;
        newRigidbody.drag = rigidbody.drag;

        return newRigidbody;
    }
    private void AddShatterForce(Rigidbody newRigidbody, Vector3 position)
    {
        newRigidbody.AddExplosionForce(
            shatterForce,
            position,
            shatterForceRadius);
    }

    private void DestroyCurrentGameObject()
    {
        var currentGameObject = gameObject;
        currentGameObject.SetActive(false);
        Destroy(currentGameObject);
    }


}
