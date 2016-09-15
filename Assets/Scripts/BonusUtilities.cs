using UnityEngine;
using System.Collections;

public static class BonusUtilities{


    public static void PerformBonus(ItemDatabase.BonusType special, float bonusValue) {
        switch (special) {
            case ItemDatabase.BonusType.extraLife:
                int lifes = Mathf.RoundToInt(bonusValue);
                if (lifes > 0) {
                    Debug.Log("Extra Life, value: "+lifes);
                }
                break;
            case ItemDatabase.BonusType.scoreMultiplier:
                Debug.Log("Score multiplied, value: "+bonusValue);
                break;
        }
    }

    public static void PerformBonus(ItemDatabase.Bonus bonus) {
        switch (bonus.bonus_type) {
            case ItemDatabase.BonusType.extraLife:
                int lifes = Mathf.RoundToInt(bonus.bonus_value);
                if (lifes > 0) {
                    Debug.Log("Extra Life, value: " +lifes);
                }
                break;
            case ItemDatabase.BonusType.scoreMultiplier:
                Debug.Log("Score multiplied, value: "+ bonus.bonus_value);
                break;
        }
    }
}
