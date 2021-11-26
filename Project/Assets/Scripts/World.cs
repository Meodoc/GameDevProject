using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    // Reference to the Chunk Prefab. Drag a Prefab into this field in the Inspector.
    [SerializeField] 
    private GameObject myPrefab;
    private PhysicMaterial worldMaterial;
    [SerializeField]
    private int size;

    private GameObject[,] chunks;

    void Awake()
    {
        worldMaterial = new PhysicMaterial
        {
            staticFriction = 0f,
            dynamicFriction = 0f,
            bounciness = 0f,
            frictionCombine = PhysicMaterialCombine.Minimum,
            bounceCombine = PhysicMaterialCombine.Minimum
        };
    }

    public void DestroyBlock(Vector3 worldCoordinate)
    {
        Vector3 chunkCoordinate = new Vector3(worldCoordinate.x / 8, 1, worldCoordinate.z / 8);
        Vector3 localCoordinate = worldCoordinate - chunkCoordinate;
        Debug.Log("chunkcoord" + chunkCoordinate);
        Debug.Log("localcoord" + localCoordinate);
        Debug.Log("x:" + Mathf.FloorToInt(chunkCoordinate.x));
        Debug.Log("z:" + Mathf.FloorToInt(chunkCoordinate.z));
        int chunkX = Mathf.FloorToInt(chunkCoordinate.x);
        int chunkZ = Mathf.FloorToInt(chunkCoordinate.z);
        chunks[chunkX, chunkZ].GetComponent<Chunk>().DestroyBlock(localCoordinate);

        // Update Mesh Collider
        MeshCollider mc = chunks[chunkX, chunkZ].GetComponent<MeshCollider>();
        Destroy(mc);
        addMeshCollider(chunkX, chunkZ);
    }

    private void addMeshCollider(int x, int z) {
        MeshCollider mc = chunks[x, z].AddComponent<MeshCollider>();
        mc.material = worldMaterial;
    }


    void Start()
    {
        chunks = new GameObject[size,size]; 
        // Instantiate chunks
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Debug.Log("instantiate now");
                chunks[x, y] = Instantiate(myPrefab, new Vector3(4 * x, 1, 4 * y), Quaternion.identity); //  This quaternion corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
                addMeshCollider(x, y);
            }
        }
    }
} 