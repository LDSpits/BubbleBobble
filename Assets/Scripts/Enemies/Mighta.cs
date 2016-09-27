using UnityEngine;
using System.Collections;

public class Mighta : Zenchan {

    public GameObject rockPrefab;
    public Vector2 rockSpawnOffset;

    private bool isThrowing;
    public bool noMoveOnThrow = true;

    public LayerMask mask;

    public float targetMaxDistanceToThrow = 4;

    public float throwCooldown = 2;

    [Range(0.1f,1)]
    public float throwCooldown_angryMultiplier;

	new void Start () {
        base.Start();
	}
	
	new void Update () {
        base.Update();
	}

    Coroutine throwCoroutine;

    protected override void DefaultActions() {
        print("isThrowing: "+isThrowing);
        if (!isThrowing) {
            print("allowThrow: " + AllowThrow());
            if (AllowThrow()&&throwCoroutine==null) {
                throwCoroutine = StartCoroutine(ThrowCoroutine());
            }
            else {
                print("base defaultActions");
                base.DefaultActions();
                return;
            }
            if (!noMoveOnThrow) {
                print("base defaultActions");
                base.DefaultActions();
            }
        }
        else if (!noMoveOnThrow) {
            print("base defaultActions");
            base.DefaultActions();
        }
    }

    private IEnumerator ThrowCoroutine() {

        isThrowing = true;
        animator.SetBool("throw", true);
        print("throw is true");

        yield return new WaitUntil(() => this.CurrentAnimTag("Throw"));

        isThrowing = false;
        animator.SetBool("throw", false);
        Throw();

        print("throwcoroutine ended");

        if (angry){
            yield return new WaitForSeconds(throwCooldown * throwCooldown_angryMultiplier);
        }
        else{
            yield return new WaitForSeconds(throwCooldown);
        }
        

        throwCoroutine = null;
    }

    private void Throw() {
        GameObject rock = Instantiate(rockPrefab, (Vector2)transform.position + rockSpawnOffset, Quaternion.identity) as GameObject;
        rock.GetComponent<Rock>().direction = (sr.flipX) ? Vector2.right : Vector2.left;
    }

    private bool CurrentAnimTag(string tag) {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag(tag);
    }

    private bool AllowThrow(){
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target) {
            float dist = targetMaxDistanceToThrow;
            Vector2 dir = (target.transform.position.x < transform.position.x) ? Vector2.left : Vector2.right;

            sr.flipX = (dir == Vector2.right)? true : false;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,dir,dist,-1);

            if(FindTargetInHits(hits, target)) {
                return true;
            }
        }
        return false;
    }

    private bool FindTargetInHits(RaycastHit2D[] hits, GameObject target) {
        foreach(RaycastHit2D hit in hits) {
            if(hit.transform.gameObject == target) {
                return true;
            }
        }
        return false;
    }
}
