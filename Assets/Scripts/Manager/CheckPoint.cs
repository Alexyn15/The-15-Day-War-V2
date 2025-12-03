using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkpointCanvas; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (checkpointCanvas != null)
                checkpointCanvas.SetActive(true);

            PlayerPrefs.SetFloat("SavedX", other.transform.position.x);
            PlayerPrefs.SetFloat("SavedY", other.transform.position.y);
            PlayerPrefs.SetFloat("SavedZ", other.transform.position.z);
            PlayerPrefs.Save();

            Debug.Log("Saved position + Enabled canvas!");
        }
    }
}
