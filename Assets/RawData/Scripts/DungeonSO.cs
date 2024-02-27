using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Data", ExcelName = "DungeonData")]
public class DungeonSO : ScriptableObject
{
    public List<DungeonData> DungeonDatas;
}
