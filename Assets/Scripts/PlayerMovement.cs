using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 checkpoint;
    private Rigidbody2D rb;
    private LogicaCOfres cofres;

    [Header("Movement")]
    private float hor;
    private Vector2 movementDirection;
    public float playerSpeed;

    [Header ("Jump")]
    public float jumpForce;
    public float rcLong;
    public float fallMultiplier;

    //[Header ("Interactuar")]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cofres = FindAnyObjectByType<LogicaCOfres>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        Debug.DrawLine(transform.position, transform.position + Vector3.down * rcLong, Color.red);
        Jump();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(hor * playerSpeed, rb.linearVelocity.y);

        if(rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.fixedDeltaTime;
        }
    }
    
    void Cofres()
    {
        
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, rcLong, LayerMask.GetMask("Suelo"));
    }
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
