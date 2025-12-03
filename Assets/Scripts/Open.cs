using UnityEngine;

public class Open : MonoBehaviour
{
    public GameObject Canvas; 
    public GameObject InteractCanvas; 
    

    public void CanvasOnClick()
    {
        Canvas.SetActive(false);
        InteractCanvas.SetActive(true); 
    }
}
