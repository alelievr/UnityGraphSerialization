using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph : ScriptableObject, ISerializationCallbackReceiver  {

	public List< Node >		nodes = new List< Node >();

	void ISerializationCallbackReceiver.OnAfterDeserialize() {}

	void ISerializationCallbackReceiver.OnBeforeSerialize()
	{

		Debug.Log("Deserilize event from: " + GetHashCode() + ", node count: " + nodes.Count);
		foreach (var node in nodes)
			node.OnBeforeSerialize(this);
	}
	
	public override string ToString()
	{
		return "[" + GetHashCode().ToString() + "]";
	}
		
}
