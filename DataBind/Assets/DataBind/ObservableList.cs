using System;
using System.Collections;

[Serializable]
public class ObservableList : ArrayList, INotifyCollectionChanged
{
	public event Action collectionChanged = delegate { };

	public new void Add(object item)
	{
		base.Add(item);
		collectionChanged.Invoke();
	}

	public new void Remove(object item)
	{
		base.Remove(item);
		collectionChanged.Invoke();
	}

	public new void AddRange(ICollection collection)
	{
		base.AddRange(collection);
		collectionChanged.Invoke();
	}

	public new void RemoveRange(int index, int count)
	{
		base.RemoveRange(index, count);
		collectionChanged.Invoke();
	}

	public new void Clear()
	{
		base.Clear();
		collectionChanged.Invoke();
	}

	public new void Insert(int index, object item)
	{
		base.Insert(index, item);
		collectionChanged.Invoke();
	}

	public new object this[int index] {
		get {
			return base[index];
		}
		set {
			base[index] = value;
			collectionChanged.Invoke();
		}
	}
}
