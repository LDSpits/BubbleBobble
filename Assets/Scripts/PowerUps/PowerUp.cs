using UnityEngine;
using System.Collections.Generic;

public class PowerUp : MonoBehaviour {

    private Animator animator;

    public GameObject waterEffectPrefab, lightningEffectPrefab, fireEffectPrefab;

    private bool active = false;

    public enum powerUpType
    {
        water = 0,
        lightning = 1,
        fire = 2,
        fireBall = 3
    }

    [SerializeField]
    private powerUpType type;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        type = (powerUpType)Random.Range(0, 4);

        animator.SetInteger("type", (int)type);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && !active)
        {
            active = true;

            if (type == 0) //Watereffect
            {
                Instantiate(waterEffectPrefab, transform.position, Quaternion.identity);
            }else if((int)type == 1) //lightning
            {

            }else if((int)type == 2) //fire
            {

            }else if((int)type == 3) //fireBall
            {

            }

            Destroy(gameObject);
        }
    }
}
