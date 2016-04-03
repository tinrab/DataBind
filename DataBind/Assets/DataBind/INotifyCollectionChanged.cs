using System;

public interface INotifyCollectionChanged
{
	event Action collectionChanged;
}
