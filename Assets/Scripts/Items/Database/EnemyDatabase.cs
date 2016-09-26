using UnityEngine;
using System.Collections;

#if UNITY_EDITOR  
using UnityEditor;
#endif

public class EnemyDatabase : ScriptableObject{

    public Enemy[] enemies;


    [System.Serializable]
    public class Enemy {
        public string name;
        public Sprite sprite;
        public MonoScript enemyScript;
        public Animator animator;
    }

    public static int GetHighestIndex(EnemyDatabase database) {
        return database.enemies.Length - 1;
    }
}


/*
#if UNITY_EDITOR

[CustomEditor(typeof(EnemyDatabase))]
public class EnemyDatabaseEditor : Editor {

    EnemyDatabase database;

    public override void OnInspectorGUI() {
        database = (EnemyDatabase)target;

        DrawDefaultInspector();

        if (GUI.changed) {
            EditorUtility.SetDirty(database);
        }
    }
}
#endif*/