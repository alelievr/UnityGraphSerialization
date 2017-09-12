using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Link
{
	public Color			color;

	public string			GUID;

	[System.NonSerialized]
	public Anchor			fromAnchor;
	[System.NonSerialized]
	public Anchor			toAnchor;

	public void OnBeforeDeserialize(Anchor anchor)
	{

		Debug.Log("[Link] OnBeforeDeserialize: " + anchor + ", type: " + anchor.groupRef.type);
		if (anchor.groupRef.type == AnchorType.Input)
			toAnchor = anchor;
		else
			fromAnchor = anchor;
	}
	
	public override string ToString()
	{
		return "[" + GetHashCode().ToString() + "]";
	}
}
