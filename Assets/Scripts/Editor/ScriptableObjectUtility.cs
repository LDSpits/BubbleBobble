using UnityEngine;
using UnityEditor;
using System.IO;

//!! Deze script MOET in een folder met de naam "Editor", anders werkt het niet  !!

//#####
//Deze script is gemaakt door een gebruiker van Unity 3D wiki
//De functie in deze script maakt een scriptableobject in de asset, deze asset kan je als een database gebruiken
//-----

public static class ScriptableObjectUtility {
    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static void CreateAsset<T>() where T : ScriptableObject {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "") {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "") {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}