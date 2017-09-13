using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class NodeEditor : EditorWindow {

	[MenuItem("Window/Node Editor")]
	static void	OpenWindow()
	{
		EditorWindow.CreateInstance< NodeEditor >().Show();
	}

	public Graph		graph;

	Vector2				scrollbar;

	void	OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Create new"))
			{
				graph = ScriptableObject.CreateInstance< Graph >();
				AssetDatabase.CreateAsset(graph, AssetDatabase.GenerateUniqueAssetPath("Assets/graph.asset"));
				AssetDatabase.Refresh();
				EditorGUIUtility.PingObject(graph);
			}
			if (GUILayout.Button("Save"))
			{
				EditorUtility.SetDirty(graph);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}
		EditorGUILayout.EndHorizontal();

		graph = EditorGUILayout.ObjectField("graph", graph, typeof(Graph), false) as Graph;

		if (graph != null)
		{
			EditorGUILayout.LabelField(graph.GetHashCode().ToString());
			if (GUILayout.Button("Create new node"))
			{
				Node n = ScriptableObject.CreateInstance< Node >();
				n.graphRef = graph;
				AssetDatabase.AddObjectToAsset(n, graph);
				graph.nodes.Add(n);
			}

			scrollbar = EditorGUILayout.BeginScrollView(scrollbar);
			{
				foreach (var node in graph.nodes)
				{
					if (node == null)
						Debug.Log("OLOL: " + node);
					if (node == null || GUILayout.Button("Remove node " + node))
					{
						graph.nodes.Remove(node);
						return ;
					}
					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField(node.name);
						DrawAnchorGroup("input anchors", node.inputAnchors, new Color(0, .6f, .6f), node);
						DrawAnchorGroup("output anchors", node.outputAnchors, new Color(0, .8f, .8f), node);
						EditorGUILayout.Space();
						EditorGUILayout.Space();
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndScrollView();
		}

		Repaint();
	}

	void DrawAnchorGroup(string title, List< AnchorGroup > anchorGroups, Color c, Node node)
	{
		Rect r = EditorGUILayout.BeginVertical(GUILayout.Width(650));
		{
			EditorGUI.DrawRect(r, c);
			EditorGUILayout.LabelField(title);
			if (GUILayout.Button("Add input anchorGroup"))
			{
				AnchorGroup	ag = new AnchorGroup(AnchorType.Input);
				ag.nodeRef = node;
				anchorGroups.Add(ag);
			}
			if (GUILayout.Button("Add output anchorGroup"))
			{
				AnchorGroup ag = new AnchorGroup(AnchorType.Output);
				ag.nodeRef = node;
				anchorGroups.Add(ag);
			}
			EditorGUILayout.Space();
			foreach (var anchorGroup in anchorGroups)
			{
				EditorGUILayout.BeginHorizontal();
				{
					if (GUILayout.Button("Remove " + anchorGroup.type + " group"))
					{
						anchorGroups.Remove(anchorGroup);
						return ;
					}
					EditorGUILayout.LabelField(anchorGroup.name + anchorGroup + ", node: " + anchorGroup.nodeRef);
					if (GUILayout.Button("Add anchor"))
					{
						Anchor a = new Anchor();
						a.groupRef = anchorGroup;
						a.GUID = System.Guid.NewGuid().ToString();
						anchorGroup.anchors.Add(a);
					}
					if (GUILayout.Button("Clear anchors"))
						anchorGroup.anchors.Clear();
				}
				EditorGUILayout.EndHorizontal();
				foreach (var anchor in anchorGroup.anchors)
				{
					Rect r2 = EditorGUILayout.BeginHorizontal(GUILayout.Width(550));
					{
						EditorGUI.DrawRect(r2, new Color(.5f, .5f, .5f));
						EditorGUILayout.LabelField("anchor " + anchor + ", AnchorGroup: " + anchor.groupRef);
						if (GUILayout.Button("Add link"))
						{
							Link l = new Link();

							l.GUID = System.Guid.NewGuid().ToString();
							l.fromAnchor = anchor;
							l.toAnchor = SelectRandomAnchorExcept(anchor);
							l.color = Random.ColorHSV();
							Debug.Log("fromAnchor GUID: " + l.fromAnchor.GUID);
							graph.anchorLinkTable.AddLink(l.fromAnchor.GUID, l);
							graph.anchorLinkTable.AddLink(l.toAnchor.GUID, l);
							l.fromAnchor.links.Add(l);
							l.toAnchor.links.Add(l);
						}
						if (GUILayout.Button("Remove"))
						{
							anchorGroup.anchors.Remove(anchor);
							return ;
						}
					}
					EditorGUILayout.EndHorizontal();
					
					Rect r3 = EditorGUILayout.BeginVertical();
					{
						EditorGUI.DrawRect(r3, new Color(.9f, .7f, 0));
						foreach (var link in anchor.links)
						{
							Rect r4 = EditorGUILayout.BeginHorizontal();
							{
								EditorGUI.DrawRect(r4, link.color);
								string h1 = (link.fromAnchor == null) ? "null" : link.fromAnchor.ToString();
								string h2 = (link.toAnchor == null) ? "null" : link.toAnchor.ToString();
								EditorGUILayout.BeginVertical();
								{
									EditorGUILayout.LabelField(link.ToString());
									EditorGUILayout.LabelField("from: " + h1 + ", to: " + h2);
									if (GUILayout.Button("Remove node", GUILayout.Width(100)))
									{
										link.fromAnchor.links.Remove(link);
										link.toAnchor.links.Remove(link);
										return ;
									}
								}
								EditorGUILayout.EndVertical();
							}
							EditorGUILayout.EndHorizontal();
						}
					}
					EditorGUILayout.EndVertical();
				}
			}
		}
		EditorGUILayout.EndVertical();
	}

	Anchor SelectRandomAnchorExcept(Anchor except)
	{
		List< Anchor >	anchors = new List< Anchor >();

		foreach (var node in graph.nodes)
			foreach (var anchorGroup in node.inputAnchors.Concat(node.outputAnchors))
				foreach (var anchor in anchorGroup.anchors)
					if (except.groupRef.type != anchor.groupRef.type)
						anchors.Add(anchor);
		if (anchors.Count == 0)
			return null;
		return anchors[Random.Range(0, anchors.Count)];
	}

}
