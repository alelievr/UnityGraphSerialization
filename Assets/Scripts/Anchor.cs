using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Anchor
{
	[System.NonSerialized]
	public AnchorGroup		groupRef;

	[SerializeField]
	public string			GUID;

	[System.NonSerialized]
	public List< Link >		links = new List< Link >();

	public void OnAfterDeserialize(AnchorGroup group)
	{
		groupRef = group;
		Debug.Log("[Anchor] Received OnBeforeDeserialized, group: " + groupRef + ", link count: " + links.Count);

		var linkGUIDs = groupRef.nodeRef.graphRef.anchorLinkTable.GetLinksFromAnchor(GUID);

 		//if there is no links, quit
		if (linkGUIDs == null)
			return ;
		
		foreach (var linkGUID in linkGUIDs)
		{
			var linkInstance = groupRef.nodeRef.graphRef.anchorLinkTable.GetLinkFromGUID(linkGUID);
			links.Add(linkInstance);
			if (groupRef.type == AnchorType.Input)
				linkInstance.fromAnchor = this;
			else
				linkInstance.toAnchor = this;
		}
	}
	
	public override string ToString()
	{
		return "[" + GUID + "]";
	}
}