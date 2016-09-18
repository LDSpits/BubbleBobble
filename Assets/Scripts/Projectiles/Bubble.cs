using UnityEngine;

public class Bubble : MonoBehaviour {

    private float speed;
    private SpriteRenderer sr;
    private Animator animator;
    
    private float seconds;
    private float timePassed = 0;

    public Vector2 direction;
	
    // Use this for initialization
    void Start () {
        transform.position = GameObject.Find("Player").transform.position;
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        speed = direction.x * 15; //Bubbel lanceer-snelheid

        seconds = 0;
        AudioManager.PlaySound(AudioManager.Sounds.BubbleShot);
	}

    void Update()
    {
		seconds += Time.deltaTime;

        if (Mathf.Abs(speed) <= 0.4f) { //Als we te langzaam gaan, stop dan af
            speed = 0;
        }else if (speed > 0) { //Naar rechts
            speed -= 0.4f;
            transform.Translate(direction * speed * Time.deltaTime);
        }else if (speed < 0) { //Naar links
            speed += 0.4f;
            transform.Translate(direction * -speed * Time.deltaTime);
        }

        /*
        //als de horizontale shelheid meer is dan de berekening van de huidige richting - de huidige snelheid
        if (rb.velocity.x > 0 ) 
        {
            if (rb.velocity.x < 0.2)
                rb.velocity = new Vector2(0, rb.velocity.y);
            else
                rb.velocity = new Vector2(rb.velocity.x - 0.1f, rb.velocity.y);
        }

        if (rb.velocity.x < 0)
        {
            if(rb.velocity.x > -0.8)
                rb.velocity = new Vector2(0, rb.velocity.y);
            else
                rb.velocity = new Vector2(rb.velocity.x + 0.1f, rb.velocity.y);
        }*/

        //Vernietig jezelf na een bepaalde tijd
        if (seconds > 2)
            animator.SetInteger("status", 1);
        if (seconds > 4)
            animator.SetInteger("status", 2);
        if (seconds > 6)
        {
            animator.SetInteger("status", 3);
            //Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(SortingLayer.GetLayerValueFromName("Default"))[0].clip.averageDuration); Nee
            Destroy(gameObject, 1);
        }

            
    }
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy") && speed != 0)
        {
            //Verkrijg het cavemonster script van de enemy
            CaveMonster caveMonsterScript = coll.GetComponent(typeof(CaveMonster)) as CaveMonster;

            caveMonsterScript.Capture(); //'Capture' de vijand, zet de vijand in bubbelmodus.

            Destroy(gameObject);
        }
        else if (coll.CompareTag("Player") && speed == 0) { //Speler raakt ons aan, ga direct kapot
            seconds = 0;
            animator.SetInteger("status", 4);
            Destroy(gameObject, 0.5f);
        }
    }


}

