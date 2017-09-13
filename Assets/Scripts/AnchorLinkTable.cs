using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class AnchorLinkTable : ISerializationCallbackReceiver {
	
	[System.Serializable]
	public class LinkGUIDDict : SerializableDictionary< string, Link > {}
	[SerializeField]
	public LinkGUIDDict				linkGUIDDict = new LinkGUIDDict();

	[System.Serializable]
	public class SerializableStringList
	{
		[SerializeField]
		public List< string >	lst = new List< string >();
	}

	[System.Serializable]
	//								anchor GUID, list of link
	public class LinkDict : SerializableDictionary< string, SerializableStringList > {}
	[SerializeField]
	public LinkDict 				linkDict = new LinkDict();

	public void AddLink(string GUID, Link l)
	{
		Debug.Log("added link to anchor " + GUID);
		if (!linkDict.dictionary.ContainsKey(GUID) || linkDict.dictionary[GUID] == null)
			linkDict.dictionary[GUID] = new SerializableStringList();
		linkDict.dictionary[GUID].lst.Add(l.GUID);;
		linkGUIDDict.dictionary[l.GUID] = l;
	}

	public void RemoveLink(string GUID, Link l)
	{
		linkDict.dictionary[GUID].lst.Remove(l.GUID);
		linkGUIDDict.dictionary.Remove(l.GUID);
	}

	public List< string > GetLinksFromAnchor(string GUID)
	{
		SerializableStringList ret = null;
		linkDict.dictionary.TryGetValue(GUID, out ret);
		if (ret != null)
			return ret.lst;
		else
			return null;
	}

	public Link		GetLinkFromGUID(string linkGUID)
	{
		Link ret = null;
		linkGUIDDict.dictionary.TryGetValue(linkGUID, out ret);
		return ret;
	}

	void ISerializationCallbackReceiver.OnAfterDeserialize()
	{
		Debug.Log("after deserialize link dict: keys: " + linkDict.dictionary.Count);
		foreach (var kp in linkDict.dictionary)
			Debug.Log(kp.Key + " -> " + ((kp.Value != null) ? kp.Value.lst.Count : -1));
	}

	void ISerializationCallbackReceiver.OnBeforeSerialize()
	{
		Debug.Log("before serialize link dict: keys: " + linkDict.dictionary.Count);
		foreach (var kp in linkDict.dictionary)
			Debug.Log(kp.Key + " -> " + ((kp.Value != null) ? kp.Value.lst.Count : -1));
	}
}
