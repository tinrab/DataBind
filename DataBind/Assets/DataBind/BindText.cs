using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Text))]
public class BindText : MonoBehaviour, IBindable
{
    private string m_OriginalText;
    private Text m_Text;

    public string key
    {
        get { return null; }
    }

    public void Bind(DataContext context)
    {
        if (m_OriginalText == null) {
            m_Text = GetComponent<Text>();
            m_OriginalText = m_Text.text;
        }

        var matches = Regex.Matches(m_OriginalText, @"\{\{[^}]*}}");
        var any = false;

        for (var i = 0; i < matches.Count; i++) {
            var m = matches[i];
            var key = m.Value.Substring(2, m.Value.Length - 4)
                       .Split(':')[0];

            if (context.ContainsKey(key)) {
                any = true;
                break;
            }
        }

        if (!any) {
            return;
        }

        m_Text.text = Regex.Replace(m_OriginalText, @"\{\{[^}]*}}", m =>
        {
            var target = m.Value.Substring(2, m.Value.Length - 4)
                          .Split(':');
            var key = target[0];
            var val = context[key];

            if (target.Length == 2 && context[key] is IFormattable) {
                var format = target[1];

                return ((IFormattable)val).ToString(format, CultureInfo.CurrentCulture);
            }

            return val.ToString();
        });
    }
}