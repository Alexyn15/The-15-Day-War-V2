using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MonoBehaviour enemyMoveScript;  

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (enemyMoveScript != null)
            {
                enemyMoveScript.enabled = false;  
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (enemyMoveScript != null)
            {
                enemyMoveScript.enabled = true;   
            }
        }
    }
}
