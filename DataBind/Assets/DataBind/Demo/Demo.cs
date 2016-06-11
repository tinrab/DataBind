using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
	class NumberItem
	{
		public int number { get; set; }
	}

	private DataBindContext m_Context;
	private ObservableList m_NumberItems;

	private void Awake()
	{
		m_Context = GetComponent<DataBindContext>();
		m_NumberItems = new ObservableList("Items");
		m_Context["Items"] = m_NumberItems;

	    m_Context["Number"] = 0;
	}

	public void OnRandomNumberClick()
	{
		int n = Random.Range(1, 100000);
		m_Context["Number"] = n;

		m_NumberItems.Add(new NumberItem {
			number = n
		});
	}

	public void OnRandomColorClick()
	{
		m_Context["Color"] = Random.ColorHSV(0.0f, 1.0f, 0.75f, 0.75f, 0.75f, 0.75f);
	}
}
