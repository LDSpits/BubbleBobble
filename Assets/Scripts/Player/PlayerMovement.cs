using UnityEngine;
using CustomLibrary.Collisions;

public class PlayerMovement : MonoBehaviour {

    public Vector2 respawn;

    public float moveSpeed = 5;
    public float jumpSpeed = 5;
    public GameObject bubblePrefab;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D bc;
    private Vector2 lastDirection = Vector2.right;
    private float seconds = 0;

    private bool isMoving = false;
    private bool onGround = false;
    private bool isDead = false;

    [SerializeField]
    private Players.player thisPlayer;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

    }

    // Update is called once per frame
    void Update() {

        isMoving = false;

        if (!isDead && !UIManager.isPaused) {
            //naar links bewegen
            if (InputManager.Left(thisPlayer) && !InputManager.Right(thisPlayer)) {
                Move(Vector2.left);
            }
            //Naar rechts bewegen
            if (InputManager.Right(thisPlayer) && !InputManager.Left(thisPlayer)) {
                Move(Vector2.right);
            }

            //Controleren of we op de grond staan
            onGround = GoodCollisions.CheckSide(this, Vector2.down, "Solid");

            //Springen
            if (InputManager.Jump(thisPlayer) && onGround) {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                AudioManager.PlaySound(AudioManager.Sounds.Jump);
            }

            //schieten van de bubbel
            if (InputManager.Action(thisPlayer)) {
                bubblePrefab.GetComponent<Bubble>().direction = lastDirection;
                Instantiate(bubblePrefab);
            }
        }
        else if(isDead){
            seconds += Time.deltaTime;

            if (seconds > 2 ) {
                isDead = false;
                animator.SetBool("isDead", false);
                transform.position = respawn;
                rb.WakeUp();
                bc.enabled = true;
            }

        }

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("onGround", onGround);

    }

    private void Move(Vector2 direction) {
        if (!GoodCollisions.CheckSide(this, direction, "Solid")) { //Links vrij
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

            if (direction == Vector2.left)
                spriteRenderer.flipX = false; //Kijk naar links
            else
                spriteRenderer.flipX = true; //Kijk naar rechts

            isMoving = true;
            lastDirection = direction; //onthoud waar we het laatst naartoe hebben gekeken
        }
    }

    private void Die()
    {
        AudioManager.PlaySound(AudioManager.Sounds.Death);
        isDead = true;
        animator.SetBool("isDead", true);
        GameManager.DecreaseLife(Players.player1);
        bc.enabled = false;
        seconds = 0;
        rb.Sleep();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.collider.CompareTag("Enemy"))
        { //Het cavemonster zit niet in een bubbel, uh oh...
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            //Verkrijg het cavemonster
            CaveMonster caveMonster = coll.GetComponent(typeof(CaveMonster)) as CaveMonster;

            if (caveMonster.IsCaptured)
            { //Het cavemonster zit in een bubbel, versla hem!
                caveMonster.Defeat(true);
                AudioManager.PlaySound(AudioManager.Sounds.BubbleCapture);
            }

        }
    }

}

public static class Players
{

    public enum player
    {
        player1,
        player2
    }

    public const player player1 = player.player1;
    public const player player2 = player.player2;

}
