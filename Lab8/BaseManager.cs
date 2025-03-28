public abstract class BaseManager<T>
{
    protected List<T> items = new List<T>();

    public virtual IEnumerable<T> GetItems()
    {
        return items.ToList();
    }

    public virtual void AddItem(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        items.Add(item);
    }

    public virtual void RemoveItem(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        items.Remove(item);
    }
}
