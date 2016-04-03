using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

[RequireComponent(typeof(Text))]
public class BindText : MonoBehaviour, IBindable
{
	private Text m_Text;
	private string m_OriginalText;

	private void Awake()
	{
		m_Text = GetComponent<Text>();
		m_OriginalText = m_Text.text;
	}

	public void Bind(DataContext context)
	{
		m_Text.text = Regex.Replace(m_OriginalText, @"\{\{[^}]*}}", m => {
			var key = m.Value.Substring(2, m.Value.Length - 4);

			if (context.ContainsKey(key)) {
				return context[key].ToString();
			}

			return m.Value;
		});
	}
}
