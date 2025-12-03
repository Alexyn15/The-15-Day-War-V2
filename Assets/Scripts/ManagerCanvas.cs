using UnityEngine;

public class ManagerCanvas : MonoBehaviour
{
    [System.Serializable]
    public class CheckPair
    {
        public GameObject objA;
        public GameObject objB;
        public bool autoMonitor = true;
    }

    [SerializeField] private CheckPair[] pairs;

    public void StartMonitoring()
    {
        foreach (var pair in pairs)
        {
            pair.autoMonitor = true;
        }
    }

    private void Update()
    {
        bool shouldPause = false;

        foreach (var pair in pairs)
        {
            if (!pair.autoMonitor) continue;
            if (pair.objA == null || pair.objB == null) continue;

            if (pair.objA.activeInHierarchy != pair.objB.activeInHierarchy)
            {
                shouldPause = true;
                break;
            }
        }

        Time.timeScale = shouldPause ? 0f : 1f;
    }
}