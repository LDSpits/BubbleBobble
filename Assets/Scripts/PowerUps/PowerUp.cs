using UnityEngine;
using System.Collections.Generic;

public class PowerUp : MonoBehaviour {

    public Sprite[] spriteList;
    private Animator animator;

    private bool Active = false;

    public enum powerUpType
    {
        water = 0,
        lightning = 1,
        fire = 2,
        fireBall = 3
    }

    private powerUpType type;

    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        type = (powerUpType)Random.Range(0, 4);
        sr.sprite = spriteList[(int)type];

        animator.SetInteger("type", (int)type);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && !Active)
        {
            Active = true;

        }
    }
}
