using UnityEngine;

public class X : MonoBehaviour
{
    [Header("Cài đặt Canvas")]
    public GameObject currentCanvas; 
    public GameObject nextCanvas;    

    public void SwitchCanvas()
    {
        if (currentCanvas != null)
            currentCanvas.SetActive(false);

        if (nextCanvas != null)
            nextCanvas.SetActive(true);
    }
}
