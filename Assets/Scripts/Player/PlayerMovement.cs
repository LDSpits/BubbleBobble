using UnityEngine;
using CustomLibrary.Collisions;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 5;
    public float jumpSpeed = 5;
    public GameObject bubblePrefab;

    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D bc;
    private Vector2 lastDirection = Vector2.left;
    private float seconds = 0;

    private bool isMoving = false;
    private bool onGround = false;
    private bool isDead = false;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        GoodCollisions.CheckSide(this, Vector2.left);
    }

    // Update is called once per frame
    void Update() {

        isMoving = false;

        if (!isDead)
        {
            //naar links bewegen
            if (InputManager.Left && !InputManager.Right)
            {
                Move(Vector2.left);
            }
            //Naar rechts bewegen
            if (InputManager.Right && !InputManager.Left)
            {
                Move(Vector2.right);
            }

            //Controleren of we op de grond staan
            onGround = GoodCollisions.CheckSide(this, Vector2.down);

            //Springen
            if (InputManager.Jump && onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                AudioManager.PlaySound(AudioManager.Sounds.Jump);
            }

            //schieten van de bubbel
            if (InputManager.Action)
            {
                bubblePrefab.GetComponent<Bubble>().direction = lastDirection;
                Instantiate(bubblePrefab);
            }
        }
        else
        {
            seconds += Time.deltaTime;

            if (seconds > 2)
            {
                isDead = false;
                animator.SetBool("isDead", false);
                transform.position = new Vector2(3, 1);
                rb.WakeUp();
                bc.enabled = true;
            }
            
        }
        
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("onGround", onGround);
        
    }

    private void Move(Vector2 direction)
    {
        if (!GoodCollisions.CheckSide(this, direction, "Solid"))
        { //Links vrij
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

            if(direction == Vector2.left)
                spriteRenderer.flipX = false; //Kijk naar links
            else
                spriteRenderer.flipX = true; //Kijk naar rechts

            isMoving = true;
            lastDirection = direction; //onthoud waar we het laatst naartoe hebben gekeken
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            AudioManager.PlaySound(AudioManager.Sounds.Death);
            isDead = true;
            animator.SetBool("isDead", true);
            GameManager.DecreaseLife();
            bc.enabled = false;
            seconds = 0;
            rb.Sleep();
        }
            
        
    }

}
