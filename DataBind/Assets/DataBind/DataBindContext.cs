using System.Collections.Generic;
using UnityEngine;

public class DataBindContext : MonoBehaviour
{
	private IDictionary<string, object> m_ActiveBinds = new Dictionary<string, object>();

	public bool ContainsKey(string key)
	{
		return m_ActiveBinds.ContainsKey(key);
	}

	public object this[string key] {
		get {
			return m_ActiveBinds[key];
		}
		set {
			if (value == null) {
				return;
			}

			m_ActiveBinds[key] = value;
			var children = GetComponentsInChildren<IBindable>();

			for (int i = 0; i < children.Length; i++) {
				children[i].Bind(this);
			}
		}
	}
}
