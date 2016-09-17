using UnityEngine;

public class Bubble : MonoBehaviour {

    public Sprite almostPopped;
    public Sprite almostPopped2;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    
    private float seconds;
    private float timePassed = 0;
    private GameObject capturedEnemy;

    public Vector2 direction;
	
    // Use this for initialization
    void Start () {

        if(direction == Vector2.zero)
        {
            print("error: no direction given!");
            Destroy(gameObject);
        }

        transform.position = GameObject.Find("Player").transform.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
		rb.velocity =  direction * 10;
        seconds = 0;
        AudioManager.PlaySound(AudioManager.Sounds.BubbleShot);
	}

    void Update()
    {
		seconds += Time.deltaTime;

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
        }

        //na 3 seconden & geen vijand gevangen vernietig jezelf
        if (seconds > 2 && !capturedEnemy)
            animator.SetInteger("status", 1);
        if (seconds > 4 && !capturedEnemy)
            animator.SetInteger("status", 2);
        if (seconds > 6 && !capturedEnemy)
        {
            animator.SetInteger("status", 3);
            Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(SortingLayer.GetLayerValueFromName("Default"))[0].clip.averageDuration);
        }
            

        //als er een vijand is gevangen
        if (capturedEnemy)
        {
            //meet de tijd op
            timePassed += Time.deltaTime;

            //als er 2 seconden voorbij zijn
            if(timePassed > 2)
            {
                //verplaats naar boven en 
                transform.position = Vector2.Lerp(transform.position, new Vector2(10, 9), 3 * Time.deltaTime);
                rb.velocity = new Vector2(0, -0.1f);
            }
            
            //en de gepasseerde tijd nadat de vijand is gevangen hoger is dan 5 seconden
            if (timePassed > 5)
            {
                capturedEnemy.transform.parent = null; //vernietig de bubbel en geef de vijand controle terug
                capturedEnemy.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy") && !capturedEnemy)
        {
            AudioManager.PlaySound(AudioManager.Sounds.BubbleCapture);
            capturedEnemy = coll.gameObject;
            gameObject.GetComponent<SpriteRenderer>().sprite = capturedEnemy.GetComponent<CaveMonster>().capturedSprite;
            capturedEnemy.SetActive(false);
            capturedEnemy.transform.SetParent(transform);
            capturedEnemy.transform.localPosition = Vector2.zero;
            transform.position = Vector2.Lerp(transform.position, new Vector2(10, 9), 3 * Time.deltaTime);
        }

        if (coll.gameObject.CompareTag("Player") && capturedEnemy)
            Destroy(gameObject);

    }
}
