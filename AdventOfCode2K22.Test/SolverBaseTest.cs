using NUnit.Framework;

[TestFixture]
public class SolverBaseTest<T> where T : AbstractSolver
{
    private AbstractSolver _solver;
    private string _path;
    public SolverBaseTest(AbstractSolver solver, string path)
    {
        _solver = solver;
        _path = path;
    }

    [Test]
    public void Check_null_path()
    {
        Assert.ThrowsAsync<ArgumentNullException>(async delegate { await _solver.Solve(); });
    }

    [Test]
    public async Task Correct_input()
    {
        Assert.Pass($"result: {await _solver.Solve(_path)}");
    }
}