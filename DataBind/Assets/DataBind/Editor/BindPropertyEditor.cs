using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (BindProperty))]
public class BindPropertyEditor : Editor
{
	public override void OnInspectorGUI()
	{
		var obj = (BindProperty) serializedObject.targetObject;
		var source = serializedObject.FindProperty("m_Source");
		var destination = serializedObject.FindProperty("m_Destination");

		EditorGUILayout.PropertyField(source);

		if (obj.source) {
			var propertyDesc = ComponentsProperties(obj.source, obj.sourceProperty);

			if (propertyDesc != null) {
				obj.source = propertyDesc.component;
				obj.sourceProperty = propertyDesc.name;
			}
		}

		EditorGUILayout.PropertyField(destination);

		if (obj.destination) {
			var propertyDesc = ComponentsProperties(obj.destination, obj.destinationProperty);

			if (propertyDesc != null) {
				obj.destination = propertyDesc.component;
				obj.destinationProperty = propertyDesc.name;
			}
		}

		EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Direction"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Update"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UpdateInEditMode"));

		if (obj.updateInEditMode) {
			obj.UpdateBind();
		}

		serializedObject.ApplyModifiedProperties();
	}

	private PropertyDescription ComponentsProperties(Component component, string selectedProperty)
	{
		var components = component.GetComponents<Component>();
		var propertyNames = new List<string>();
		var properties = new List<PropertyDescription>();
		var selectedIndex = -1;

		foreach (var comp in components) {
			foreach (var property in comp.GetType().GetProperties()) {
				if (property.Name == selectedProperty) {
					selectedIndex = propertyNames.Count;
				}

				propertyNames.Add(comp.name + "/" + property.Name);
				properties.Add(new PropertyDescription(property.Name, comp));
			}
		}

		var newSelected = EditorGUILayout.Popup("Property", selectedIndex, propertyNames.ToArray());

		if (newSelected != selectedIndex) {
			return properties[newSelected];
		}

		return null;
	}

	private class PropertyDescription
	{
		public PropertyDescription(string name, Component component)
		{
			this.name = name;
			this.component = component;
		}

		public string name { get; set; }
		public Component component { get; set; }
	}
}