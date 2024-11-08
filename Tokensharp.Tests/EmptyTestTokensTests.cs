﻿namespace Tokensharp.Tests;

public class EmptyTestTokensTests : TokenizerTestBase<EmptyTokens>
{
    [Test]
    public void Number()
    {
        RunTest("1", EmptyTokens.Number, lexeme: "1");
    }

    [Test]
    public void NumberFollowedByText()
    {
        RunTest("1T",
        [
            new TestCase<EmptyTokens>(TokenType<EmptyTokens>.Number, "1"),
            new TestCase<EmptyTokens>(TokenType<EmptyTokens>.Text, "T")
        ]);
    }

    [Test]
    public void TextFollowedByNumber()
    {
        RunTest("T1",
        [
            new TestCase<EmptyTokens>(TokenType<EmptyTokens>.Text, "T"),
            new TestCase<EmptyTokens>(TokenType<EmptyTokens>.Number, "1")
        ]);
    }
}