using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tree
{
    [System.Serializable]
    public class TreeNode
    {
        public TreeNode Parent
        {
            get; protected set;
        }

        public TreeNode LeftChild
        {
            get; protected set;
        }

        public TreeNode RightChild
        {
            get; protected set;
        }

        public int Value
        {
            get; protected set;
        }

        public TreeNode (int value, TreeNode parent)
        {
            Value = value;
            Parent = parent;
        }

        public void AddChild(int newValue)
        {
            if (newValue < Value)
            {
                if (LeftChild == null)
                {
                    LeftChild = new TreeNode(newValue, this);
                }
                else
                {
                    LeftChild.AddChild(newValue);
                }
            }
            else if (newValue > Value)
            {
                if (RightChild == null)
                {
                    RightChild = new TreeNode(newValue, this);
                }
                else
                {
                    RightChild.AddChild(newValue);
                }
            }
            else
            {
                Debug.Log("Doublette ignored: " + newValue);
            }
        }

        public override string ToString()
        {
            return Level() + ":" + Value.ToString() + " ";
        }

        public string Inorder()
        {
            string order = "";
            if (LeftChild != null)
            {
                order += LeftChild.Inorder();
            }
            order += this.ToString();
            if (RightChild != null)
            {
                order += RightChild.Inorder();
            }
            return order;
        }

        public string Preorder()
        {
            string order = "";
            order += this.ToString();
            if (LeftChild != null)
            {
                order += LeftChild.Preorder();
            }
            if (RightChild != null)
            {
                order += RightChild.Preorder();
            }
            return order;
        }

        public int Level()
        {
            if (Parent == null)
            {
                return 0;
            }
            else
            {
                return 1 + Parent.Level();
            }
        }

        public int Height()
        {
            if (LeftChild == null && RightChild == null)
            {
                return 0;
            }
            int lh = 0;
            int rh = 0;
            if (LeftChild != null)
            {
                lh = LeftChild.Height();
            }
            if (RightChild != null)
            {
                rh = RightChild.Height();
            }
            return 1 + Math.Max(lh, rh);
        }


        public void PrintNode()
        {
            string line = "";
            for (int i = 0; i < this.Level(); i++)
            {
                line += "|  ";
            }
            line += "+--";
            line += this.ToString();
            Debug.Log(line);
            if (LeftChild != null)
            {
                LeftChild.PrintNode();
            }
            if (RightChild != null)
            {
                RightChild.PrintNode();
            }
        }
    }
    
    [System.Serializable]
    public class DecisionTreeNode : TreeNode
    {
        [HideInInspector]
        public int nodeID;
        public string Text;
        //public TreeNode LeftChild, RightChild;
        //public DecisionTreeNode LeftLeaf, RightLeaf;
        public DecisionTreeNode(string decisionText, int value = 0, TreeNode parent = null) : base(value, parent)
        {
            Text = decisionText;
        }
        public void AddLeftNode(DecisionTreeNode leftNode)
        {
            LeftChild = leftNode;
            leftNode.Parent = this;
        }

        public void AddRightNode (DecisionTreeNode rightNode)
        {
            RightChild = rightNode;
            rightNode.Parent = this;
        }

        public void NodeAction(GameController controller, string answer = "")
        {
            if (LeftChild == null && RightChild == null)
            {
                Debug.Log("You ended up with " + Text);
                controller.LogStringWithReturn("You ended up with " + Text);
            }
            else if (LeftChild == null || RightChild == null)
            {
                Debug.Log("The only option is left is to " + Text);
                controller.LogStringWithReturn("The only option is left is to " + Text);
            }
            else
            {
                controller.LogStringWithReturn("Question: " + Text + "\n" + "Answer (y)es or (n)o.");
                Debug.Log("Question: " + Text);
                Debug.Log("Answer (y)es or (n)o.");
                if (answer == "y" || answer == "yes")
                    ((DecisionTreeNode)LeftChild).NodeAction(controller);
                else if (answer == "n" || answer == "no")
                    ((DecisionTreeNode)RightChild).NodeAction(controller);
            }
        }

/*        public DecisionTreeNode ReturnCurrNode(DecisionTreeNode currNode)
        {
            if (LeftChild == null && RightChild == null)
                return null;
            else if (LeftChild == null || RightChild == null)
                return currNode;
            else
            {
                return 
            }
        }*/

        public override string ToString()
        {
            return Level() + ":" + Text + " ";
        }
    }

    /*    [System.Serializable]
        public class DecisionTree : MonoBehaviour
        {
            //[Header("Odds are 'yes', evens are 'no'")]
            //public Dictionary<int, string> decisions;
            //public string 
            private List<DecisionTreeNode> decisionList;

            //public

            //public List<DecisionTreeNode> decisions;
            *//*        DecisionTreeNode bees1 = new DecisionTreeNode(0, null, "Appears fuzzy, hairy on the thorax (y)? Or not (n)");
                    DecisionTreeNode bees2 = new DecisionTreeNode(0, null, "Hair on hind legs (y)? Or hair on abdomen (n)?");
                    private void Start()
                    {
                        bees1.AddLeftNode(bees2);
                        bees1.NodeAction();
                    }
            *//*
            private void Start()
            {
                for (int i = 0; i < decisions.Count; i++)
                {
                    DecisionTreeNode newNode = new DecisionTreeNode(decisions[i]);
                    decisionList.Add(newNode);
                }
            }
        }*/

    
/*    public struct DecisionTreeSerializable
    {
        public string eventDescription;
        public int nodeID;
        public int parentID;
        public bool isLefChild;
    }
*/[System.Serializable]
    public struct DecisionTreeDictValue
    {
        public DecisionTreeNode treeNode;
        public int parentID;
        public bool isLeftChild;
    }

/*    [System.Serializable]
    public struct DecisionTreeDictValue2
    {
        public int parentID;
        public bool isLeftChild;
    }*/

    [System.Serializable]
    public class DecisionTreeDictionary : Dictionary<int, DecisionTreeDictValue>
    {
        public void Add(int _key, DecisionTreeNode _treeNode, int _parentID, bool _isLeftChild)
        {
            DecisionTreeDictValue _val;
            _val.treeNode = _treeNode;
            _val.parentID = _parentID;
            _val.isLeftChild = _isLeftChild;
            this.Add(_key, _val);
        }
    }
/*
    [System.Serializable]
    public class DecisionTreeDictionaryRevert : Dictionary<DecisionTreeNode, DecisionTreeDictValue2>
    {
        public void Add(DecisionTreeNode _key, int _parentID, bool _isLeftChilde)
        {
            DecisionTreeDictValue2 _val;
            _val.parentID = _parentID;
            _val.isLeftChild = _isLeftChilde;
            this.Add(_key, _val);
        }
    }*/

    [System.Serializable]
    public class DecisionTree : MonoBehaviour
    {
/*        public string eventDescription;
        public int nodeID;
        public int parentID;
        public bool isLefChild;*/
        
        //public List<DecisionTreeSerializable> decisions;
        //private List<DecisionTreeNode> decisionList;
        //DecisionTreeDictionary<int, DecisionTreeDictValue> decisionDict;
/*        public DecisionTreeDictionary dict;

        private void Start()
        {
*//*            for (int i = 0; i < decisions.Count; i++)
            {
                DecisionTreeNode newNode = new DecisionTreeNode(decisions[i].eventDescription);
                int newNodeID = decisions[i].nodeID;
                //decisionDict.Add(newNodeID, newNode);
            }*//*

            //for (int i = 1; i < decisionDict.Count; i++)
            {

            }
            for (int i = 0; i < dict.Count; i++)
            {
                //dict[i].treeNode.DecisionText = 
            }
            for (int i = 1; i < dict.Count; i++)
            {
                if (dict[i].isLeftChild)
                    dict[dict[i].parentID].treeNode.AddLeftNode(dict[i].treeNode);
                else
                    dict[dict[i].parentID].treeNode.AddRightNode(dict[i].treeNode);
            }
        }*/
    }
}