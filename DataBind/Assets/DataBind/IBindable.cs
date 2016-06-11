public interface IBindable
{
	string key { get; }
	void Bind(DataContext context);
}
