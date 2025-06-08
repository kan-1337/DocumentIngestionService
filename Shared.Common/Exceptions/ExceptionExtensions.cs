namespace Shared.Common.Exceptions;
public static class ExceptionExtensions
{
    public static bool IsNotFoundException<TEntity, TKey>(this Exception ex, out string message)
    {
        if (ex is NotFoundException<TEntity, TKey> nfEx)
        {
            message = nfEx.Message;
            return true;
        }

        message = string.Empty;
        return false;
    }
}
