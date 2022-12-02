using NUnit.Framework;

[TestFixture]
public class AdventOfCode2K22
{
    [Test]
    public void Part1_null_path()
    {
        var solver = new Day1.Part1Solver();
        Assert.ThrowsAsync<ArgumentNullException>(async delegate { await solver.Solve(); });
    }
    
    [Test]
    public async Task Day1_Part1_correct_input()
    {
        var solver = new Day1.Part1Solver();
        Assert.Pass($"result: {await solver.Solve("Day1/input.txt")}");
    }
    
    
    [Test]
    public async Task Day1_Part2_correct_input()
    {
        var solver = new Day1.Part2Solver();
        Assert.Pass($"result: {await solver.Solve("Day1/input.txt")}");
    }
    
    [Test]
    public async Task Day2_Part1_correct_input()
    {
        var solver = new Day2.Part1Solver();
        Assert.Pass($"result: {await solver.Solve("Day2/input.txt")}");
    }
    
    
    [Test]
    public async Task Day2_Part2_correct_input()
    {
        var solver = new Day2.Part2Solver();
        Assert.Pass($"result: {await solver.Solve("Day1/input.txt")}");
    }
}