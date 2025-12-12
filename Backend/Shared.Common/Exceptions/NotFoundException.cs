namespace Shared.Common.Exceptions;
public class NotFoundException<TEntity, TKey>(TKey key) : Exception($"{typeof(TEntity).Name} with key '{key}' was not found.")
{
}