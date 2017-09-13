using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AnchorLinkTable {
	
	[System.Serializable]
	public class LinkGUIDDict : SerializableDictionary< string, Link > {}
	[SerializeField]
	public LinkGUIDDict				linkGUIDDict = new LinkGUIDDict();

	[System.Serializable]
	//								anchor GUID, list of link
	public class LinkDict : SerializableDictionary< string, List< string > > {}
	[SerializeField]
	public LinkDict 				linkDict = new LinkDict();

	public void AddLink(string GUID, Link l)
	{
		if (linkDict[GUID] == null)
			linkDict[GUID] = new List< string >();
		linkDict[GUID].Add(l.GUID);;
		linkGUIDDict[l.GUID] = l;
	}

	public void RemoveLink(string GUID, Link l)
	{
		linkDict[GUID].Remove(l.GUID);
		linkGUIDDict.Remove(l.GUID);
	}

	public List< string > GetLinksFromAnchor(string GUID)
	{
		List< string > ret = null;
		linkDict.TryGetValue(GUID, out ret);
		return ret;
	}

	public Link		GetLinkFromGUID(string linkGUID)
	{
		Link ret = null;
		linkGUIDDict.TryGetValue(linkGUID, out ret);
		return ret;
	}
}
