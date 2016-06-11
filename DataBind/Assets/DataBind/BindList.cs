using UnityEngine;

public class BindList : MonoBehaviour, IBindable
{
	private DataContext m_Context;
	[SerializeField]
	public string m_ItemKey;
	[SerializeField]
	private GameObject m_ItemPrefab;
	[SerializeField]
	public string m_Key;

	public string key {
		get { return m_Key; }
	}

	public void Bind(DataContext context)
	{
		m_Context = new DataContext();

		if (context.ContainsKey(m_Key)) {
			ClearChildren();

			var list = (ObservableList) context[m_Key];

			for (var i = 0; i < list.Count; i++) {
				var itemData = list[i];
				var item = Instantiate(m_ItemPrefab);
				item.transform.SetParent(transform, false);
				item.transform.localScale = Vector3.one;

				var bindables = item.GetComponentsInChildren<IBindable>(true);
				var properties = itemData.GetType().GetProperties();

				var model = item.GetComponent<IModel>();

				if (model != null) {
					model.model = itemData;
				}

			    for (var j = 0; j < properties.Length; j++) {
					var p = properties[j];

					m_Context[m_ItemKey + "." + p.Name] = p.GetValue(itemData, null);
				}

				for (var j = 0; j < bindables.Length; j++) {
					bindables[j].Bind(m_Context);
				}
			}
		}
	}

	private void ClearChildren()
	{
		for (var i = 0; i < transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}