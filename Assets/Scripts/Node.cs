using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : ScriptableObject {

	[System.NonSerialized]
	public Graph				graph;

	public List< AnchorGroup >	inputAnchors = new List< AnchorGroup >();
	public List< AnchorGroup >	outputAnchors = new List< AnchorGroup >();

	public void OnBeforeSerialize(Graph g)
	{
		graph = g;
		Debug.Log("OnBeforeDeserilized Node, from graph: " + graph);
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
