using System.Collections.Generic;

public class ObjectPool<T> where T : class, IPoolable
{
    private readonly Stack<T> pool = new();
    private readonly IFactory<T> factory;

    public ObjectPool(IFactory<T> factory)
    {
        this.factory = factory;
    }

    public T Get()
    {
        var item = pool.Count > 0 ? pool.Pop() : factory.Create();
        item.OnGetFromPool();
        return item;
    }

    public void Return(T item)
    {
        item.OnReturnToPool();
        pool.Push(item);
    }
}