using UnityEngine;
using System.Collections;

//De unity editor library moet alleen worden geïmporteerd als de unity editor wordt gebruikt
//Zonder deze check kan het project niet worden gebuild en gerund.

#if UNITY_EDITOR  
using UnityEditor;
#endif


public class Item : MonoBehaviour {

    public int itemIndex;

    public bool pickupAble = true;

    public ItemDatabase database;

    float points;

    ItemDatabase.Bonus[] bonusList;

    void Start () {
        OverwriteSelfByDatabaseData();
	}
	
    public void OverwriteSelfByDatabaseData() {
        if (itemIndex < database.items.Length)
        {
            ItemDatabase.Item item = database.items[itemIndex];
            transform.name = item.name;
            GetComponent<SpriteRenderer>().sprite = item.sprite;
            points = item.points;
            bonusList = item.bonus;

            GameObject parentObject = GameObject.Find("Items");
            if (parentObject)
            {
                transform.parent = parentObject.transform;
            }
        }
    }

	void OnTriggerStay2D (Collider2D other) {
	    if(other.tag == "Player" && pickupAble) {
            GameManager.AddScore(PlayerID.player1,(int)points);
            foreach(ItemDatabase.Bonus bonus in bonusList) {
                BonusUtilities.PerformBonus(bonus);
            }
            print("received points: "+points);
            Destroy(gameObject);
        }
	}

    //public IEnumerator UnpickAbleCoroutine(float time) {
    //    print("unpickable coroutine started");
    //    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    //    sr.color -= new Color(0,0,0, 0.6f);

    //    yield return new WaitForSeconds(time);

    //    sr.color += new Color(0, 0, 0, 0.6f);

    //    pickupAble = true;

    //    print("unpickable coroutine ended");
    //}
}

//Deze class hieronder moet alleen worden gebruikt en uitgevoerd als de unity editor wordt gebruikt
//Zonder deze check kan het project niet worden gebuild en gerund.

#if UNITY_EDITOR 
[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{

    int oldItemIndex = -1;

    public override void OnInspectorGUI()
    {
        Item itemComp = (Item)target; //target haalt de component die in de editor wordt bewerkt. (Dus de Item component)

        EditorGUILayout.LabelField(itemComp.transform.name,EditorStyles.boldLabel);
        itemComp.itemIndex = EditorGUILayout.IntSlider("Item Index",itemComp.itemIndex, 0, GetIDBHighestIndex(itemComp));

        if (oldItemIndex!=itemComp.itemIndex)
        {
            itemComp.OverwriteSelfByDatabaseData();
        }
        oldItemIndex = itemComp.itemIndex;

        DrawDefaultInspector();

        if (GUI.changed) {
            EditorUtility.SetDirty(itemComp); //sla de waardes op
        }
    }

    int GetIDBHighestIndex(Item itemComp) //IDB = Item database
    {
        ItemDatabase database = itemComp.database;
        if (database == null || database.items.Length == 0)
        {
            return 100;
        }
        else
        {
            return database.items.Length - 1;
        }
    }
}

#endif