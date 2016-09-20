using UnityEngine;
using CustomLibrary.Collisions;

#if UNITY_EDITOR  
using UnityEditor;
#endif

public class CaveMonster : MonoBehaviour {

    public ItemDatabase database;
    public GameObject itemPrefab;
    public int itemIndex = -1;

    public float speed = 5;
    public float jumpSpeed = 5;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private Animator animator;

    private bool captured;
    private bool defeated;
    private float timePassed;

    public MoveDirection startDirection;
    private MoveDirection direction;

    public void SetDirection(MoveDirection direction) {
        this.direction = direction;
    }

    //Voor wall checks in/uitschakelen
    public bool checkWalls = true;

    //Voor random springen in/uitschakelen
    public bool randomJumping = true;

    public enum MoveDirection {
        idle = 0,
        left = 1,
        right = 2,
        randomHorizontal = 3
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        direction = startDirection;
        //Kies aan het begin een random horizontale positie als dit zo is ingesteld
        if (direction == MoveDirection.randomHorizontal) { direction = (MoveDirection)Random.Range(1, 3); }

        animator = GetComponent<Animator>();

        //Zorg ervoor dat enemies elkaar negeren
        Collider2D coll = GetComponent<Collider2D>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Collider2D otherColl = enemy.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(coll, otherColl);
        }
    }

    public void SpawnItem() {
        
        if (database != null && itemIndex != -1) {
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity) as GameObject;
            Item itemComp = item.GetComponent<Item>();
            itemComp.itemIndex = itemIndex;
            //itemComp.pickupAble = false;
            //itemComp.StartCoroutine(itemComp.UnpickAbleCoroutine(1));
        }

    }

    public void Defeat(bool dropItem) {
        //We zijn verslagen, nu vallen we naar beneden totdat we de grond raken, dan droppen we een item
        defeated = true;
        animator.SetBool("defeated", true);
    }

    void Update() {

        if (defeated) { //We zijn verslagen

            if (!GoodCollisions.CheckSide(this, Vector2.down, "Solid")) {
                transform.Translate(Vector3.down * 2 * Time.deltaTime);
            }
            else { //Grond geraakt, spawn een item en vernietig jezelf
                SpawnItem();
                Destroy(gameObject);
            }
        }
        else if (captured) { //We zijn in bubbel mode
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
                    animator.SetBool("captured", false);
                }
        }
        else { //Loop rond

            if (randomJumping) { //Doe een random jump als dit aanstaat
                JumpRandomizer();
            }

            if (checkWalls) { //Verander de richting alleen als checkWalls aanstaat
                if (direction == MoveDirection.left && GoodCollisions.CheckSide(this, Vector2.left, "Solid")) { //Naar Rechts
                    direction = MoveDirection.right;
                }
                else if (direction == MoveDirection.right && GoodCollisions.CheckSide(this, Vector2.right, "Solid")) { //Naar Links
                    direction = MoveDirection.left;
                }
            }

            //Beweeg!
            Move(direction);

        }
    }

    private void Move(MoveDirection moveDir) {
        //Bepaal welke richting we op gaan
        Vector2 dir = Vector2.zero;
        switch (moveDir) {
            case MoveDirection.left:  dir = Vector2.left;  sr.flipX = false; break; //L
            case MoveDirection.right: dir = Vector2.right; sr.flipX = true;  break; //R
            default: return;
        }

        Vector2 movement = dir * speed * Time.deltaTime;
        transform.Translate(movement); //Beweeg
    }

    public bool OnGround {
        //Controleer of we op de grond staan
        get { return GoodCollisions.CheckSide(this, Vector2.down, "Solid"); }
    }

    private void JumpRandomizer() {
        //Spring op een willekeurig moment de lucht in
        float randomFloat = Random.Range(0, 500);
        if (randomFloat < 10 && OnGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    public bool IsCaptured {
        get { return captured; }
    }

    public void Capture() {
        this.captured = true;
        rb.isKinematic = true;
        bc.isTrigger = true; //Collider uitschakelen
        animator.SetBool("captured", true);
        timePassed = 0;
    }

    //Deze class hieronder moet alleen worden gebruikt en uitgevoerd als de unity editor wordt gebruikt
    //Zonder deze check kan het project niet worden gebuild en gerund.

#if UNITY_EDITOR

    [CustomEditor(typeof(CaveMonster))]
    public class CaveMonsterEditor : Editor {

        public override void OnInspectorGUI() {
            CaveMonster monsterComp = (CaveMonster)target; //target haalt de component die in de editor wordt bewerkt. (Dus de Item component)

            monsterComp.database = EditorGUILayout.ObjectField("Item Database", monsterComp.database, typeof(ItemDatabase), false) as ItemDatabase;

            if (monsterComp.database != null) {
                if (monsterComp.itemIndex != -1) {
                    if (monsterComp.itemIndex > GetIDBHighestIndex(monsterComp)) {
                        monsterComp.itemIndex = GetIDBHighestIndex(monsterComp);
                    }
                    EditorGUILayout.LabelField(monsterComp.database.items[monsterComp.itemIndex].name, EditorStyles.boldLabel);
                }
                monsterComp.itemPrefab = EditorGUILayout.ObjectField("Item Prefab", monsterComp.itemPrefab, typeof(GameObject), false) as GameObject;
                monsterComp.itemIndex = EditorGUILayout.IntSlider("Item Index", monsterComp.itemIndex, -1, GetIDBHighestIndex(monsterComp));
            }


            DrawDefaultInspector();

            if (GUI.changed) {
                EditorUtility.SetDirty(monsterComp); //sla de waardes op
            }
        }

        int GetIDBHighestIndex(CaveMonster monsterComp) //IDB = Item database
        {
            if (monsterComp.database == null) {
                return 100;
            }
            return monsterComp.database.items.Length - 1;
        }
    }

#endif
}

