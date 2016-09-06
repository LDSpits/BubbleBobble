using UnityEngine;
using System.Collections;
using UnityEditor;

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
            if (value == MoveDirection.left) {
                sr.flipX = false;
            }
            else if (value == MoveDirection.right) {
                sr.flipX = true;
            }
            else if(value == MoveDirection.randomHorizontal) {
                direction = (MoveDirection)Random.Range(1f, 3f);
                sr.flipX = direction != (MoveDirection)1;
                return;
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
        Move(Direction);

        RaycastHit2D hitLeft;
        hitLeft = Physics2D.Raycast(transform.position, Vector3.left, 0.6f,mask);

        RaycastHit2D hitRight;
        hitRight = Physics2D.Raycast(transform.position, Vector3.right, 0.6f,mask);

        if (hitLeft&&Direction==MoveDirection.left) {
            print("(LEFT) hitgameobject name: "+hitLeft.transform.name);
            Direction = MoveDirection.right;
        }
        else if (hitRight&&Direction==MoveDirection.right) {
            print("(RIGHT) hitgameobject name: " + hitRight.transform.name);
            Direction = MoveDirection.left;
        }
    }

    void Move(MoveDirection dir) {
        Vector2 dirV2 = Vector2.zero;
        bool jump = true;
        switch (dir) {
            case MoveDirection.left: dirV2 = Vector2.left; jump = false; break;
            case MoveDirection.right: dirV2 = Vector2.right; jump = false; break;
            default: return;
        }
        Vector2 movement = dirV2 * speed * Time.deltaTime;
        transform.Translate(movement);
    }



    void DrawCollisionPointsLines(Collision2D collision, float lineLifeSpan) {
        foreach(ContactPoint2D contact in collision.contacts) {
            Debug.DrawLine(transform.position,contact.point,Color.green,lineLifeSpan);
        }
    }
}
