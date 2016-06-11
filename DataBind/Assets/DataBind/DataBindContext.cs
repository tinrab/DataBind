using UnityEngine;

public class DataBindContext : MonoBehaviour
{
	private DataContext m_DataContext;

	public object this[string key] {
		get { return m_DataContext[key]; }
		set {
			if (m_DataContext == null) {
				m_DataContext = new DataContext();
				m_DataContext.contextChanged += BindChanged;
			}

			m_DataContext[key] = value;
		}
	}

	public bool ContainsKey(string key)
	{
		return m_DataContext.ContainsKey(key);
	}

	public void BindChanged(string key)
	{
		var children = GetComponentsInChildren<IBindable>();

		if (children == null) {
			return;
		}

		for (var i = 0; i < children.Length; i++) {
			if (string.IsNullOrEmpty(children[i].key) || children[i].key == key) {
				children[i].Bind(m_DataContext);
			}
		}
	}

	public void BindAll()
	{
		var children = GetComponentsInChildren<IBindable>();

		if (children == null) {
			return;
		}

		if (m_DataContext == null) {
			m_DataContext = new DataContext();
		}

		for (var i = 0; i < children.Length; i++) {
			children[i].Bind(m_DataContext);
		}
	}
}