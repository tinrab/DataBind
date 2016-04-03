using UnityEngine;

public class DataBindContext : MonoBehaviour
{
	private DataContext m_DataContext;

	private void Awake()
	{
		m_DataContext = new DataContext();
		m_DataContext.contextChanged += BindAll;
	}

	public bool ContainsKey(string key)
	{
		return m_DataContext.ContainsKey(key);
	}

	public object this[string key] {
		get {
			return m_DataContext[key];
		}
		set {
			m_DataContext[key] = value;
		}
	}

	private void BindAll()
	{
		var children = GetComponentsInChildren<IBindable>();

		if (children == null) {
			return;
		}

		for (int i = 0; i < children.Length; i++) {
			children[i].Bind(m_DataContext);
		}
	}
}
