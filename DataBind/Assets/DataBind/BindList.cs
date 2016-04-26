using System;
using System.Collections.Generic;
using UnityEngine;

public class BindList : MonoBehaviour, IBindable
{
	[SerializeField]
	public string m_Key;
	[SerializeField]
	public string m_ItemKey;
	[SerializeField]
	private GameObject m_ItemPrefab;
	private DataContext m_Context;

	public void Bind(DataContext context)
	{
		m_Context = new DataContext();

		if (context.ContainsKey(m_Key)) {
			ClearChildren();

			var list = (ObservableList)context[m_Key];

			for (int i = 0; i < list.Count; i++) {
				var itemData = list[i];
				var item = Instantiate(m_ItemPrefab);
				item.transform.SetParent(transform);

				var bindables = item.GetComponentsInChildren<IBindable>();
				var properties = itemData.GetType().GetProperties();

				for (int j = 0; j < properties.Length; j++) {
					var p = properties[j];

					m_Context["item." + p.Name] = p.GetValue(itemData, null);
				}

				for (int j = 0; j < bindables.Length; j++) {
					bindables[j].Bind(m_Context);
				}
			}
		}
	}

	private void ClearChildren()
	{
		for (int i = 0; i < transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
