namespace AOTInterpreter.Tests;

public class LexerTests
{
    [Fact]
    public void NextToken_Should_Return_Plus_For_Plus_Sign()
    {
        var lexer = new Lexer("+");
        var token = lexer.NextToken();
        
        Assert.Equal(TokenType.Plus, token.Type);
        Assert.Equal("+", token.Text);
    }
}