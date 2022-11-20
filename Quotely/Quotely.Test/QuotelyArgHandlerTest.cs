using Moq;
using Quotely.Code.Handlers;
using Quotely.Code.Models;
using Quotely.Code.Services;
using Xunit;

namespace Quotely.Test;

public class QuotelyArgHandlerTest
{
    private readonly Mock<IQuoteService> _quoteService = new();
    private readonly Quote _englishQuote = new Quote(QuoteString: "Cool Quote", Author: "Clifford");
    private readonly Quote _russianQuote = new Quote(QuoteString: "Ни один человек", Author: "Генри Бигер");
    private readonly String _usage = "Quotely.Code [Russian | English]";

    [Fact]
    public async Task ArgHandler_ReturnsEnglishLanguage_FromEnglishArgument()
    {
        _quoteService.Setup(x => x.GetQuoteAsync("en"))
            .ReturnsAsync(_englishQuote);

        var argHandler = new ArgHandler(_quoteService.Object);

        string[] args = { "English" };
        var res = await argHandler.ParseArgumentAsync(args);
        
        Assert.Equal(_englishQuote.Output, res);
    }
    
    [Fact]
    public async Task ArgHandler_ReturnsRussianLanguage_FromRussianArgument()
    {
        _quoteService.Setup(x => x.GetQuoteAsync("ru"))
            .ReturnsAsync(_russianQuote);

        var argHandler = new ArgHandler(_quoteService.Object);

        string[] args = { "Russian" };
        var res = await argHandler.ParseArgumentAsync(args);
        
        Assert.Equal(_russianQuote.Output, res);
    }
    
    [Fact]
    public async Task ArgHandler_ReturnsEnglishLanguage_WhenGivenNoArgument()
    {
        _quoteService.Setup(x => x.GetQuoteAsync("en"))
            .ReturnsAsync(_englishQuote);
        _quoteService.Setup(x => x.GetQuoteAsync("ru"))
            .ReturnsAsync(_russianQuote);

        var argHandler = new ArgHandler(_quoteService.Object);

        string[] args = { };
        var res = await argHandler.ParseArgumentAsync(args);
        
        Assert.Equal(_englishQuote.Output, res);
    }
    
    [Fact]
    public async Task ArgHandler_PrintsUsage_WhenGivenInvalidOption()
    {
        _quoteService.Setup(x => x.GetQuoteAsync("en"))
            .ReturnsAsync(_englishQuote);
        _quoteService.Setup(x => x.GetQuoteAsync("ru"))
            .ReturnsAsync(_russianQuote);

        var argHandler = new ArgHandler(_quoteService.Object);

        string[] args = { "Spanish" };
        var res = await argHandler.ParseArgumentAsync(args);
        
        Assert.Equal(res, _usage);
    }
}
