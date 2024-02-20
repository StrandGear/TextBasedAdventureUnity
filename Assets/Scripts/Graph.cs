/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs
{
    [System.Serializable]
    public class GraphNode
    {
        List<GraphEdge> _edges;
        //public string nodeName;
        public Room nodeRoom;

        public GraphNode(Room _nodeRoom)
        {
            nodeRoom = _nodeRoom;
            _edges = new List<GraphEdge>();
        }

        public void AddEdge(GraphEdge edge)
        {
            _edges.Add(edge);
        }

        public float GetAdjacenceWeight(GraphNode otherNode)
        {
            foreach (GraphEdge edge in _edges)
            {
                if (otherNode == this)
                {
                    if (edge._nodeA == edge._nodeB)
                        return edge._weight;
                }
                else if (edge.isIncidentTo(otherNode))
                {
                    return edge._weight;
                }
            }
            return 0f;
        }

        public override string ToString()
        {
            return nodeRoom.roomName;
        }
    }

    [System.Serializable]
    public class GraphEdge
    {
        public GraphNode _nodeA;
        public GraphNode NodeA
        {
            get
            {
                return _nodeA;
            }
        }

        public GraphNode _nodeB;
        public GraphNode NodeB
        {
            get
            {
                return _nodeB;
            }
        }
        public float _weight;
        public float Weight
        {
            get
            {
                return _weight;
            }
        }

        public GraphEdge(GraphNode nodeA, GraphNode nodeB, float weight)
        {
            this._nodeA = nodeA;
            this._nodeB = nodeB;
            this._weight = weight;
            nodeA.AddEdge(this);
            if (nodeA != nodeB)
            {
                nodeB.AddEdge(this);
            }
        }

        public bool isIncidentTo(GraphNode node)
        {
            return (_nodeA == node || _nodeB == node);
        }

        public override string ToString()
        {
            return _nodeA.ToString() + ", " + _nodeB.ToString();
        }

        public string ToString2()
        {
            return _nodeA.ToString() + ", " + _nodeB.ToString() + ", " + _weight;
        }
    }


    [System.Serializable]
    public class Graph
    {
        List<GraphNode> _nodes;
        List<GraphEdge> _edges;

        public Graph()
        {
            _nodes = new List<GraphNode>();
            _edges = new List<GraphEdge>();
        }

        public void AddNode(GraphNode node)
        {
            _nodes.Add(node);
        }

        public GraphEdge AddEdge(GraphNode nodeA, GraphNode nodeB, float weight)
        {
            GraphEdge edge = new GraphEdge(nodeA, nodeB, weight);
            _edges.Add(edge);
            return edge;
        }

        public string ToMatrix()
        {
            string matrix = "\t";
            foreach (GraphNode nodeA in _nodes)
            {
                matrix += nodeA.ToString() + "\t";
            }
            matrix += "\n";
            foreach (GraphNode nodeA in _nodes)
            {
                matrix += nodeA.ToString() + "\t";
                foreach (GraphNode nodeB in _nodes)
                {
                    float weight = nodeA.GetAdjacenceWeight(nodeB);
                    matrix += weight + "\t";
                }
                matrix += "\n";
            }
            return matrix;
        }
    }

}
*/