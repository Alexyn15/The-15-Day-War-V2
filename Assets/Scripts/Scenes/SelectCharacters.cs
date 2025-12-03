using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacters : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Select Characters");
    }
}
