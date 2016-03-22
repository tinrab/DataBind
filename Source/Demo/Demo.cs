using UnityEngine;

public class Demo : MonoBehaviour
{
	private DataBindContext m_Context;

	private void Awake()
	{
		m_Context = GetComponent<DataBindContext>();
	}

	public void OnRandomNumberClick()
	{
		m_Context["Number"] = Random.Range(1, 101);
	}

	public void OnRandomColorClick()
	{
		m_Context["Color"] = Random.ColorHSV(0.0f, 1.0f, 0.75f, 0.75f, 0.75f, 0.75f);
	}
}
