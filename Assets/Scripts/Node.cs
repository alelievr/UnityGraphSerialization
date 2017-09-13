using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : ScriptableObject, ISerializationCallbackReceiver {

	[System.NonSerialized]
	public Graph				graphRef;

	public List< AnchorGroup >	inputAnchors = new List< AnchorGroup >();
	public List< AnchorGroup >	outputAnchors = new List< AnchorGroup >();

	void ISerializationCallbackReceiver.OnBeforeSerialize()
	{
		Debug.Log("beforeSerialize node");
	}

	void ISerializationCallbackReceiver.OnAfterDeserialize()
	{
		Debug.Log("afterDeserialize node");
	}

	void OnEnable()
	{
		Debug.Log("Node OnEnable");
	}

	public void OnAfterDeserialize(Graph g)
	{
		graphRef = g;
		Debug.Log("OnBeforeDeserilizedCustom Node, from graph: " + graphRef + ", input anchors: " + inputAnchors.Count + ", output anchors: " + outputAnchors.Count);
		foreach (var a in inputAnchors)
			a.OnBeforeDeserialize(this);
		foreach (var a in outputAnchors)
			a.OnBeforeDeserialize(this);
	}
	
	public override string ToString()
	{
		return "[" + GetHashCode().ToString() + "]";
	}
}
