using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;

    public GameObject player;
    public GameObject pauseGameCanvas;
    public GameObject gameoverCanvas;
    public GameObject gamewinCanvas;
    public Button restartButton;
    public Button exitButton;

    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (pauseGameCanvas != null) pauseGameCanvas.SetActive(false);
        if (gameoverCanvas != null) gameoverCanvas.SetActive(false);
        if (gamewinCanvas != null) gamewinCanvas.SetActive(false);

        if (restartButton != null) restartButton.gameObject.SetActive(false);
        if (exitButton != null) exitButton.gameObject.SetActive(false);

        if (scoreText != null) scoreText.gameObject.SetActive(true);
        if (player != null) player.SetActive(true);
    }

    private void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.J))
        {
            isPaused = !isPaused;

            if (pauseGameCanvas != null)
                pauseGameCanvas.SetActive(isPaused);

            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        if ((gameoverCanvas != null && gameoverCanvas.activeSelf) || (gamewinCanvas != null && gamewinCanvas.activeSelf))
        {
            Time.timeScale = 0f;
        }
    }

   public void GameOver()
    {
    Time.timeScale = 0f;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    
    if (gameoverCanvas != null) 
        gameoverCanvas.SetActive(true);

}


   public void GameWinner()
    {
    Time.timeScale = 0f;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    
    if (gamewinCanvas != null) 
        gamewinCanvas.SetActive(true);

    
}

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
