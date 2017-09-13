using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph : ScriptableObject, ISerializationCallbackReceiver  {

	public List< Node >		nodes = new List< Node >();

	public AnchorLinkTable	anchorLinkTable = new AnchorLinkTable();

	void ISerializationCallbackReceiver.OnBeforeSerialize() { Debug.Log("Before serialize Graph"); }

	void ISerializationCallbackReceiver.OnAfterDeserialize()
	{
		Debug.Log("Graph AfterDeserilize event from: " + GetHashCode() + ", node count: " + nodes.Count);
		//nope you can't do that
		// foreach (var node in nodes)
			// node.OnAfterDeserialize(this);
		// anchorLinkTable.OnAfterDeserialize(this);
	}

	void OnEnable()
	{
		foreach (var node in nodes)
			node.OnAfterDeserialize(this);
		Debug.Log("Graph OnEnable");
	}
	
	public override string ToString()
	{
		return "[" + GetHashCode().ToString() + "]";
	}
		
}
