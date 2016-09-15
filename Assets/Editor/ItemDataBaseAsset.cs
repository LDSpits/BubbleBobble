using UnityEngine;
using UnityEditor;

//!! Deze script MOET in een folder met de naam "Editor", anders werkt het niet  !!

//#####
//Deze script is gemaakt door een gebruiker van Unity 3D wiki
//-----

public class ItemDataBaseAsset {
    [MenuItem("Assets/Create/Item Database")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<ItemDatabase>();
    }
}