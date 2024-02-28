using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Data", ExcelName = "PlayerBaseLevel")]
public class PlayerBaseLevel : ScriptableObject
{
	public List<PlayerLevelInfo> MagicianLevel;
	public List<PlayerLevelInfo> OrcWarriorsLevel;
}
