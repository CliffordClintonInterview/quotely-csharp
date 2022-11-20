namespace Quotely.Code.Models;

public readonly record struct Quote(string QuoteString, string Author)
{
    public string Output => $"Quote: {QuoteString}\nAuthor: {Author}";
}
