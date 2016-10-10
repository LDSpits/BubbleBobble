using UnityEngine;
using System.Collections.Generic;
using CustomLibrary.Collisions;

public class Player : MonoBehaviour
{

    public Vector2 respawnLocation;

    public float moveSpeed = 5;
    public float jumpSpeed = 5;
    public GameObject bubblePrefab;

    //Alle animaties van de speler die geswapt moeten worden
    public AnimationClip[] animationClips;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D bc;
    private Vector2 lastDirection = Vector2.right;

    private bool isMoving, onGround, isDead = false;
    private bool canDie = true;

    [SerializeField]
    private PlayerID thisPlayer;

    public PlayerID ThisPlayer
    {
        get { return thisPlayer; }
    }

    // Use this for initialization
    void Start()
    {
        //registeer de speler bij de GameManager
        thisPlayer = GameManager.RegisterPlayer(this);
        //zet de naam naar de speler ID
        gameObject.name = thisPlayer.ToString();

        //verkrijg alle componenten
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        //Overschrijf alle animaties
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;
        overrideController["Anim_player_idle"] = animationClips[0];
        overrideController["Anim_player_moving"] = animationClips[1];
        overrideController["Anim_player_falling"] = animationClips[2];
        overrideController["Anim_player_dead"] = animationClips[3];
        overrideController["Anim_player_shooting"] = animationClips[4];
        overrideController["Anim_player_shootingfalling"] = animationClips[5];
        animator.runtimeAnimatorController = overrideController;
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = false;

        if (!isDead && !UIManager.isPaused)
        {
            print(string.Format("Player id: {0}", thisPlayer));
            //naar links bewegen
            if (InputManager.Left(thisPlayer) && !InputManager.Right(thisPlayer))
            {
                Move(Vector2.left);
            }
            //Naar rechts bewegen
            if (InputManager.Right(thisPlayer) && !InputManager.Left(thisPlayer))
            {
                Move(Vector2.right);
            }

            //Controleren of we op de grond staan
            onGround = GoodCollisions.CheckSide(this, Vector2.down, "Solid");

            //Springen
            if (InputManager.Jump(thisPlayer) && onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                AudioManager.PlaySound(AudioManager.Sounds.Jump);
            }

            //schieten van de bubbel
            if (InputManager.Action(thisPlayer))
            {
                bubblePrefab.GetComponent<Bubble>().direction = lastDirection;
                Instantiate(bubblePrefab,transform.position,Quaternion.identity);
                animator.SetTrigger("shoot");
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

            if (direction == Vector2.left)
                spriteRenderer.flipX = false; //Kijk naar links
            else
                spriteRenderer.flipX = true; //Kijk naar rechts

            isMoving = true;
            lastDirection = direction; //onthoud waar we het laatst naartoe hebben gekeken
        }
    }

    public void Die()
    {
        AudioManager.PlaySound(AudioManager.Sounds.Death);
        isDead = true;
        canDie = false;
        animator.SetBool("isDead", true);
        bc.enabled = false;
        rb.Sleep();
    }

    public void Respawn()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f); //Maak ons doorzichtig en onsterfelijk
        print(string.Format("Player {0} has respawned, is unkillable",thisPlayer));
        isDead = false;
        animator.SetBool("isDead", false);
        transform.position = respawnLocation;
        rb.WakeUp();
        bc.enabled = true;
    }

    public void Mortalize()
    {
        print(string.Format("Player {0} can now be killed.",thisPlayer));
        canDie = true;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Enemy") && canDie)
        { //Het cavemonster zit niet in een bubbel, uh oh...
            GameManager.DecreaseLife(thisPlayer);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy")) {

            Enemy enemy = coll.GetComponent<Enemy>();
            if (enemy) {
                enemy.Defeat();
                AudioManager.PlaySound(AudioManager.Sounds.BubbleCapture);
                return;
            }
        }
    }

    void OnTriggerStay2D(Collider2D coll) {
        if (coll.CompareTag("WaterFX")) {
            transform.Translate(coll.transform.up * Time.deltaTime);
        }
    }

}

public enum PlayerID
{
    player1,
    player2,
    player3,
    player4
}