using UnityEngine;

public class OpenCanvas : MonoBehaviour
{
    public GameObject Canvas; 

    public void CanvasOnClick()
    {
        Canvas.SetActive(true); 
    }
}
