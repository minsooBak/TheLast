using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DB", ExcelName ="EnemyDataSheet")]
public class EnemySO : ScriptableObject
{
	public List<EnemyData> EnemyData; 
	public List<DungeonData> DungeonData; 

}
