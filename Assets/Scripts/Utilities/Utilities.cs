using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    public static string Tokey(this int id)
    {
        string key = "K_" + id.ToString();
        return key;
    }
    public static List<string> MakeListPath(this string path)
    {
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        return paths;

    }
    public static int CalculatorStat(int min, int max, int lv_Max, int lv, float factor)
    {
        //Value = Min + (Max - Min)* ((Lv - 1) / (Lv_Max - 1)) ^ Factor
        float level = (float)(lv - 1) / (float)(lv_Max - 1);
        float pow = Mathf.Pow(level, factor);
        float rel = min + (max - min) * pow;
        int total = (int)(rel * 100f);
        int a = total / 100;
        int b = total % 100;
        if (b >= 50)
            a += 1;
        return a;
        //return (int)(Mathf.RoundToInt(rel * 100f) * 0.01f);
    }
    public static float CalculatorStat(float min, float max, int lv_Max, int lv, float factor)
    {
        //Value = Min + (Max - Min)* ((Lv - 1) / (Lv_Max - 1)) ^ Factor
        float level = (float)(lv - 1) / (float)(lv_Max - 1);
        float pow = Mathf.Pow(level, factor);
        float rel = min + (max - min) * pow;
        float total = (float)(rel * 100f);
        float a = total / 100f;
      
        return a;
        //return (int)(Mathf.RoundToInt(rel * 100f) * 0.01f);
    }
}
