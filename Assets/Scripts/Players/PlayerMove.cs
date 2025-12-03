using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;     
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Animator anim;  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
            return;
        }
        rb.gravityScale = 0; 

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on " + gameObject.name);
            return;
        }

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
            return;
        }

        if (PlayerPrefs.HasKey("PlayerX"))
        {
            transform.position = new Vector2(
                PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY")
            );

            if (GetComponent<PlayerHealth>() != null)
                GetComponent<PlayerHealth>().SetHealth(PlayerPrefs.GetInt("PlayerHealth"));
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");  
        
        movement = movement.normalized;

        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; 
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true; 
        }

        if (movement.magnitude > 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
