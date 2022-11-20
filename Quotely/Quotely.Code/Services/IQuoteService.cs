using Quotely.Code.Models;

namespace Quotely.Code.Services;

public interface IQuoteService
{
    public Task<Quote> GetQuoteAsync(string Language);
}
