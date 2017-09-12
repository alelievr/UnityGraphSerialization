# UnityGraphSerialization
Unity graph serialization tests

Structure used:
- A `Graph` is composed of multiple (a list of) `Nodes`
- A `Node` contains a list of `AnchorGroup`
- An `AnchorGroup` contains a list of `Anchor`
- An `Anchor` contains a list of `Links`
- Each `Link` have two references of `Anchors`: `from` and `to`
- Each `Anchor` have the reference of the parent `AnchorGroup`
- Each `AnchorGroup` have the reference of the parent `Node`
- Each `Node` have the reference of the parent `Graph`
