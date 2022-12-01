namespace Shared;

public interface ISolver
{
    Task<string> Solve(string? input = null);
}