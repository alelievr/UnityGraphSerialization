using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnchorType
{
	Input,
	Output,
}

[System.Serializable]
public class AnchorGroup
{
	[System.NonSerialized]
	public Node					nodeRef;
	public AnchorType			type;

	public string				name;	
	public List< Anchor >		anchors = new List< Anchor >();

	public void OnBeforeDeserialize(Node node)
	{
		nodeRef = node;
		Debug.Log("[AnchorGroup] received OnDeserilized, node: " + nodeRef + ", anchors count: " + anchors.Count);
		foreach (var a in anchors)
			a.OnBeforeDeserialize(this);
	}

	public AnchorGroup(AnchorType type)
	{
		this.type = type;
	}
	
	public override string ToString()
	{
		return "[" + GetHashCode().ToString() + "]";
	}

}