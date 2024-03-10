using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class BYDataTableMaker
{
    [MenuItem("Assets/BY/Create Binanry file from Tab Delimiter(text file)", false, 1)]
    public static void CreateBinaryFile()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            TextAsset csv_file = (TextAsset)obj;
            string table_name = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(csv_file));
            ScriptableObject sc_ab = ScriptableObject.CreateInstance(table_name);
            if (sc_ab == null)
                return;
            AssetDatabase.CreateAsset(sc_ab, "Assets/Resources/Config/" + table_name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            BYDataImport bYDataImport = (BYDataImport)sc_ab;
            bYDataImport.CreateBinaryFile(csv_file);
            EditorUtility.SetDirty(bYDataImport);

        }
    }
}
