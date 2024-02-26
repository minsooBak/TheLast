using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath ="Resources/Data/")]
public class ItemData : ScriptableObject
{
	public List<ItemEntity> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
