public abstract class AbstractSolver : ISolver {
    
    public async Task<string> Solve(string? input = null)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException(nameof(input));
        }
        else
        {
            using var reader = new StreamReader(input);
            return await CoreSolve(reader);
        }
    }

    internal virtual Task<string> CoreSolve(StreamReader reader) 
    {
        throw new NotImplementedException();
    }
}