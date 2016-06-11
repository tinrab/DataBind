using System;

public interface INotifyCollectionChanged
{
	event Action<string> collectionChanged;
}
