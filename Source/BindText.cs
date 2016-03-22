using UnityEngine;
using UnityEngine.UI;

public class BindText : MonoBehaviour, IBindable
{
	[SerializeField]
	private Text m_Text;
	[SerializeField]
	private string m_Key;
	[SerializeField]
	private string m_DefaultValue;

	public void Bind(DataBindContext context)
	{
		if (context.ContainsKey(m_Key)) {
			m_Text.text = context[m_Key].ToString();
		} else {
			m_Text.text = m_DefaultValue;
		}
	}
}
