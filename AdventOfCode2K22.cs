using NUnit.Framework;

[TestFixture]
public class AdventOfCode2K22
{
    [Test]
    public void Part1_null_path()
    {
        var solver = new Part1Solver();
        Assert.ThrowsAsync<ArgumentNullException>(async delegate { await solver.Solve(); });
    }
    
    [Test]
    public async Task Part1_correct_input()
    {
        var solver = new Part1Solver();
        Assert.Pass($"result: {await solver.Solve("Day1/input.txt")}");
    }
    
    
    [Test]
    public async Task Part2_correct_input()
    {
        var solver = new Part2Solver();
        Assert.Pass($"result: {await solver.Solve("Day1/input.txt")}");
    }
}