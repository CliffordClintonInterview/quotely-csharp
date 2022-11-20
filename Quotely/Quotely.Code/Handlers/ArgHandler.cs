using Quotely.Code.Models;
using Quotely.Code.Services;

namespace Quotely.Code.Handlers;

public class ArgHandler : IArgHandler
{
    private readonly IQuoteService _quoteService;

    public ArgHandler(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    public async Task<string> ParseArgumentAsync(string[] args)
    {
        string[] validLanguage = { "en", "ru" };
        try
        {
            var languageArg = args.Length == 1 ? args[0] : "English";
            var language = languageArg.ToLower()[..2];

            if (!validLanguage.Contains(language))
            {
                return GetUsage();
            }

            Quote quote = await _quoteService.GetQuoteAsync(language);
            return quote.Output;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private string GetUsage()
    {
        return "Quotely.Code [Russian | English]";
    }
}
