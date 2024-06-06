using UnityEngine;

public class TerrainGeneratorQuestion1 : MonoBehaviour
{
    public float scale = 20f;
    public int height = 50;
    public int octaves = 3;
    public float amplitudeInitiale = 0.5f;
    public float frequenceInitiale = 1.5f;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null && terrain.terrainData != null)
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
        }
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        int width = (int)terrainData.size.x;
        int depth = (int)terrainData.size.z;

        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, height, depth);
        terrainData.SetHeights(0, 0, GenerateHeights(width, depth));
        return terrainData;
    }

    float[,] GenerateHeights(int width, int depth)
    {
        float[,] heights = new float[width, depth];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                heights[x, z] = CalculateHeight(x, z, width, depth);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int z, int width, int depth)
    {
        float amplitude = amplitudeInitiale;
        float frequency = frequenceInitiale;
        float height = 0;

        for (int i = 0; i < octaves; i++)
        {
            float xCoord = x / (float)width * frequency * scale;
            float zCoord = z / (float)depth * frequency * scale;
            float sample = Mathf.PerlinNoise(xCoord, zCoord) * 2 - 1;
            height += sample * amplitude;

            amplitude *= amplitudeInitiale;
            frequency *= frequenceInitiale;
        }

        return (height + 1) / 2;  // Juste normaliser pour rester dans [0, 1]
    }
}
