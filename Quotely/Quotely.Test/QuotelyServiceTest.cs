using System.Net;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;
using Quotely.Code.Services;
using Xunit;

namespace Quotely.Test;

public class QuotelyServiceTest
{
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();
    private readonly Mock<HttpMessageHandler> _httpMessageHandler = new();
    private readonly string _quoteApi = "http://api.forismatic.com/api/1.0/?method=getQuote&format=json&lang=en";

    [Fact]
    public async Task ApiResponse_ShouldDeserialize_Successfully()
    {
        var quoteText = "Now is the time for all good men";
        var quoteAuthor = "Me";
        HttpResponseMessage result = new HttpResponseMessage()
        {
            Content = JsonContent.Create(new
            {
                quoteText,
                quoteAuthor,
                senderName = "",
                senderLink = "",
                quoteLink = "http://forismatic.com/en/fedd7a0d8e/"
            }),
            StatusCode = HttpStatusCode.OK
        };
        
        _httpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(result).Verifiable();

        var client = new HttpClient(_httpMessageHandler.Object);
        _httpClientFactory.Setup(x => x.CreateClient("Forismatic")).Returns(client);

        var quoteService = new QuoteService(_httpClientFactory.Object);
        //
        var res = await quoteService.GetQuoteAsync("en");
        Assert.Equal(res.QuoteString, quoteText);
        Assert.Equal(res.Author, quoteAuthor);
        Assert.Equal(res.Output, $"Quote: {quoteText}\nAuthor: {quoteAuthor}");
    }
    
    [Fact]
    public async Task ApiResponse_ShouldHaveUnknownAuthor_WhenAuthorIsNull()
    {
        var quoteText = "Now is the time for all good men";
        HttpResponseMessage result = new HttpResponseMessage()
        {
            Content = JsonContent.Create(new
            {
                quoteText,
                quoteAuthor = "",
                senderName = "",
                senderLink = "",
                quoteLink = "http://forismatic.com/en/fedd7a0d8e/"
            }),
            StatusCode = HttpStatusCode.OK
        };
        
        _httpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(result).Verifiable();

        var client = new HttpClient(_httpMessageHandler.Object);
        _httpClientFactory.Setup(x => x.CreateClient("Forismatic")).Returns(client);

        var quoteService = new QuoteService(_httpClientFactory.Object);
        //
        var res = await quoteService.GetQuoteAsync("en");
        Assert.Equal(res.Author, "Unknown");
    }
}
