using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QTableData", menuName = "AI/QTable")]
public class QTableSO : ScriptableObject
{
    public List<QActionEntry> struggling;
    public List<QActionEntry> balanced;
    public List<QActionEntry> dominating;
}