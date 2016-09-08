using UnityEngine;
using CustomLibrary.Collisions;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 5;
    public float jumpSpeed = 5;
    public GameObject bubblePrefab;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastDirection = Vector2.left;

    private bool onGround = false;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        //Naar links bewegen
        if (InputManager.Left) {
            if (!GoodCollisions.CheckSide(this, Vector2.left,  0.1f, "Solid")) { //Links vrij
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                spriteRenderer.flipX = false; //Kijk naar links
                lastDirection = Vector2.left; //onthoud waar we het laatst naartoe hebben gekeken
            }
        }
        //Naar rechts bewegen
        if (InputManager.Right) {
            if (!GoodCollisions.CheckSide(this, Vector2.right,  0.1f, "Solid")) { //Rechts vrij
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                spriteRenderer.flipX = true; //Kijk naar rechts
                lastDirection = Vector2.right; //onthoud waar we het laatst naartoe hebben gekeken
            }
        }

        //Controleren of we op de grond staan
        onGround = GoodCollisions.CheckSide(this, Vector2.down, 0.1f, -261);

        //Springen
        if (InputManager.Jump && onGround) {
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
}
