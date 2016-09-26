using UnityEngine;
using UnityEditor;
using System.IO;

//!! Deze script MOET in een folder met de naam "Editor", anders werkt het niet  !!

//#####
//Deze script is gemaakt door een gebruiker van Unity 3D wiki, aangepast door Audi van Gog
//-----

public class DataBaseAssets {
    [MenuItem("Assets/Create/Item Database")]
    public static void CreateItemDatabase() {
        ScriptableObjectUtility.CreateAsset<ItemDatabase>();
    }

    [MenuItem("Assets/Create/Enemy Database")]
    public static void CreateEnemyDatabase() {
        ScriptableObjectUtility.CreateAsset<EnemyDatabase>();
    }

    //Extra
    [MenuItem("Assets/Create/Text File")]
    public static void CreateTextFile() {
        //Om een of andere reden moet ik zowel voor als na het maken van de text bestand de assetdatabase refreshen om het text asset tevoorschijn te krijgen.
        AssetDatabase.Refresh();
        string path = "Assets/newTextFile.txt";
        if (System.IO.File.Exists("Assets/newTextFile.txt")) {
            int fileNumber = 2;
            while (System.IO.File.Exists("Assets/newTextFile"+fileNumber+".txt")) {
                fileNumber++;
            }
            path = "Assets/newTextFile" + fileNumber + ".txt";
        }
        System.IO.File.WriteAllText(path, "");
        AssetDatabase.Refresh();
    }
}