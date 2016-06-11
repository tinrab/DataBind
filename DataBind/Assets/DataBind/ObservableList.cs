using System;
using System.Collections;

[Serializable]
public class ObservableList : ArrayList, INotifyCollectionChanged
{
	public event Action<string> collectionChanged = delegate { };
	private string m_Key;

	public ObservableList(string key)
	{
		m_Key = key;
	}

	public new void Add(object item)
	{
		base.Add(item);
		collectionChanged.Invoke(m_Key);
	}

	public new void Remove(object item)
	{
		base.Remove(item);
		collectionChanged.Invoke(m_Key);
	}

	public new void AddRange(ICollection collection)
	{
		base.AddRange(collection);
		collectionChanged.Invoke(m_Key);
	}

	public new void RemoveRange(int index, int count)
	{
		base.RemoveRange(index, count);
		collectionChanged.Invoke(m_Key);
	}

	public new void Clear()
	{
		base.Clear();
		collectionChanged.Invoke(m_Key);
	}

	public new void Insert(int index, object item)
	{
		base.Insert(index, item);
		collectionChanged.Invoke(m_Key);
	}

	public new object this[int index] {
		get {
			return base[index];
		}
		set {
			base[index] = value;
			collectionChanged.Invoke(m_Key);
		}
	}
}
