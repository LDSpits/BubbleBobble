using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    public Vector2 direction;

    public float speed = 10;

    private bool destroyed;

    Animator animator;

    void OnTriggerEnter2D(Collider2D otherColl)
    {
        //if (otherColl.tag == "Player")
        //{
        //    print("Audi pls fix, iz nie goe");
        //    GameManager.DecreaseLife(otherColl.GetComponent<Player>().ThisPlayer);
        //}
        /*else*/ if(otherColl.tag == "Solid")
        {
            destroyed = true;
        }
    }

	void Start () {
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        if (!destroyed){
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if (CurrentAnimName("DestroyedFinal"))
        {
            Destroy(gameObject);
        }

        
	}
    private bool CurrentAnimName(string name) {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
