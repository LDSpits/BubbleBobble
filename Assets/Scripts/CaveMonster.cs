using UnityEngine;
using System.Collections;
using UnityEditor;
using CustomLibrary.Collisions;

public class CaveMonster : MonoBehaviour {

    public LayerMask mask;

    public Sprite capturedSprite;

    public float speed = 5;
    public float jumpForce = 100;

    bool gotoPlayer;

    public MoveDirection startDirection;
    private MoveDirection direction;
    public MoveDirection Direction
    {
        get{
            return direction;
        }
        set{
            switch (value) {
                case MoveDirection.left: sr.flipX = false; break;
                case MoveDirection.right: sr.flipX = true; break;
                case MoveDirection.randomHorizontal:
                    direction = (MoveDirection)Random.Range(1f, 3f);

                    if (direction == MoveDirection.left)
                        sr.flipX = false;
                    else if(direction == MoveDirection.right)
                        sr.flipX = true;
                    break;
                case MoveDirection.jumpLeft: sr.flipX = false; goto case MoveDirection.jump;
                case MoveDirection.jumpRight: sr.flipX = true; goto case MoveDirection.jump;
                case MoveDirection.jump:
                    if (!OnGround()) {
                        return;
                    }
                    break;
            }
            direction = value;
        }
    }

    public enum MoveDirection {
        idle,
        left,
        right,
        jump,
        jumpLeft,
        jumpRight,
        randomHorizontal
    }

    Rigidbody2D rb;
    SpriteRenderer sr;

    void OnCollisionEnter2D(Collision2D coll) {
        DrawCollisionPointsLines(coll,2);
        
    }

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        
        sr = GetComponent<SpriteRenderer>();
        Direction = startDirection;
    }
	
	void Update () {

        //TEST
        if (Input.GetKeyDown(KeyCode.F1)) {
            Direction = MoveDirection.jumpLeft;
        }
        if (Input.GetKeyDown(KeyCode.F2)) {
            Direction = MoveDirection.jump;
        }
        if (Input.GetKeyDown(KeyCode.F3)) {
            Direction = MoveDirection.jumpRight;
        }
        if (Input.GetKeyDown(KeyCode.F4)) {
            rb.MovePosition(transform.position);
        }
        //----

        JumpRandomizer();

        Move(Direction);

        if (GoodCollisions.CheckSide(this, Vector2.left, mask)) {
            Direction = MoveDirection.right;
        }
        else if (GoodCollisions.CheckSide(this, Vector2.right, mask)) {
            Direction = MoveDirection.left;
        }
    }

    void Move(MoveDirection dir) {
        Vector2 dirV2 = Vector2.zero;
        bool jump = true;
        switch (dir) {
            case MoveDirection.left: dirV2 = Vector2.left; jump = false; break;
            case MoveDirection.right: dirV2 = Vector2.right; jump = false; break;
            case MoveDirection.jump: dirV2 = Vector2.up; jump = true; break;
            case MoveDirection.jumpRight: dirV2 = Vector2.up * 1.5f + Vector2.right; jump = true; break;
            case MoveDirection.jumpLeft: dirV2 = Vector2.up * 1.5f+Vector2.left; jump = true; break;
            default: return;
        }
        if (jump) {
            //rb.AddForce(dirV2*jumpForce);
            rb.velocity = dirV2 * jumpForce;
            if (Direction == MoveDirection.jumpLeft) {
                Direction = MoveDirection.left;
            }
            else if (Direction == MoveDirection.jumpRight) {
                Direction = MoveDirection.left;
            }
            else {
                Direction = (sr.flipX) ? MoveDirection.right: MoveDirection.left;
            }
            jump = false;
        }
        else if (OnGround()) {
            Vector2 movement = dirV2 * speed * Time.deltaTime;
            transform.Translate(movement);
        }
        
    }



    void DrawCollisionPointsLines(Collision2D collision, float lineLifeSpan) {
        foreach(ContactPoint2D contact in collision.contacts) {
            Debug.DrawLine(transform.position,contact.point,Color.green,lineLifeSpan);
        }
    }

    bool OnGround() {
        return GoodCollisions.CheckSide(this, Vector2.down, 0.6f, mask);
    }

    void JumpRandomizer() {
        //Dit is een comment om de git client uit te testen!
        float randomFloat = Random.Range(0, 400);
        if (randomFloat < 3) {
            Direction = MoveDirection.jumpLeft;
        }
        else if (randomFloat < 6) {
            Direction = MoveDirection.jumpRight;
        }
    }
}

