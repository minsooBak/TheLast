using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Data", ExcelName = "PlayerBaseSkill")]
public class PlayerBaseSkill : ScriptableObject
{
	public List<PlayerSkillInfo> MagicianSkill;
	public List<PlayerSkillInfo> OrcWarriorsSkill;
}
