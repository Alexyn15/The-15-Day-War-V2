using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject gameWinCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (gameWinCanvas != null)
                gameWinCanvas.SetActive(true);

            Time.timeScale = 0f;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                Destroy(enemy);
            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
                Destroy(bullet);
                
        }
    }
}
