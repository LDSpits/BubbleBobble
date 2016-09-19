using UnityEngine;
using System.Collections;
using CustomLibrary.Collisions;

public class FireSpread : MonoBehaviour {


    private GameObject flame;
    private float speed;

    // Use this for initialization
    private void Start () {
        SpawnFlames();
    }
	
	// Update is called once per frame
	private void Update () {

	    //Val naar beneden
        if (GoodCollisions.CheckSide(this, Vector2.down, "Solid")){
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }



	}

    private void SpawnFlames()
    {
        Vector3 currentPos = transform.position;
        float lengthLeft = 0, lengthRight = 0;

        bool leftFree = false, rightFree = false;


        int i = 1;
        do
        {
            print("Slag : " + i++);
            //op het moment dat er aan de linkerkant grond is verlengen we links
            if (!leftFree && GoodCollisions.CheckSide(currentPos + (Vector3.left * i), this, Vector2.down, "Solid"))
            {
                lengthLeft++;
            }
            else leftFree = true;

            //op het moment dat er aan de rechterkant grond is verlengen we rechts
            if (!rightFree && GoodCollisions.CheckSide(currentPos + (Vector3.right * i), this, Vector2.down, "Solid"))
            {
                lengthRight++;
            }
            else rightFree = true;
        }
        while (!leftFree && !rightFree);

        print(i * lengthLeft + lengthRight);


        print(string.Format("LeftFree: {0}, RightFree: {1}", leftFree, rightFree));


    }


}
