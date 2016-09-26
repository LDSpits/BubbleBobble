using UnityEngine;
using System.Collections;
using CustomLibrary.Collisions;

public class Monsta : Enemy {

    
    //Direction
    Direction direction;

    public Direction startDirection;

    public enum Direction {
        leftUp = 0,
        rightUp = 1,
        rightDown = 2,
        leftDown = 3
    }
    //----------
    void OnCollisionEnter2D(Collision2D collision) {
        bool isRight = false;
        bool isUp = false;

        Vector2 posDifference = (Vector2)transform.position - collision.contacts[0].point;

        isRight = posDifference.x > Random.Range(0, 0.5f);
        isUp = posDifference.y > Random.Range(0, 0.5f);
        //}

        if (isRight) {
            if (isUp) {
                direction = Direction.rightUp;
            }
            else {
                direction = Direction.rightDown;
            }
        }
        else {
            if (isUp) {
                direction = Direction.leftUp;
            }
            else {
                direction = Direction.leftDown;
            }
        }
    }

    new void Start() {
        base.Start();
    }
    new void Update() {
        sr.flipX = HorDirIsRight();

        base.Update();
    }

    protected override void DefaultActions() {
        Fly();
    }

    private void Fly() {
        Vector2 moveDir = Vector2.zero;
        switch (direction) {
            case Direction.leftDown: moveDir = new Vector2(-1, -1); break;
            case Direction.leftUp: moveDir = new Vector2(-1, 1); break;
            case Direction.rightDown: moveDir = new Vector2(1, -1); break;
            case Direction.rightUp: moveDir = new Vector2(1, 1); break;
        }
        //Om te voorkomen dat de Monsta door muren heen gaat.
        Vector2 movement = moveDir * speed * Time.deltaTime;

        //print("frametime: "+Time.deltaTime+" movement: "+ movement);

        if (Time.deltaTime > 0.05 /* lager dan 20 fps*/ ){
            while (OnCollisionCourseWithTag(transform.position, (Vector2)transform.position + movement, "Solid") && Mathf.Abs(movement.magnitude) > 1) {
                movement = new Vector2(movement.x * 0.5f, movement.y * 0.5f);
            }
            //print("endMovement: "+movement);
        }
        
        
        transform.Translate(movement);
    }

    private bool OnCollisionCourseWithTag(Vector2 currentPos, Vector2 nextPos, string tag) {
        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPos, nextPos);
        foreach(RaycastHit2D hit in hits) {
            if(hit.transform.tag == tag) {
                print("Collision Course == true");
                return true;
            }
        }
        return false;
    }

    private bool HorDirIsRight() {
        return direction == Direction.rightDown || direction == Direction.rightUp;
    }
}
