using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    public GameObject canvasToClose;   
    public Interact interactScript;    

    public void CanvasOnClick()
    {
        if (canvasToClose != null)
            canvasToClose.SetActive(false);

        if (interactScript != null)
            interactScript.StopInteract();
    }
}
