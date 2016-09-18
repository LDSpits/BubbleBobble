using UnityEngine;
using CustomLibrary.Collisions;

public class CaveMonster : MonoBehaviour {

    public float speed = 5;
    public float jumpSpeed = 5;

    private bool gotoPlayer;

    private Animator animator;
    private bool captured;
    private float timePassed;

    public MoveDirection startDirection;
    private MoveDirection direction;

    public void SetDirection(MoveDirection direction) {
        this.direction = direction;
    }

    //Voor wall checks in/uitschakelen
    protected bool checkWalls = true;

    //Voor springen in/uitschakelen
    protected bool randomJumping = true;

    public enum MoveDirection {
        idle = 0,
        left = 1,
        right = 2,
        randomHorizontal = 3
    }

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        direction = startDirection;
        //Kies aan het begin een random horizontale positie als dit zo is ingesteld
        if (direction == MoveDirection.randomHorizontal) { direction = (MoveDirection)Random.Range(1, 3); }

        animator = GetComponent<Animator>();

        //Zorg ervoor dat enemies elkaar negeren
        Collider2D coll = GetComponent<Collider2D>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Collider2D otherColl = enemy.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(coll, otherColl);
        }
    }

    void Update() {

        if (captured) { //Bubbel modus
                timePassed += Time.deltaTime;

                //als er 2 seconden voorbij zijn
                if (timePassed > 2) {
                    //verplaats naar boven en 
                    transform.position = Vector2.Lerp(transform.position, new Vector2(10, 9), 3 * Time.deltaTime);
                    rb.velocity = new Vector2(0, -0.1f);
                }

                //Als de gepasseerde tijd nadat we zijn gevangen hoger is dan 5 seconden, ontsnappen we
                if (timePassed > 5) {
                    captured = false;
                    animator.SetBool("captured", false);
                }

        }
        else { //Loop rond

            if (randomJumping) { //Doe een random jump als dit aanstaat
                JumpRandomizer();
            }

            if (checkWalls) { //Verander de richting alleen als checkWalls aanstaat
                if (direction == MoveDirection.left && GoodCollisions.CheckSide(this, Vector2.left, "Solid")) { //Naar Rechts
                    direction = MoveDirection.right;
                }
                else if (direction == MoveDirection.right && GoodCollisions.CheckSide(this, Vector2.right, "Solid")) { //Naar Links
                    direction = MoveDirection.left;
                }
            }

            //Beweeg!
            Move(direction);

        }
    }

    private void Move(MoveDirection moveDir) {
        //Bepaal welke richting we op gaan
        Vector2 dir = Vector2.zero;
        switch (moveDir) {
            case MoveDirection.left:  dir = Vector2.left;  sr.flipX = false; break; //L
            case MoveDirection.right: dir = Vector2.right; sr.flipX = true;  break; //R
            default: return;
        }

        Vector2 movement = dir * speed * Time.deltaTime;
        transform.Translate(movement); //Beweeg
    }

    public bool OnGround {
        //Controleer of we op de grond staan
        get { return GoodCollisions.CheckSide(this, Vector2.down, "Solid"); }
    }

    private void JumpRandomizer() {
        //Spring op een willekeurig moment de lucht in
        float randomFloat = Random.Range(0, 500);
        if (randomFloat < 10 && OnGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    public bool IsCaptured {
        get { return captured; }
    }

    public void Capture() {
        this.captured = true;
        animator.SetBool("captured", true);
        timePassed = 0;
    }


}

