using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Anchor
{
	[System.NonSerialized]
	public AnchorGroup		groupRef;

	public string			GUID;

	[System.NonSerialized]
	public List< Link >		links = new List< Link >();

	public void OnBeforeDeserialize(AnchorGroup group)
	{
		groupRef = group;
		Debug.Log("[Anchor] Received OnBeforeDeserialized, group: " + groupRef + ", link count: " + links.Count);

		int index = 0;
		var fGUID = groupRef.nodeRef.graphRef.anchorLinkTable.fromAnchorGUID;
		var tGUID = groupRef.nodeRef.graphRef.anchorLinkTable.toAnchorGUID;
		var lGUID = groupRef.nodeRef.graphRef.anchorLinkTable.linksGUID;
		if (groupRef.type == AnchorType.Input)
			foreach (var guid in fGUID)
			{
				if (guid == GUID)
				{
					var link = groupRef.nodeRef.graphRef.anchorLinkTable.GetLinkFromGUID(lGUID[index]);
					link.fromAnchor = this;
					link.toAnchor = groupRef.nodeRef.graphRef.anchorLinkTable.GetAnchorFromGUID(tGUID[index]);
					links.Add(link);
				}
				index++;
			}
		else
			index = tGUID.FindIndex(guid => guid == GUID);
		
		
	}
	
	public override string ToString()
	{
		return "[" + GetHashCode().ToString() + "]";
	}
}