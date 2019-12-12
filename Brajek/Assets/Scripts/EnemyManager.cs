using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;                
    public float spawnTime = 3f;       
    public Transform[] spawnPoints; 

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);

        for (int i = 0; i < 10; i++) {
          Spawn(0);
        }
    }

    void Spawn() {
        int index = Random.Range(0, spawnPoints.Length);
        Spawn(index);
    }

    void Spawn(int index)
    {
        GameObject newEnemy = Instantiate(enemy, 
            spawnPoints[index].position, 
            spawnPoints[index].rotation);
    }
}
