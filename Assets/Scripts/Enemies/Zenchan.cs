using UnityEngine;
using System.Collections;
using CustomLibrary.Collisions;

public class Zenchan : Enemy {

    public float jumpSpeed;

    //Directions
    public MoveDirection startDirection;
    private MoveDirection direction;

    public void SetDirection(MoveDirection direction) {
        this.direction = direction;
    }

    public enum MoveDirection {
        idle = 0,
        left = 1,
        right = 2,
        randomHorizontal = 3
    }


    //Voor wall checks in/uitschakelen
    public bool checkWalls = true;

    //Voor random springen in/uitschakelen
    public bool randomJumping = true;

    protected override void Start () {
        base.Start();
        

        direction = startDirection;
        //Kies aan het begin een random horizontale positie als dit zo is ingesteld
        if (direction == MoveDirection.randomHorizontal) {
            direction = (MoveDirection)Random.Range(1, 3);
        }

    }
	
	protected override void Update () {
        base.Update();
	}

    protected override void DefaultActions() {
        if (randomJumping) { //Doe een random jump als dit aanstaat
            JumpRandomizer();
        }

        if (checkWalls) { //Verander de richting alleen als checkWalls aanstaat

            if (CheckMoveDir(MoveDirection.left)) {
                direction = MoveDirection.right;
            }
            else if(CheckMoveDir(MoveDirection.right)){
                direction = MoveDirection.left;
            }
        }

        //Beweeg!
        Move(direction);
    }

    //Movement
    //--

    private bool CheckMoveDir(MoveDirection moveDir) {
        Vector2 dirV2 = (moveDir == MoveDirection.left) ? Vector2.left : Vector2.right;
        return direction == moveDir && GoodCollisions.CheckSide(this, dirV2, "Solid");
    }

    protected void Move(MoveDirection moveDir) {
        //Bepaal welke richting we op gaan
        Vector2 dir = Vector2.zero;
        switch (moveDir) {
            case MoveDirection.left: dir = Vector2.left; sr.flipX = false; break; //L
            case MoveDirection.right: dir = Vector2.right; sr.flipX = true; break; //R
            default: return;
        }

        Vector2 movement = dir * speed * Time.deltaTime;
        transform.Translate(movement); //Beweeg
    }

    //--
    //--------

    //Jump
    //--

    private void JumpRandomizer() {
        //Spring op een willekeurig moment de lucht in
        float randomFloat = Random.Range(0, 500);
        if (randomFloat < 10 && OnGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    public bool OnGround{
        //Controleer of we op de grond staan
        get { return GoodCollisions.CheckSide(this, Vector2.down, "Solid"); }
    }

    //--
    //-----
}
