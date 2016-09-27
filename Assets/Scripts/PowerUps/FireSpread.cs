using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CustomLibrary.Collisions;

public class FireSpread : MonoBehaviour {


    private GameObject flame;
    private float speed = 3f;

    public List<GameObject> fireSlaves = new List<GameObject>();
    public GameObject fireSlave;

    private bool didFallThrough = false;
    private bool reachedFloor = false;
    private bool hasFilledFloor = false;

    private int lengthLeft = 0, lengthRight = 0;
    private int currentIndex = 1;
    private float seconds = 0;

    private void Start(){
        //transform.position = new Vector3(Mathf.Floor(transform.position.x) + 0.5f, transform.position.y, transform.position.z);
    }

	
	// Update is called once per frame
	private void Update () {

	    //Val naar beneden
        if (!GoodCollisions.CheckSide(this, Vector2.down, GetComponent<BoxCollider2D>().size.y/2, "Solid")){
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }else {
            if (!reachedFloor) {
                //Rond de positie af op een heel getal
                transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
                CalculateLength();
                reachedFloor = true;
            }else {
                seconds += Time.deltaTime;
                if (!hasFilledFloor && seconds > 0.1) {
                    SpawnFlames();
                    seconds = 0;
                }
            }
        }

	}

    private void CalculateLength()
    {
        Vector3 currentPos = transform.position;

        bool leftFree = false, rightFree = false;


        int i = 0;
        do{
            print("Slag : " + i++);
            //op het moment dat er aan de linkerkant grond is verlengen we links
            if (!leftFree && CheckCollisions(transform.position, Vector3.left, i)) {
                lengthLeft++;
            }
            else { leftFree = true; }


            //op het moment dat er aan de rechterkant grond is verlengen we rechts
            if (!rightFree && CheckCollisions(transform.position, Vector3.right, i)) {
                lengthRight++;
            }
            else { rightFree = true; }
        }
        while (!leftFree || !rightFree);

        //Verander in een fireslave
        Destroy(gameObject, 5);
        GetComponent<Animator>().runtimeAnimatorController = fireSlave.GetComponent<Animator>().runtimeAnimatorController;

        print(string.Format("LengthLeft: {0}, LengthRight: {1}", lengthLeft, lengthRight));
        print(string.Format("LeftFree: {0}, RightFree: {1}", leftFree, rightFree));

    }

    private bool CheckCollisions(Vector3 origin, Vector3 direction, int i) {
        return (GoodCollisions.CheckSide(origin + (direction *(i + 0.01f)), this, Vector2.down, "Solid") && !GoodCollisions.CheckSide(origin + (direction * (i - 0.06f)), this, direction, "Solid")) ;
    }

    private void SpawnFlames() {

        if (currentIndex <= lengthLeft)
            Destroy(Instantiate(fireSlave, transform.position + Vector3.left * currentIndex, Quaternion.identity, transform.parent), 5);

        if (currentIndex <= lengthRight)
            Destroy(Instantiate(fireSlave, transform.position + Vector3.right * currentIndex, Quaternion.identity, transform.parent), 5);

        currentIndex += 1;
    }


}
