using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Terrain WorldTerrain;
    public LayerMask TerrainLayer;
    public static float TerrainLeft, TerrainRight, TerrainTop, TerrainBottom, TerrainWidth, TerrainLenght, TerrainHeight;

    public static ArrayList units = new ArrayList();
    public static ArrayList positions = new ArrayList();
    public static ArrayList rotations = new ArrayList();

    private TerrainCollider terrainCollider;

    private int numberOfEnemyOne = 30;
    private int numberOfEnemyTwo = 20;

    // Start is called before the first frame update
    void Awake()
    {
        TerrainLeft = WorldTerrain.transform.position.x;
        TerrainBottom = WorldTerrain.transform.position.z;

        TerrainWidth = WorldTerrain.terrainData.size.x;
        TerrainLenght = WorldTerrain.terrainData.size.z;
        TerrainHeight = WorldTerrain.terrainData.size.y;

        TerrainRight = TerrainLeft + TerrainWidth;
        TerrainTop = TerrainBottom + TerrainLenght;

        InstantiateRandomPosition("StoneMonster", numberOfEnemyOne, 1f);
        InstantiateRandomPosition("Ghost_Brown", numberOfEnemyTwo, 1.5f);

        terrainCollider = GetComponent<TerrainCollider>();

    }

    public void InstantiateRandomPosition(string _resource, int _amount, float _addedHeight)
    {
        int i = 0;
        float terrainHeight = 0f;
        RaycastHit hit;
        float randomPositionX, randomPositionY, randomPositionZ;
        Vector3 randomPosition = Vector3.zero;

        int mountainCompensation = 40;

        do
        {
            i++;
            // Generate the positions
            randomPositionX = Random.Range(TerrainLeft + mountainCompensation, TerrainRight - mountainCompensation);
            randomPositionZ = Random.Range(TerrainBottom + mountainCompensation, TerrainTop - mountainCompensation);

            if (Physics.Raycast(new Vector3(randomPositionX, 9999f, randomPositionZ),
                Vector3.down,
                out hit,
                Mathf.Infinity, 
                TerrainLayer))
            {
                terrainHeight = hit.point.y;
            }

            randomPositionY = terrainHeight + _addedHeight;
            randomPosition = new Vector3(randomPositionX, randomPositionY, randomPositionZ);

            // Note that the prefabs must been in the Resources folder
            Instantiate(Resources.Load(_resource, typeof(GameObject)), randomPosition, Quaternion.identity);

        } while (i < _amount);
    }
}
