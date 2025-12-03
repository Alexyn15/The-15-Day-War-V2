using UnityEngine;

public class Notice : MonoBehaviour
{
    public GameObject noticeCanvas; 

    public void NoticeCanvasOnClick()
    {
        noticeCanvas.SetActive(false); 
    }
}
