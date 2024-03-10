using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Profiling;

public class BYDataImport:ScriptableObject
{
    public virtual void CreateBinaryFile(TextAsset csv_file)
    { 
    
    }

}
public class ConfigCompare<T> : IComparer<T> where T : class, new()
{
    private List<FieldInfo> key_infos = new List<FieldInfo>();
    public ConfigCompare(params string[] key_info_names)
    {
        for (int i = 0; i < key_info_names.Length; i++)
        {
            FieldInfo key_info = typeof(T).GetField(key_info_names[i], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            key_infos.Add(key_info);
        }
    }
    public int Compare(T x, T y)
    {
        int result = 0;
        for (int i = 0; i < key_infos.Count; i++)
        {
            object val_x = key_infos[i].GetValue(x);
            object val_y = key_infos[i].GetValue(y);
            result = ((IComparable)val_x).CompareTo(val_y);
            if (result != 0)
            {
                break;
            }
        }
        return result;
    }
    public T SetValueSearch(params object[] values)
    {
        T key = new T();
        for (int i = 0; i < values.Length; i++)
        {
            key_infos[i].SetValue(key, values[i]);
        }
        return key;
    }
}
public abstract class BYDataTable<T> : BYDataImport where T : class, new()
{
    public List<T> records = new List<T>();
    protected ConfigCompare<T> configCompare;
    private void OnEnable()
    {
        DefindCompare();
    }
    public abstract ConfigCompare<T> DefindCompare();
    public override void CreateBinaryFile(TextAsset csv_file)
    {
        base.CreateBinaryFile(csv_file);
        List<List<string>> grid= SplitCSVFile(csv_file);
        Type record_type= typeof(T);
        FieldInfo[] fieldInfos = record_type.GetFields(BindingFlags.NonPublic| BindingFlags.Public| BindingFlags.Instance);
        for (int i = 1; i < grid.Count; i++)
        {
            List<string> line_data = grid[i];
            string jsontext = "{";
            for (int f = 0; f < fieldInfos.Length; f++)
            {
                if (f > 0)
                    jsontext += ",";
                if (fieldInfos[f].FieldType != typeof(string))
                {
                    string data_field = "0";
                    if (f < line_data.Count)
                    {
                        if (line_data[f] != string.Empty)
                        {

                            data_field = line_data[f];
                        }
                    }
                    jsontext += "\"" + fieldInfos[f].Name + "\":" + data_field;
                }
                else
                {
                    string data_field = string.Empty;
                    if (f < line_data.Count)
                    {
                        if (line_data[f] != string.Empty)
                        {
                            data_field = line_data[f];
                        }
                    }
                    jsontext += "\"" + fieldInfos[f].Name + "\":\"" + data_field + "\"";
                }

            }
            jsontext += "}";
            T r = JsonUtility.FromJson<T>(jsontext);
             records.Add(r);
        }
        records.Sort(configCompare);
         // records.BinarySearch();

    }

    private List<List<string>> SplitCSVFile(TextAsset csvText)
    {
        List<List<string>> grid= new List<List<string>>();
        string[] lines= csvText.text.Split('\n');
        for(int i=0; i<lines.Length;i++)
        {
            string s = lines[i];
            if(s.CompareTo(string.Empty) != 0)
            {
                string[] line_data=s.Split('\t');
                List<string> ls_line = new List<string>();
                foreach (string e in line_data)
                {
                    string new_char = Regex.Replace(e, @"\t|\n|\r", "");
                    new_char = Regex.Replace(new_char, @"""", ""+ "");
                    ls_line.Add(e);
                }
                grid.Add(ls_line);
            }
          
        }
        return grid;
    }

    public T GetRecordBykeySearch(params object[] values)
    {
        T obj_key = configCompare.SetValueSearch(values);
        int index = records.BinarySearch(obj_key, configCompare);

        if (index >= 0 && index < records.Count)
            return records[index];
        else
            return null;
    }

}
