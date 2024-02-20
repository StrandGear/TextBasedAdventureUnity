/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree;

[System.Serializable]
public class SerializableDictionary<TKey, DecisionTreeDictValue> : Dictionary<TKey, DecisionTreeDictValue>, ISerializationCallbackReceiver
{
	public struct DecisionTreeDictValue
	{
		public DecisionTreeNode treeNode;
		public int parentID;
		public bool isLeftChild;
	}

	[SerializeField]
	private List<TKey> keys = new List<TKey>();

	[SerializeField]
	private List<DecisionTreeDictValue> values = new List<DecisionTreeDictValue>();

	// save the dictionary to lists
	public void OnBeforeSerialize()
	{
		keys.Clear();
		values.Clear();
		foreach (KeyValuePair<TKey, DecisionTreeDictValue> pair in this)
		{
			keys.Add(pair.Key);
			values.Add(pair.Value);
		}
	}

	// load dictionary from lists
	public void OnAfterDeserialize()
	{
		this.Clear();

		if (keys.Count != values.Count)
			throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

		for (int i = 0; i < keys.Count; i++)
			this.Add(keys[i], values[i]);
	}
	public void Add(int _key, DecisionTreeNode _treeNode, int _parentID, bool _isLeftChild)
	{
		DecisionTreeDictValue _val;
		_val.treeNode = _treeNode;
		_val.parentID = _parentID;
		_val.isLeftChild = _isLeftChild;
		this.Add(_key, _val);
	}

	*//*	public struct DecisionTreeDictValue
		{
			public DecisionTreeNode nodeValue;
			public 
		}*//*
}
*/