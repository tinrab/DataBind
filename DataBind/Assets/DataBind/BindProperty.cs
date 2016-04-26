using System.Reflection;
using UnityEngine;

public class BindProperty : MonoBehaviour
{
	public enum Direction
	{
		SourceUpdatesDestination,
		DestinationUpdatesSource
	}

	public enum UpdateMethod
	{
		OnUpdate,
		OnLateUpdate,
		OnFixedUpdate
	}

	private PropertyInfo m_CachedDestinationProperty;
	private PropertyInfo m_CachedSourceProperty;

	[SerializeField]
	private Component m_Destination;
	[SerializeField]
	private string m_DestinationProperty;
	[SerializeField]
	private Direction m_Direction;
	[SerializeField]
	private Component m_Source;
	[SerializeField]
	private string m_SourceProperty;
	[SerializeField]
	private UpdateMethod m_Update;
	[SerializeField]
	private bool m_UpdateInEditMode;

	public Component source {
		get { return m_Source; }
		set { m_Source = value; }
	}

	public Component destination {
		get { return m_Destination; }
		set { m_Destination = value; }
	}

	public string sourceProperty {
		get { return m_SourceProperty; }
		set { m_SourceProperty = value; }
	}

	public string destinationProperty {
		get { return m_DestinationProperty; }
		set { m_DestinationProperty = value; }
	}

	public Direction direction {
		get { return m_Direction; }
	}

	public UpdateMethod updateMethod {
		get { return m_Update; }
	}

	public bool updateInEditMode {
		get { return m_UpdateInEditMode; }
	}

	private void Update()
	{
		if (m_Update == UpdateMethod.OnUpdate) {
			UpdateBind();
		}
	}

	private void FixedUpdate()
	{
		if (m_Update == UpdateMethod.OnFixedUpdate) {
			UpdateBind();
		}
	}

	private void LateUpdate()
	{
		if (m_Update == UpdateMethod.OnLateUpdate) {
			UpdateBind();
		}
	}

	public void UpdateBind()
	{
		if (m_SourceProperty == null || m_DestinationProperty == null) {
			return;
		}

		if (m_CachedSourceProperty == null || m_CachedSourceProperty.Name != m_SourceProperty
		    || m_CachedDestinationProperty == null || m_CachedDestinationProperty.Name != m_DestinationProperty) {
			Cache();
		}

		switch (m_Direction) {
			case Direction.SourceUpdatesDestination:
				if (m_CachedDestinationProperty.PropertyType == typeof (string)) {
					m_CachedDestinationProperty.SetValue(m_Destination, m_CachedSourceProperty.GetValue(m_Source, null).ToString(),
						null);
				} else {
					m_CachedDestinationProperty.SetValue(m_Destination, m_CachedSourceProperty.GetValue(m_Source, null), null);
				}
				break;
			case Direction.DestinationUpdatesSource:
				if (m_CachedSourceProperty.PropertyType == typeof (string)) {
					m_CachedSourceProperty.SetValue(m_Source, m_CachedDestinationProperty.GetValue(m_Destination, null).ToString(),
						null);
				} else {
					m_CachedSourceProperty.SetValue(m_Source, m_CachedDestinationProperty.GetValue(m_Destination, null), null);
				}
				break;
		}
	}

	public void Cache()
	{
		m_CachedSourceProperty = m_Source.GetType().GetProperty(m_SourceProperty);
		m_CachedDestinationProperty = m_Destination.GetType().GetProperty(m_DestinationProperty);
	}
}