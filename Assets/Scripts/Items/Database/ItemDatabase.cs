using UnityEngine;
using System.Collections;

public class ItemDatabase : ScriptableObject {

    public Item[] items;

    public enum BonusType {
        extraLife,
        scoreMultiplier
    }


    [System.Serializable]
    public class Item {
        public string name;
        public int points;
        public Sprite sprite;
        public Bonus[] bonus;
    }

    [System.Serializable]
    public class Bonus {
        public BonusType bonus_type;
        public float bonus_value;
    }
}
