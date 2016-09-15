using UnityEngine;
using CustomLibrary.Collisions;
using System.Collections.Generic;

public class WaterSnake : MonoBehaviour {

    public List<GameObject> waterBlocks = new List<GameObject>();
    public GameObject waterBlockFollowerPrefab;

	void Start () {
        waterBlocks.Add(gameObject); //Voeg eerst de 'kop' toe
	}
	
	void Update () {
        if (!GoodCollisions.CheckSide(this, Vector2.down, "Solid"))
        {
            //transform.position += Vector3.down * 0.05f;
        }
        else //We raken de grond
        {
            if (!GoodCollisions.CheckSide(this, Vector2.left, "Solid"))
            {
                //transform.position += Vector3.left * 0.05f;
            }
        }

    }

    private void Follow(){  
        //Bewaar de positie voordat we begen voor elke iteratie
        Vector3 lastPos = transform.position;
        Quaternion lastRot = transform.rotation;
        Sprite lastSprite = GetComponent<SpriteRenderer>().sprite;

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
    }

    private void AddWaterBlock()
    {
        GameObject block = Instantiate(waterBlockFollowerPrefab) as GameObject;
        block.transform.parent = transform.parent;
        waterBlocks.Add(block);
    }

}
