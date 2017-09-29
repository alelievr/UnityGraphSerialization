using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : ScriptableObject {

	[System.NonSerialized]
	public Graph				graphRef;

	public List< AnchorGroup >	inputAnchors = new List< AnchorGroup >();
	public List< AnchorGroup >	outputAnchors = new List< AnchorGroup >();

	public void OnAfterDeserialize(Graph g)
	{
		graphRef = g;

		Debug.Log("Node OnAfterDeserialize");

		foreach (var a in inputAnchors)
			a.OnBeforeDeserialize(this);
		foreach (var a in outputAnchors)
			a.OnBeforeDeserialize(this);
	}
	
	public override string ToString()
	{
		return "Node [" + GetHashCode().ToString() + "]";
	}
}
