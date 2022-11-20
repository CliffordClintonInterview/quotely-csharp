using System.Net.Http.Json;
using Quotely.Code.Models;

namespace Quotely.Code.Services;

public class QuoteService : IQuoteService
{
    private readonly string _quoteApi = "http://api.forismatic.com/api/1.0/?method=getQuote&format=json";
    private readonly IHttpClientFactory _httpClientFactory;

    public QuoteService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<Quote> GetQuoteAsync(string Language)
    {
        var httpClient = _httpClientFactory.CreateClient("Forismatic");
        
        try
        {
            var apiUri = new Uri($"{_quoteApi}&lang={Language}");
            
            var quoteResponse = await httpClient.GetFromJsonAsync<QuoteResponse>(apiUri);

            var quoteString = quoteResponse!.quoteText; 
            var quoteAuthor = string.IsNullOrEmpty(quoteResponse.quoteAuthor) ? "Unknown" : quoteResponse.quoteAuthor;
        
            return new Quote(QuoteString: quoteString, Author: quoteAuthor);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
