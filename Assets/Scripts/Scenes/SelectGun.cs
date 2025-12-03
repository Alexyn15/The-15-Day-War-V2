using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectGun : MonoBehaviour
{
    public void LoadSelectGun()
    {
        SceneManager.LoadScene("SelectGun");
    }
}