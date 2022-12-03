public class Edge {
    // Start and end nodes
    public NodeGr startNode;
    public NodeGr endNode;

    // Create a link
    public Edge(NodeGr from, NodeGr to) {
        startNode = from;
        endNode = to;
    }
}
