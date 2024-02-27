using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Data", ExcelName ="EnemyData")]
public class EnemySO : ScriptableObject
{
	public List<EnemyData> EnemyDatas; 

}
