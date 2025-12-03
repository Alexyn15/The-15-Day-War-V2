using UnityEngine;

public class Continues : MonoBehaviour
{
    public void OnContinueButton()
    {
        SaveGameHandler.LoadGame();
    }
}

