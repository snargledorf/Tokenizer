﻿namespace Tokenizer.Tests;

public record CsvTokenTypes(string Lexeme, int Id) : TokenType<CsvTokenTypes>(Lexeme, Id), ITokenType<CsvTokenTypes>
{
    public static readonly CsvTokenTypes Comma = new(",", 0);
    public static readonly CsvTokenTypes EndOfRecord = new("\r\n", 1);
    public static readonly CsvTokenTypes DoubleQuote = EndOfRecord.Next("\"");

    public static IEnumerable<CsvTokenTypes> TokenTypes { get; } =
    [
        Comma,
        EndOfRecord,
        DoubleQuote
    ];
    
    public static CsvTokenTypes Create(string token, int id) => new(token, id);
}

public class CsvTests : TokenizerTestBase<CsvTokenTypes>
{
    [Test]
    public void CsvWithQuotesAndNewLine()
    {
        var testStr = """
                      test, 123
                      foo,"bar
                      bizz"
                      """;

        RunTest(testStr, 
        [
            new TestCase<CsvTokenTypes>(CsvTokenTypes.Text, "test"),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.Comma),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.WhiteSpace, " "),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.Number, "123"),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.EndOfRecord),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.Text, "foo"),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.Comma),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.DoubleQuote),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.Text, "bar"),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.EndOfRecord),
            new TestCase<CsvTokenTypes>(CsvTokenTypes.Text, "bizz")
        ]);
    }
}