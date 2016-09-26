using UnityEngine;
using System.Collections;

public class Pulpul : Enemy {

    bool moveControl = true;

    Vector2 movement = Vector2.zero;
    Vector2 oldMovement = Vector2.zero;

    public LayerMask mask;

    public float minMoveMagnitudeForBouncing;
    private Coroutine moveControlCooldownCoroutine;

    private Coroutine randomMovementCoroutine;

    private bool useRandomMovement;

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.transform.tag == "Solid" && movement.magnitude >= minMoveMagnitudeForBouncing) {
            Vector2 posDifference = collision.contacts[0].point - (Vector2)transform.position;
            movement = Vector2.ClampMagnitude(-movement + -posDifference, speed * Time.deltaTime * 2);
            if(moveControlCooldownCoroutine == null) {
                moveControlCooldownCoroutine = StartCoroutine(MoveControlCooldown(1, 0.5f));
            }
        }
    }

    new void Start() {
        
        base.Start();
    }
    new void Update() {
        base.Update();
    }

    protected override void DefaultActions() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player) {
            Fly(player.transform.position);
        }
        else {
            print("No player has been found");
        }
    }

    private void Fly(Vector2 targetPos) {
        if (moveControl) {
            if (Mathf.Round(movement.magnitude) < 5) {
                Vector2 posDifference = targetPos - (Vector2)transform.position;

                if (!Physics2D.BoxCast(transform.position, bc.size, 0, targetPos.normalized, posDifference.magnitude, mask) && Vector2.Distance(transform.position, targetPos) > 5) {
                    movement = (targetPos - (Vector2)transform.position).normalized * speed * Time.deltaTime;
                    useRandomMovement = false;
                    randomMovementCoroutine = null;
                }
                else if(!Physics2D.Linecast(transform.position,targetPos,mask) && Vector2.Distance(transform.position,targetPos) > 5) {
                    movement = (targetPos - (Vector2)transform.position).normalized * speed * Time.deltaTime;
                    useRandomMovement = false;
                    randomMovementCoroutine = null;
                }
                else if(randomMovementCoroutine == null){
                    useRandomMovement = true;
                    randomMovementCoroutine = StartCoroutine(RandomMovement());
                }
            }
        }
        movement = V2Lerp(oldMovement, movement, 0.7f);
        transform.Translate(movement);
        oldMovement = movement;
    }


    IEnumerator MoveControlCooldown(float moveControlCooldownTime, float coroutineAvailabilityCooldown) {
        moveControl = false;
        yield return new WaitForSeconds(moveControlCooldownTime);
        moveControl = true;
        yield return new WaitForSeconds(coroutineAvailabilityCooldown);
        moveControlCooldownCoroutine = null;
    }

    Vector2 V2Lerp(Vector2 aPos, Vector2 bPos, float t) {
        return new Vector2(Mathf.Lerp(aPos.x, bPos.x, t), Mathf.Lerp(aPos.y, bPos.y, t));
    }

    IEnumerator RandomMovement() {
        while (useRandomMovement) {

            RandomizeMovement();

            if (Physics2D.Linecast(transform.position, (Vector2)transform.position + movement.normalized * bc.size.x, mask)) {
                yield return new WaitForSeconds(Random.Range(0.3f, 1));
            }
            else {
                yield return new WaitForSeconds(Random.Range(2, 5));
            }
            
            
        }
    }

    void RandomizeMovement() {
        Vector2 randomMovement = GetRandomDirection() * speed * Time.deltaTime;
        Vector2 randomMovePos = (Vector2)transform.position + randomMovement;

        movement = randomMovement;

        print("movement: " + movement);
    }

    Vector2 GetRandomDirection() {
        return new Vector2(Random.Range(-100,100), Random.Range(-100, 100)).normalized;
    }
}

