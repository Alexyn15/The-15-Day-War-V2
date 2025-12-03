using UnityEngine;

public class OpenOB: MonoBehaviour
{
    [SerializeField] private GameObject targetObject; 
    [SerializeField] private ManagerCanvas managerCanvas; // ĐÃ ĐỔI TÊN: Drag ManagerCanvas vào đây

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (targetObject != null)
            {
                targetObject.SetActive(true); 
                Debug.Log($"✅ Bật {targetObject.name}!");
            }

            // BẮT ĐẦU MONITOR ManagerCanvas
            if (managerCanvas != null)
            {
                managerCanvas.StartMonitoring();
            }
            else
            {
                // Auto tìm nếu quên drag
                managerCanvas = FindObjectOfType<ManagerCanvas>();
                if (managerCanvas != null)
                    managerCanvas.StartMonitoring();
            }
        }
    }
}