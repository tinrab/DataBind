using UnityEngine;
using UnityEngine.UI;

public class BindColor : MonoBehaviour, IBindable
{
	[SerializeField]
	private Graphic m_Graphic;
	[SerializeField]
	private string m_Key;

	public string key {
		get { return m_Key; }
	}

	public void Bind(DataContext context)
	{
		if (context.ContainsKey(m_Key)) {
			m_Graphic.color = (Color) context[m_Key];
		}
	}
}