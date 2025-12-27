namespace AOTInterpreter.Tests;

public class ParserTests
{
    [Theory]
    [InlineData("print 5 + 3 * 2", "11\n")] 
    [InlineData("x = 10\nprint x / 2", "5\n")]
    [InlineData("print (5 + 3) * 2", "16\n")] 
    public void FullPipeline_Should_Evaluate_Correctly(string input, string expectedOutput)
    {
        var output = new StringWriter();
        Console.SetOut(output);

        try 
        {
            var lexer = new Lexer(input);
            var tokens = new List<Token>();
            Token token;
            do
            {
                token = lexer.NextToken();
                tokens.Add(token);
            } while (token.Type != TokenType.EOF);

            var parser = new Parser(tokens);
            var block = parser.Parse();

            var evaluator = new Evaluator();
            evaluator.Execute(block);

            Assert.Equal(expectedOutput, output.ToString());
        }
        finally
        {
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }
    }

    [Fact]
    public void Parser_Should_Throw_Exception_On_Invalid_Syntax()
    {
        var tokens = new List<Token> 
        { 
            new(TokenType.Print, "print", 1, 1), 
            new(TokenType.Plus, "+", 1, 7),     
            new(TokenType.EOF, "", 1, 8) 
        };
        var parser = new Parser(tokens);

        Assert.Throws<Exception>(() => parser.Parse());
    }
}