using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5f;
    public int scoreValue = 1;

    void Update()
    {
        if (FindObjectOfType<PlayerController>().isGameOver)
        {
            return; // 게임 오버 시 업데이트 중단
        }

        transform.Translate(0, -speed * Time.deltaTime, 0);

        if (transform.position.y < -6f)
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.gameOverText.gameObject.SetActive(true);
                playerController.isGameOver = true;
                playerController.finalScoreText.gameObject.SetActive(true);
                playerController.finalScoreText.text = "Final Score: " + FindObjectOfType<ScoreManager>().GetScore();
                Debug.Log("Game Over");
            }
        }
    }
}
