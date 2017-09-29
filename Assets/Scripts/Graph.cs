using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph : ScriptableObject {

	public List< Node >		nodes = new List< Node >();

	public AnchorLinkTable	anchorLinkTable = new AnchorLinkTable();

	void OnEnable()
	{
		foreach (var node in nodes)
			node.OnAfterDeserialize(this);
		Debug.Log("Graph OnEnable");
	}
	
	public override string ToString()
	{
		return "Graph [" + GetHashCode().ToString() + "]";
	}
		
}
