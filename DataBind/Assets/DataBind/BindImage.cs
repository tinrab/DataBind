using UnityEngine;
using UnityEngine.UI;

public class BindImage : MonoBehaviour, IBindable
{
	[SerializeField]
	private Image m_Image;
	[SerializeField]
	private string m_Key;

	public string key {
		get { return m_Key; }
	}

	public void Bind(DataContext context)
	{
		if (context.ContainsKey(m_Key)) {
			m_Image.sprite = (Sprite) context[m_Key];
		}
	}
}