using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphs2
{
    [System.Serializable]
    public class GraphNode2
    {
        List<GraphEdge2> _edges;
        public string name;

        public GraphNode2(string _name)
        {
            name = _name;
            _edges = new List<GraphEdge2>();
        }

        int getDeg()
        {
            return _edges.Count;
        }

        public void AddEdge(GraphEdge2 edge)
        {
            _edges.Add(edge);
        }

        public bool IsAdjacent(GraphNode2 otherNode)
        {
            foreach (GraphEdge2 edge in _edges)
            {
                if (edge.isIncidentTo(otherNode))
                {
                    return true;
                }
            }
            return false;
        }

        public float GetAdjacenceWeight(GraphNode2 otherNode)
        {
            foreach (GraphEdge2 edge in _edges)
            {
                if (otherNode == this) // checking loops
                {
                    if (edge.NodeA == edge.NodeB)
                    {
                        return edge.Weight;
                    }
                } // checking incidence for different nodes
                else if (edge.isIncidentTo(otherNode))
                {
                    return edge.Weight;
                }
            }
            return 0f;
        }

        public GraphNode2[] getNeighbours()
        {
            GraphNode2[] neighbours = new GraphNode2[_edges.Count];
            int i = 0;
            foreach (GraphEdge2 edge in _edges)
            {
                if (edge.NodeA == this)
                {
                    neighbours[i] = edge.NodeB;
                }
                else
                {
                    neighbours[i] = edge.NodeA;
                }
                i++;
            }
            return neighbours;
        }

        void addEdge(GraphEdge2 edge)
        {
            _edges.Add(edge);
        }

        public override string ToString()
        {
            return name;
        }

    }

    public class GraphEdge2
    {
        private GraphNode2 _nodeA;

        public GraphNode2 NodeA
        {
            get
            {
                return _nodeA;
            }
        }

        private GraphNode2 _nodeB;

        public GraphNode2 NodeB
        {
            get
            {
                return _nodeB;
            }
        }

        private float _weight;

        public float Weight
        {
            get
            {
                return _weight;
            }
        }

        public GraphEdge2(GraphNode2 nodeA, GraphNode2 nodeB, float weight)
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


        public bool isIncidentTo(GraphNode2 node)
        {
            return (_nodeA == node || _nodeB == node);
        }


        public override string ToString()
        {
            return NodeA.ToString() + ", " + NodeB.ToString();
        }

        public string ToString2()
        {
            return NodeA.ToString() + ", " + NodeB.ToString() + ", " + Weight;
        }


    }

    public class Graph2 : MonoBehaviour
    {
        public List<GraphNode2> _nodes;
        List<GraphEdge2> _edges;
        public Graph2()
        {
            _nodes = new List<GraphNode2>();
            _edges = new List<GraphEdge2>();
        }

        public void addNode(GraphNode2 node)
        {
            _nodes.Add(node);
        }

        public GraphEdge2 addEdge(GraphNode2 nodeA, GraphNode2 nodeB, float weight)
        {
            GraphEdge2 edge = new GraphEdge2(nodeA, nodeB, weight);
            _edges.Add(edge);
            return edge;
        }

        public override string ToString()
        {
            string v = "V = {";
            foreach (GraphNode2 node in _nodes)
            {
                v += "(" + node.ToString() + "),";
            }
            v += "}";

            string e = "E = {";
            string w = "W = {";

            foreach (GraphEdge2 edge in _edges)
            {
                e += "(" + edge.ToString() + "),";
                w += "(" + edge.ToString2() + "),";
            }

            e += "}";
            w += "}";



            return v + "\n" + e + "\n" + w;
        }

        public string ToMatrix()
        {
            string matrix = "\t";
            foreach (GraphNode2 nodeA in _nodes)
            {
                matrix += nodeA.ToString() + "\t";
            }
            matrix += "\n";
            foreach (GraphNode2 nodeA in _nodes)
            {
                matrix += nodeA.ToString() + "\t";
                foreach (GraphNode2 nodeB in _nodes)
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

