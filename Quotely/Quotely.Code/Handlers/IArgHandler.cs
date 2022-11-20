namespace Quotely.Code.Handlers;

public interface IArgHandler
{
    public Task<string> ParseArgumentAsync(string[] args);
}
