using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Data",ExcelName ="PlayerBaseStatus")]
public class PlayerBaseStatus : ScriptableObject
{
	public List<PlayerStatusInfo> PlayerStatuses;
}
