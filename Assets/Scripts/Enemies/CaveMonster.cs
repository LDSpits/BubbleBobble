using UnityEngine;
using CustomLibrary.Collisions;

public class CaveMonster : MonoBehaviour {

    public float speed = 5;
    public float jumpSpeed = 5;

    bool gotoPlayer;

    public MoveDirection startDirection;
    private MoveDirection direction;

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
    }

    void Update() {

        JumpRandomizer();

        if (direction == MoveDirection.left && GoodCollisions.CheckSide(this, Vector2.left, "Solid")) { //Rechts
            direction = MoveDirection.right;
        }
        else if (direction == MoveDirection.right && GoodCollisions.CheckSide(this, Vector2.right, "Solid")) { //Links
            direction = MoveDirection.left;
        }
        else if(direction == MoveDirection.randomHorizontal) { 
            //Kies een random horizontale positie
            direction = (MoveDirection)Random.Range(1,3);
        }

        Move(direction);

    }

    void Move(MoveDirection dir) {
        //Bepaal welke richting we op gaan
        Vector2 dirV2 = Vector2.zero;
        switch (dir) {
            case MoveDirection.left:  dirV2 = Vector2.left;  sr.flipX = false; break; //L
            case MoveDirection.right: dirV2 = Vector2.right; sr.flipX = true;  break; //R
            default: return;
        }

        Vector2 movement = dirV2 * speed * Time.deltaTime;
        transform.Translate(movement); //Beweeg
    }

    bool OnGround {
        //Controleer of we op de grond staan
        get { return GoodCollisions.CheckSide(this, Vector2.down, "Solid"); }
    }

    void JumpRandomizer() {
        //Spring op een willekeurig moment de lucht in
        float randomFloat = Random.Range(0, 500);
        if (randomFloat < 10 && OnGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

}

