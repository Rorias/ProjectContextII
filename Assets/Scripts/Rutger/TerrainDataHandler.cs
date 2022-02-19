using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDataHandler : MonoBehaviour
{
    public Terrain terrain;
    public int chunksX = 30;
    public int chunksY = 30;
    public Vector2 chunkSize = new Vector2();
    public List<GameObject> grassList = new List<GameObject>();
    public List<GameObject>[,] grassChunks;
    public Vector2Int activeChunk = new Vector2Int();

    private void Start()
    {
        GenerateColliders();
        GenerateClusters();
    }

    // Start is called before the first frame update
    public void GenerateColliders()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(transform.childCount - 1 - i).gameObject);
        }
        
        grassList.Clear();
        TerrainData terrainData;
        terrainData = terrain.terrainData;
        for (int i = 0; i < terrainData.treeInstanceCount; i++)
        {
            float x = (terrain.transform.position.x + (terrainData.treeInstances[i].position.x * terrainData.size.x));
            float y = (terrain.transform.position.y + (terrainData.treeInstances[i].position.y * terrainData.size.y));
            float z = (terrain.transform.position.z + (terrainData.treeInstances[i].position.z * terrainData.size.z));
            //Debug.Log(x + ", "+ z);
            GameObject n = new GameObject("GrassCol");
            n.tag = "Grass";
            n.transform.position = new Vector3(x, y, z);
            n.AddComponent<SphereCollider>().isTrigger = true;
            n.transform.SetParent(transform);
            n.SetActive(false);
            grassList.Add(n);
        }
        //terrainData.RefreshPrototypes();
    }

    public int grassNr()
    {
        return grassList.Count;
    }

    public void GenerateClusters()
    {
        chunkSize.x = terrain.terrainData.size.x/chunksX;
        chunkSize.y = terrain.terrainData.size.z/chunksY;
        Debug.Log("Chunk size: " + chunkSize);
        grassChunks = new List<GameObject>[chunksX, chunksY];
        //List<List<List<GameObject>>> chunks = new List<List<List<GameObject>>>();
        Debug.Log("Generating chunks...");
        for (int x = 0; x < chunksX; x++)
        {
            for (int y = 0; y < chunksY; y++)
            {
                grassChunks[x, y] = new List<GameObject>();
                //Debug.Log("Looking for grass in chunk X:" + x + ", Y:" + y);
                for (int g = 0; g < grassList.Count; g++) {
                    //Debug.Log(g);
                    Vector3 pos = grassList[g].transform.position;
                    if(pos.x > x * chunkSize.x && pos.x < (x+1) * chunkSize.x)
                    {
                        if (pos.z > y * chunkSize.y && pos.z < (y+1) * chunkSize.y)
                        {
                            //Grass is in this chunk
                            //Debug.Log("Found chunk for grass!");
                            grassChunks[x,y].Add(grassList[g]);
                        }
                    }
                }
            }
        }
        //grassChunks = chunks;
        Debug.Log("Done!");
    }

    public void UpdateChunks(Vector3 pos)
    {
        //Debug.Log(pos);
        for (int x = 0; x < chunksX; x++)
        {
            for (int y = 0; y < chunksY; y++)
            {
                //float xmin = x * chunkSize.x;
                //float xmax = (x + 1) * chunkSize.x;
                //Debug.Log(xmin + " - " + xmax);
                if (pos.x > x * chunkSize.x && pos.x < (x + 1) * chunkSize.x)
                {
                    //Debug.Log("p in x");
                    if (pos.z > y * chunkSize.y && pos.z < (y+1) * chunkSize.y)
                    {
                        ///Debug.Log("playerChunk found!");
                        if (activeChunk.x != x || activeChunk.y != y)
                        {
                            DeactivateChunk(activeChunk.x, activeChunk.y);
                            ActivateChunk(x, y);
                            activeChunk.x = x;
                            activeChunk.y = y;
                        }
                    }
                }
            }
        }
    }

    void ActivateChunk(int x, int y)
    {
        Debug.Log("Activating chunk: " + x + ", " + y);
        foreach (GameObject obj in grassChunks[x,y])
        {
            obj.SetActive(true);
        }
    }

    void DeactivateChunk(int x, int y)
    {
        Debug.Log("Deactivating chunk: " + x + ", " + y);
        foreach (GameObject obj in grassChunks[x,y]){
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
