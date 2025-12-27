namespace AOTInterpreter.Tests;

public class EvaluatorTests
{
    private readonly Evaluator _evaluator = new();

    [Fact]
    public void BasicAddition_ShouldComputeCorrectSum()
    {
        var expr = new BinaryExpr(
            new NumberExpr(5),
            TokenType.Plus,
            new NumberExpr(10)
        );

        var program = new BlockStmt([new PrintStmt(expr)]);

        /* TODO: update evaluator to return computed values instead of side effecting
         to stdout (allow for proper asserts) */
        
        _evaluator.Execute(program);
    }

    [Fact]
    public void Assignment_ShouldStoreVariableInEnvironment()
    {
        const string varName = "x";
        var assignment = new AssignStmt(varName, new NumberExpr(42));

        _evaluator.Execute(new BlockStmt([assignment]));

        // TODO 
    }
}