using System.Diagnostics;
using NUnit.Framework;

public class SolverBaseTest<T> where T : AbstractSolver
{
    private AbstractSolver _solver;
    private Stopwatch _stopWatch;
    private string _path;
    public SolverBaseTest(AbstractSolver solver, string path)
    {
        _solver = solver;
        _path = path;
        _stopWatch = new Stopwatch();
    }

    [Test]
    public void Check_null_path()
    {
        Assert.ThrowsAsync<ArgumentNullException>(async delegate { await _solver.Solve(); });
    }

    [Test]
    public async Task Correct_input()
    {
        _stopWatch.Start();
        var result = await _solver.Solve(_path);
        _stopWatch.Stop();
        Console.WriteLine($"{typeof(T).FullName} | result: {result}, elapsedTime: {_stopWatch.ElapsedMilliseconds}ms");
        Assert.Pass();
    }
}