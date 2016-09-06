using UnityEngine;
using CustomLibrary;

public class Bubble : MonoBehaviour {

    private Rigidbody2D rb;
    
    private float seconds;
    private float timePassed = 0;
    private GameObject capturedEnemy;

    public Vector2 direction;
	
    // Use this for initialization
    void Start () {

        if(direction == null)
        {
            print("error: no direction given!");
            Destroy(gameObject);
        }

        transform.position = GameObject.Find("Player").transform.position;
        rb = GetComponent<Rigidbody2D>();
		rb.velocity =  direction * 10;
        seconds = 0;
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

        //na 3 seconden verplaats naar boven
        if (seconds > 3)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(10,9), 3 * Time.deltaTime);
            rb.velocity = new Vector2(0, -0.1f);
        }

        //na 7 seconden & geen vijand gevangen vernietig jezelf
        if (seconds > 7 && !capturedEnemy)
            Destroy(gameObject);

        //als er een vijand is gevangen
        if (capturedEnemy)
        {
            timePassed += Time.deltaTime;

            //en de gepasseerde tijd nadat de vijand is gevangen hoger is dan 5 seconden
            if(timePassed > 5)
            {
                capturedEnemy.transform.parent = null; //vernietig de bubbel en geef de vijand controle terug
                capturedEnemy.SetActive(true);
                GameObject.Destroy(gameObject);
            }
        }
    }
	
	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy") && !capturedEnemy)
        {
            capturedEnemy = coll.gameObject;
            gameObject.GetComponent<SpriteRenderer>().sprite = capturedEnemy.GetComponent<CaveMonster>().capturedSprite;
            capturedEnemy.SetActive(false);
            capturedEnemy.transform.SetParent(transform);
            capturedEnemy.transform.localPosition = new Vector2();
            transform.position = Vector2.Lerp(transform.position, new Vector2(10, 9), 3 * Time.deltaTime);
        }

        if (coll.gameObject.CompareTag("Player") && capturedEnemy)
            Destroy(gameObject);

    }
}
