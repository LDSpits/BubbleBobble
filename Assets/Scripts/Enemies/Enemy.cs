using UnityEngine;
using System.Collections;
using CustomLibrary.Collisions;

#if UNITY_EDITOR  
using UnityEditor;
#endif

public class Enemy : MonoBehaviour {

	//Items
	[HideInInspector]public ItemDatabase database;
	[HideInInspector]public GameObject itemPrefab;
	[HideInInspector]public int itemIndex = -1;

    //Speed
    protected float speed;

    public float defaultSpeed;

    //Angry vars
    protected bool angry; //Gebruik SetAngryState() om de waarde te veranderen.

    [Range(1, 5)]
    public float angrySpeedMultiplier;

    //Componenten
    protected Rigidbody2D rb;
	protected BoxCollider2D bc;
	protected SpriteRenderer sr;
	protected Animator animator;

    //Capture and death
    protected bool captured;
    protected bool defeated;
    protected float timePassed;

	protected virtual void Start () {
        speed = defaultSpeed;
		GetAllComponents ();
		IgnoreCollidersOfOtherEnemies ();
	}

	protected virtual void Update () {
		if (defeated) {
			DefeatOutput ();
		}
		else if(captured){
			CaptureOutput ();
		}
        else {
            DefaultActions();
        }
	}

	//Defeat functies
	//--

    public enum KilledBy {
        defaultKill,
        waterfall,
        fireball,
        lighting,
        fire
    }

	public void Defeat(KilledBy killedByAction = KilledBy.defaultKill) {

        switch (killedByAction) {
            case KilledBy.fire:
                itemIndex = 0;
                print("Killed by fire");
                break;
            case KilledBy.fireball:
                itemIndex = 1;
                print("Killed by fireball");
                break;
            case KilledBy.waterfall:
                itemIndex = 2;
                print("Killed by waterfall");
                break;
            case KilledBy.lighting:
                itemIndex = 3;
                print("Killed by lighting");
                break;
        }

		//We zijn verslagen, nu vallen we naar beneden totdat we de grond raken, dan droppen we een item
		defeated = true;
		animator.SetBool("defeated", true);
	}

    protected void DefeatOutput(){
        if (!GoodCollisions.CheckSide(this, Vector2.down, "Solid")) {
            transform.Translate(Vector3.down * 2 * Time.deltaTime);
        }
        else { //Grond geraakt, spawn een item en vernietig jezelf
            SpawnItem();
            Destroy(gameObject);
        }
    }

	public void SpawnItem() {
		if (itemPrefab != null &&itemIndex != -1) {
			GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity) as GameObject;
			Item itemComp = item.GetComponent<Item>();
			itemComp.itemIndex = itemIndex;
		}
	}

	//--
	//--------------

	//Capture functies
	//--
	public void Capture() {
		this.captured = true;
		rb.isKinematic = true;
		bc.isTrigger = true; //Collider uitschakelen
		animator.SetBool("captured", true);
		timePassed = 0;
	}

    protected void CaptureOutput(){
		timePassed += Time.deltaTime;

		//Beweeg omhoog
		if (!GoodCollisions.CheckSide(this, Vector2.up, "Solid")) {
			transform.Translate(Vector3.up * Time.deltaTime);
		}

		//Als de gepasseerde tijd nadat we zijn gevangen hoger is dan 5 seconden, ontsnappen we
		if (timePassed > 5) {
			captured = false;
			rb.isKinematic = false; //Rigidbody wakkerschudden
			bc.isTrigger = false; //Collider aanzetten
			animator.SetBool ("captured", false);
            SetAngryState(true);
		}
	}

	protected virtual void DefaultActions(){
		print ("Warning: using DefaultActions() of the Enemy base class, use an overriden function of a subclass");
	}
	public bool IsCaptured {
		get { return captured; }
	}
    //--
    //---------------


    //Functies voor "angry state"
    //--

    protected void SetAngryState(bool isAngry) {
        angry = isAngry;
        animator.SetBool("isAngry", isAngry);

        if (isAngry) {
            speed *= angrySpeedMultiplier;
        }
        else {
            speed = defaultSpeed;
        }
    }

    //--
    //------

    //Start functies
    //--
    protected void GetAllComponents(){
		rb = GetComponent<Rigidbody2D> ();
		bc = GetComponent<BoxCollider2D> ();
		sr = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator> ();
	}

    protected void IgnoreCollidersOfOtherEnemies(){
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			Collider2D otherColl = enemy.GetComponent<Collider2D>();
			Physics2D.IgnoreCollision(bc, otherColl);
		}
	}

	//--
	//-----------------
}

//Deze class hieronder moet alleen worden gebruikt en uitgevoerd als de unity editor wordt gebruikt
//Zonder deze check kan het project niet worden gebuild en gerund.

#if UNITY_EDITOR

[CustomEditor(typeof(Enemy),editorForChildClasses:true)]
public class EnemyEditor : Editor {

    public override void OnInspectorGUI() {
        Enemy monsterComp = (Enemy)target; //target haalt de component die in de editor wordt bewerkt. (Dus de Item component)

        EditorGUILayout.LabelField("Items", EditorStyles.boldLabel);

        monsterComp.database = EditorGUILayout.ObjectField("Item Database", monsterComp.database, typeof(ItemDatabase), false) as ItemDatabase;

        if (monsterComp.database != null) {
            int highestIDBIndex = ItemDatabase.GetHighestIndex(monsterComp.database);
            if (monsterComp.itemIndex != -1) { //Index -1 is "geen item";
                if (monsterComp.itemIndex > highestIDBIndex) {
                    monsterComp.itemIndex = highestIDBIndex;
                }
                EditorGUILayout.LabelField(monsterComp.database.items[monsterComp.itemIndex].name, EditorStyles.boldLabel);
            }
            monsterComp.itemPrefab = EditorGUILayout.ObjectField("Item Prefab", monsterComp.itemPrefab, typeof(GameObject), false) as GameObject;
            monsterComp.itemIndex = EditorGUILayout.IntSlider("Item Index", monsterComp.itemIndex, -1, highestIDBIndex);
        }
        else {
            monsterComp.itemIndex = -1;
        }

        EditorGUILayout.LabelField("Default Inspector", EditorStyles.boldLabel);

        DrawDefaultInspector();

        if (GUI.changed) {
            EditorUtility.SetDirty(monsterComp); //sla de waardes op
        }
    }
}

#endif



