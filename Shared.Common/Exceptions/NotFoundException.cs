namespace Shared.Common.Exceptions;
public class NotFoundException<TEntity, TKey> : Exception
{
    public NotFoundException(TKey key)
        : base($"{typeof(TEntity).Name} with key '{key}' was not found.")
    {
    }
}