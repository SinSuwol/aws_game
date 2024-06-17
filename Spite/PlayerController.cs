using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public bool isGameOver = false;
    public Text gameOverText;
    public Text finalScoreText;
    public float minX = -8f;
    public float maxX = 8f;

    private Rigidbody2D rb;
    private ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManager>();
        gameOverText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver)
        {
            float move = Input.GetAxis("Horizontal");
            Vector2 velocity = rb.velocity;
            velocity.x = move * speed;
            rb.velocity = velocity;

            // 플레이어의 위치를 제한
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        finalScoreText.gameObject.SetActive(true);
        finalScoreText.text = "Final Score: " + scoreManager.GetScore();
        Debug.Log("Game Over");
    }

    void RestartGame()
    {
        // 현재 씬을 다시 로드하여 게임을 재시작
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
