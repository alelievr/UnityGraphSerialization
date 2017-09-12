using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anchor
{
	[System.NonSerialized]
	public AnchorGroup		groupRef;

	public List< Link >		links = new List< Link >();

	public void OnBeforeDeserialize(AnchorGroup group)
	{
		groupRef = group;
		Debug.Log("[Anchor] Received OnBeforeDeserialized, group: " + groupRef + ", link count: " + links.Count);
		foreach (var l in links)
			l.OnBeforeDeserialize(this);
	}
	
	public override string ToString()
	{
		return "[" + GetHashCode().ToString() + "]";
	}
}