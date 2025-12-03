using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameHandler : MonoBehaviour
{
    public static void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        PlayerHealth health = player.GetComponent<PlayerHealth>();
        PlayerMove move = player.GetComponent<PlayerMove>(); 

        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("PlayerHealth", health != null ? health.GetHealth() : 100);
        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey("SavedScene");
    }

    public static void LoadGame()
    {
        if (!HasSave()) 
        {
            Debug.Log("No Save Found → Load Level1 mới");
            SceneManager.LoadScene("Level1");
            return;
        }

        string scene = PlayerPrefs.GetString("SavedScene");
        SceneManager.LoadScene(scene);
    }
}
