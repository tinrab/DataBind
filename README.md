# DataBind
Simple data binding for unity.

## Usage
1. Import package file DataBind.unitypackage into Unity
2. Add `DataBindContext` to parent object
3. Add `Bind*` components to children
4. Use `dataBindContext[key] = value` to bind values

Check out Demo/DemoScene for details.

## Bind Text
Surround keys with double curly braces in UI Text and set values in parent `DataBindContext`

![alt text](https://50bdf9794352d3405148f2b7972a1c29b0637e4e.googledrive.com/host/0B5dN6w6eVDL5TEgxclhmN083ZVU/Capture.PNG)

```csharp
var context = GetComponent<DataBindContext>();
context["Username"] = "Bobby";
```
Result:

![alt text](https://50bdf9794352d3405148f2b7972a1c29b0637e4e.googledrive.com/host/0B5dN6w6eVDL5TEgxclhmN083ZVU/Capture2.PNG)

## Custom Binds 
Package contains components for binding UI Text, UI Image's sprite and UI Graphics's color. Custom binds can be created simply by implementing `IBindable` interface.

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
