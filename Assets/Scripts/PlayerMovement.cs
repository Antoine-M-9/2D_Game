using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;

    private bool isJumping; // si il saute
    private bool isGrounded; // si il est au sol
    [HideInInspector] // permet de cacher isClimbing dans l'inspector de Unity
    public bool isClimbing; // si il grimpe

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers; 

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }

    void Update() 
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        // si j'appuie sur espace et que isGrounded est égal à true alors je peux sauter
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x); // Permet de toujours avoir une valeur positive en fonction de la direction
        // Abs signifie absolut, donc valeur toujours positive
        animator.SetFloat("Speed", characterVelocity); // Permet d'envoyer notre vitesse à notre condition "Speed"
        animator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        // permet de savoir si le personnage est au sol  ou non

        MovePlayer(horizontalMovement, verticalMovement); // Fait appel à la fonction qui va déplacer notre joueur
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            // déplacement horizontal
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f); 

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else 
        {
            // déplacement vertical
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f) {
            spriteRenderer.flipX = false;
        } else if (_velocity < -0.1f){
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
