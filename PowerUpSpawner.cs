using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject[] powerUps; // Drag Health & Speed Boost prefabs here
    public Transform[] spawnPoints; // Empty GameObjects as spawn locations
    public int powerUpCount = 4; // Number of power-ups per loop

    public void SpawnPowerUps()
    {
        for (int i = 0; i < powerUpCount; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            int powerUpIndex = Random.Range(0, powerUps.Length);
           GameObject Powerups = Instantiate(powerUps[powerUpIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }
}
