# DataBind
Simple data binding for unity

## Usage
1. Import package file DataBind.unitypackage into Unity.
2. Add `DataBindContext` to parent object.
3. Add `Bind*` components to children and set `key` property.
4. Use `DataBindContext::Bind(key, value)` to bind values. Components will automatically be updated.

Check out Demo/DemoScene for details.

## Custom Binds 
Package contains components for binding UI Text, UI Image's sprite and UI Graphics's color. Custom binds can be created simply by implementing `IBndable` interface.

```csharp
using UnityEngine;
using UnityEngine.UI;

public class BindPosition : MonoBehaviour, IBindable
{
	[SerializeField]
	private Transform m_Target;
	[SerializeField]
	private string m_Key;

	public void Bind(DataBindContext context)
	{
		if (context.ContainsKey(m_Key)) {
			m_Target.position = (Vector3)context[m_Key];
		}
	}
}
```
