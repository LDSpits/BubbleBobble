using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public int itemIndex;

    public ItemDatabase database;

    float points;

    ItemDatabase.Bonus[] bonusList;

    void Start () {
        OverwriteSelfByDatabaseData();
	}
	
    void OverwriteSelfByDatabaseData() {
        ItemDatabase.Item item = database.items[itemIndex];      
        transform.name = item.name;
        GetComponent<SpriteRenderer>().sprite = item.sprite;
        points = item.points;
        bonusList = item.bonus;

    }

	void OnTriggerEnter2D (Collider2D other) {
	    if(other.tag == "Player") {
            foreach(ItemDatabase.Bonus bonus in bonusList) {
                BonusUtilities.PerformBonus(bonus);
            }
            print("received points: "+points);
            Destroy(gameObject);
        }
	}
}
