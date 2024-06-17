using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 1f;
    private float nextSpawn = 0f;
    public float difficultyIncreaseRate = 0.1f;
    public float obstacleSpeed = 5f;

    void Update()
    {
        // ���� ���� ���� Ȯ��
        if (FindObjectOfType<PlayerController>().isGameOver)
        {
            return; // ���� ���� �� ������Ʈ �ߴ�
        }

        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 6f, 0);
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            obstacle.GetComponent<Obstacle>().speed = obstacleSpeed;
        }

        if (spawnRate > 0.2f)
        {
            spawnRate -= difficultyIncreaseRate * Time.deltaTime;
        }

        obstacleSpeed += difficultyIncreaseRate * Time.deltaTime;
    }
}
