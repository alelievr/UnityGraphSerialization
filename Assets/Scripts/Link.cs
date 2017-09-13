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

	public override string ToString()
	{
		return "[" + GetHashCode().ToString() + "]";
	}
}
