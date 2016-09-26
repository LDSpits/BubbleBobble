using UnityEngine;
using CustomLibrary.Collisions;
using System.Collections.Generic;
using System;

public class WaterSnake : MonoBehaviour {

    private Vector3 direction;
    private float seconds = 0;
    private bool hasDestination = false;
    private SpriteRenderer sr;
    private Sprite bodySprite;

    public Sprite waterHeadStandard, waterFallingBody, waterTurning;
    public float distance = 0.5f;

    public List<GameObject> waterBlocks = new List<GameObject>();
    public GameObject waterBlockFollowerPrefab;

    void Start () {

        AudioManager.PlaySound(AudioManager.Sounds.WaterFlow);

        transform.position = new Vector3(transform.position.x, (float)Math.Round(transform.position.y) + 0.25f ,transform.position.z);

        sr = GetComponent<SpriteRenderer>();

        direction = Vector3.down;

        waterBlocks.Add(gameObject); //Voeg eerst de 'kop' toe
        for(int i=0; i<10; i++) {
            AddWaterBlock();
        }
    }
	
	void Update () {

        seconds += Time.deltaTime;

        if(seconds < 0.03f)
        {
            return;
        }
        seconds = 0;
        Follow();

        if (!GoodCollisions.CheckSide(this, Vector2.down,0.5f,"Solid"))
        {
            //Op de eerste frame dat er geen grond onder ons is
            if (hasDestination == true) {
                waterBlocks[1].GetComponent<SpriteRenderer>().sprite = waterTurning; //Geef de body de turning sprite
                waterBlocks[1].transform.eulerAngles = CalcBodyRotation(true);
            }

            transform.position += Vector3.down * 0.5f;
            transform.eulerAngles = new Vector3(0,0,180);
            bodySprite = waterFallingBody;
            hasDestination = false;
        }
        else //We raken de grond
        {
            //als die geen richting heeft
            if (!hasDestination)
            {
                //kies een richting
                direction = Choose();
                waterBlocks[1].GetComponent<SpriteRenderer>().sprite = waterTurning;
                waterBlocks[1].transform.eulerAngles = CalcBodyRotation(false);

                //bepaal rotatie voor het hoofd van de slang aan de hand van de richting
                if (direction == Vector3.left)
                    transform.eulerAngles = new Vector3(0,0,90);
                else if (direction == Vector3.right)
                    transform.eulerAngles = new Vector3(0, 0, 270);

                hasDestination = true;
                transform.position += direction * 0.5f;
            }
            else
            {
                sr.sprite = waterHeadStandard;
                bodySprite = waterFallingBody;
                
                transform.position += direction * 0.5f;
            }
        }
    }

    private Vector2 Choose()
    {
        Vector3 currentPos = transform.position;
        for (int i = 1; i < 11; i++)
        {
            //op het moment dat aan de linkerkant een gat is
            if (!GoodCollisions.CheckSide(currentPos + (Vector3.left * i), this, Vector2.down, "Solid"))
            {
                return Vector2.left;
            }

            //op het moment dat aan de rechterkant een gat is
            if (!GoodCollisions.CheckSide(currentPos + (Vector3.right * i), this, Vector2.down, "Solid"))
            {
                return Vector2.right;
            }
        }
        return Vector2.down;
    }

    private Vector3 CalcHeadRotation()
    {
        if (direction == Vector3.left) {
            return new Vector3(0, 0, 90); //Links
        }

        return new Vector3(0, 0, 270); //Rechts
    }

    private Vector3 CalcBodyRotation(bool goingDown)
    {
        if (goingDown) {
            if (direction == Vector3.left) {
                return new Vector3(0, 0, 0); //Links
            }
            else {
                return new Vector3(0, 0, 270); //Rechts
            }
        }

        if (direction == Vector3.left) {
            return new Vector3(0, 0, 180); //Links
        }

        return new Vector3(0, 0, 90); //Rechts
    }

    private void Follow(){  
        //Bewaar de positie voordat we begen voor elke iteratie
        Vector3 lastPos = transform.position;
        Quaternion lastRot = transform.rotation;
        Sprite lastSprite = bodySprite;

        for (int i = 1; i < waterBlocks.Count; i++){ //Begin vanaf index 1, volg de kop
            //Sla de huidige positie op als laatste positie
            Vector3 currentPos = waterBlocks[i].transform.position;
            Quaternion currentRot = waterBlocks[i].transform.rotation;
            Sprite currentSprite = waterBlocks[i].GetComponent<SpriteRenderer>().sprite;

            //Volg het blok van 1 index voor ons
            waterBlocks[i].transform.position = lastPos;
            waterBlocks[i].transform.rotation = lastRot;
            waterBlocks[i].GetComponent<SpriteRenderer>().sprite = lastSprite;

            lastPos = currentPos;
            lastRot = currentRot;
            lastSprite = currentSprite;
        }

        //waterBlocks[waterBlocks.Count - 1].GetComponent<SpriteRenderer>().sprite = waterHeadStandard; //< Laatste deel van de slang sprit


    }

    private void AddWaterBlock()
    {
        GameObject block = Instantiate(waterBlockFollowerPrefab) as GameObject;
        block.transform.parent = transform.parent;
        block.transform.position = transform.position;
        block.transform.localScale = new Vector3(1.01f, 1.01f);
        waterBlocks.Add(block);
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        //Sleur mee
    }


}
