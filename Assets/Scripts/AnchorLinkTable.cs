using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AnchorLinkTable {

	public List< string >			fromAnchorGUID = new List< string >();
	public List< string >			toAnchorGUID = new List< string >();
	public List< string >			linksGUID = new List< string >();

	public List< Link >				links = new List< Link >();

	public List< Anchor >			anchors = new List< Anchor >();

	public void AddLink(string fromGUID, string toGUID, Link l)
	{
		fromAnchorGUID.Add(fromGUID);
		toAnchorGUID.Add(toGUID);
		linksGUID.Add(l.GUID);
		links.Add(l);
	}

	public void RemoveLink(string fromGUID, string toGUID, string lGUID)
	{
		fromAnchorGUID.Remove(fromGUID);
		toAnchorGUID.Remove(toGUID);
		int index = linksGUID.FindIndex(l => l == lGUID);
		links.RemoveAt(index);
		linksGUID.RemoveAt(index);
	}

	public Link GetLinkFromGUID(string guid)
	{
		return links.FirstOrDefault(l => l.GUID == guid);
	}

	public Anchor GetAnchorFromGUID(string guid)
	{
		return anchors.FirstOrDefault(a => a.GUID == guid);
	}
}
