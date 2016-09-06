using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public float ease = 0.5f;
    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = Vector2.Lerp(transform.position, player.position, Time.deltaTime * ease);
	}
}
