using System.Text.RegularExpressions;

namespace Day19;

public class Part1 : AbstractSolver
{
    internal override async Task<string> CoreSolve(StreamReader reader)
    {
        var content = await reader.ReadToEndAsync();
        var lines = content.Split("\r\n");
        var blueprints = LoadBlueprints(lines);
        var id = 1;
        var totalQuality = 0;
        foreach (var blueprint in blueprints)
        {
            var production = new Production();
            var totalCosts = blueprint.TotalCosts;
            var averageCosts = blueprint.AverageCosts;
            for (int i = 0; i < 24; i++)
            {
                float
                    oreOutput = production.Output[Resource.Ore],
                    clayOutput = Math.Max(production.Output[Resource.Clay], 1f / 1000),
                    obsidianOutput = Math.Max(production.Output[Resource.Obsidian], 1f / 1000),
                    geodeOutput = production.Output[Resource.Geode];

                int
                    oreTurnCost = (int)Math.Ceiling(blueprint.Machines[Resource.Ore].Costs[Resource.Ore] / oreOutput),
                    clayTurnCost = (int)Math.Ceiling(blueprint.Machines[Resource.Clay].Costs[Resource.Ore] / oreOutput),
                    obsidianOreTurnCost = (int)Math.Ceiling(blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore] / oreOutput),
                    obsidianClayTurnCost = (int)Math.Ceiling(blueprint.Machines[Resource.Obsidian].Costs[Resource.Clay] / clayOutput),
                    geodeOreTurnCost = (int)Math.Ceiling(blueprint.Machines[Resource.Geode].Costs[Resource.Ore] / oreOutput),
                    geodeObsidianTurnCost = (int)Math.Ceiling(blueprint.Machines[Resource.Geode].Costs[Resource.Obsidian] / obsidianOutput);

                int
                    oreTurnToBuild = (int)Math.Ceiling(
                        (blueprint.Machines[Resource.Ore].Costs[Resource.Ore] - Math.Min(
                            production.Resources[Resource.Ore],
                            blueprint.Machines[Resource.Ore].Costs[Resource.Ore]))
                        / oreOutput),
                    clayTurnToBuild = (int)Math.Ceiling(
                        (blueprint.Machines[Resource.Clay].Costs[Resource.Ore] - Math.Min(
                            production.Resources[Resource.Ore],
                            blueprint.Machines[Resource.Clay].Costs[Resource.Ore]))
                        / oreOutput),
                    obsidianOreTurnToBuild = (int)Math.Ceiling(
                        (blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore] - Math.Min(
                            production.Resources[Resource.Ore],
                            blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore]))
                        / oreOutput),
                    obsidianClayTurnToBuild = (int)Math.Ceiling(
                        (blueprint.Machines[Resource.Obsidian].Costs[Resource.Clay] - Math.Min(
                            production.Resources[Resource.Clay],
                            blueprint.Machines[Resource.Obsidian].Costs[Resource.Clay]))
                        / clayOutput),
                    geodeOreTurnToBuild = (int)Math.Ceiling(
                        (blueprint.Machines[Resource.Geode].Costs[Resource.Ore] - Math.Min(
                            production.Resources[Resource.Ore],
                            blueprint.Machines[Resource.Geode].Costs[Resource.Ore]))
                        / oreOutput),
                    geodeObsidianTurnToBuild = (int)Math.Ceiling(
                        (blueprint.Machines[Resource.Geode].Costs[Resource.Obsidian] - Math.Min(
                            production.Resources[Resource.Obsidian],
                            blueprint.Machines[Resource.Geode].Costs[Resource.Obsidian]))
                        / obsidianOutput);


                var increases = new Dictionary<Resource, int> {
                    { Resource.Ore, 0 },
                    { Resource.Clay, 0 },
                    { Resource.Obsidian, 0 },
                    { Resource.Geode, 0 },
                };
                if (production.Resources[Resource.Ore] >= blueprint.Machines[Resource.Geode].Costs[Resource.Ore]
                    && production.Resources[Resource.Obsidian] >= blueprint.Machines[Resource.Geode].Costs[Resource.Obsidian])
                {
                    increases[Resource.Geode]++;
                    production.Resources[Resource.Ore] -= blueprint.Machines[Resource.Geode].Costs[Resource.Ore];
                    production.Resources[Resource.Obsidian] -= blueprint.Machines[Resource.Geode].Costs[Resource.Obsidian];
                }
                if ((Math.Max(geodeObsidianTurnToBuild, geodeOreTurnToBuild) > 2
                        || production.Resources[Resource.Ore] - blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore] + production.Output[Resource.Ore]
                            >= 
                            blueprint.Machines[Resource.Geode].Costs[Resource.Ore])
                    && geodeObsidianTurnCost > obsidianClayTurnCost
                    && production.Resources[Resource.Ore] >= blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore]
                    && production.Resources[Resource.Clay] >= blueprint.Machines[Resource.Obsidian].Costs[Resource.Clay])
                {
                    increases[Resource.Obsidian]++;
                    production.Resources[Resource.Ore] -= blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore];
                    production.Resources[Resource.Clay] -= blueprint.Machines[Resource.Obsidian].Costs[Resource.Clay];
                }
                if ((Math.Max(geodeObsidianTurnToBuild, geodeOreTurnToBuild) > 2
                        || production.Resources[Resource.Ore] - blueprint.Machines[Resource.Clay].Costs[Resource.Ore] + production.Output[Resource.Ore]
                            >= 
                            blueprint.Machines[Resource.Geode].Costs[Resource.Ore])
                    && (Math.Max(obsidianOreTurnToBuild, obsidianClayTurnToBuild) > 2
                        || production.Resources[Resource.Ore] - blueprint.Machines[Resource.Clay].Costs[Resource.Ore] + production.Output[Resource.Ore]
                            >=
                            blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore])
                    && production.Resources[Resource.Clay] <= blueprint.Machines[Resource.Obsidian].Costs[Resource.Clay] * 2
                    && obsidianClayTurnCost > clayTurnCost
                    && production.Resources[Resource.Ore] >= blueprint.Machines[Resource.Clay].Costs[Resource.Ore])
                {
                    increases[Resource.Clay]++;
                    production.Resources[Resource.Ore] -= blueprint.Machines[Resource.Clay].Costs[Resource.Ore];
                }
                if ((Math.Max(geodeObsidianTurnToBuild, geodeOreTurnToBuild) > 2
                        || production.Resources[Resource.Ore] 
                            >= 
                            blueprint.Machines[Resource.Geode].Costs[Resource.Ore] + production.Output[Resource.Ore])
                    && (Math.Max(obsidianOreTurnToBuild, obsidianClayTurnToBuild) > 2
                        || production.Resources[Resource.Ore] - blueprint.Machines[Resource.Ore].Costs[Resource.Ore] + production.Output[Resource.Ore]  
                            >= 
                            blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore])
                    && production.Resources[Resource.Ore] <= blueprint.Machines[Resource.Obsidian].Costs[Resource.Ore] * 2
                    && production.Resources[Resource.Ore] <= blueprint.Machines[Resource.Clay].Costs[Resource.Ore] * 2
                    && production.Resources[Resource.Ore] <= blueprint.Machines[Resource.Geode].Costs[Resource.Ore] * 2
                    && clayTurnCost > oreTurnCost
                    && production.Resources[Resource.Ore] >= blueprint.Machines[Resource.Ore].Costs[Resource.Ore])
                {
                    increases[Resource.Ore]++;
                    production.Resources[Resource.Ore] -= blueprint.Machines[Resource.Ore].Costs[Resource.Ore];
                }

                //collect resources
                production.Resources[Resource.Ore] += production.Output[Resource.Ore];
                production.Resources[Resource.Clay] += production.Output[Resource.Clay];
                production.Resources[Resource.Obsidian] += production.Output[Resource.Obsidian];
                production.Resources[Resource.Geode] += production.Output[Resource.Geode];

                //add production unit created with spending
                production.Output[Resource.Ore] += increases[Resource.Ore];
                production.Output[Resource.Clay] += increases[Resource.Clay];
                production.Output[Resource.Obsidian] += increases[Resource.Obsidian];
                production.Output[Resource.Geode] += increases[Resource.Geode];
            }
            totalQuality += id++ * production.Resources[Resource.Geode];
        }
        return $"{totalQuality}";
    }

    private static List<Blueprint> LoadBlueprints(string[] lines)
    {
        var r = new Regex[] {
            new Regex(@"Each ore robot costs (\d*) ore"),
            new Regex(@"Each clay robot costs (\d*) ore"),
            new Regex(@"Each obsidian robot costs (\d*) ore and (\d*) clay"),
            new Regex(@"Each geode robot costs (\d*) ore and (\d*) obsidian"),
        };
        var blueprints = new List<Blueprint>();
        foreach (var line in lines)
        {
            var blueprint = new Blueprint();
            blueprint.Machines[Resource.Ore] = new Machine
            {
                Costs = new Dictionary<Resource, int> {
                    {Resource.Ore, int.Parse(r[0].Match(line).Groups[1].Value)}
                }
            };
            blueprint.Machines[Resource.Clay] = new Machine
            {
                Costs = new Dictionary<Resource, int> {
                    {Resource.Ore, int.Parse(r[1].Match(line).Groups[1].Value)}
                }
            };
            blueprint.Machines[Resource.Obsidian] = new Machine
            {
                Costs = new Dictionary<Resource, int> {
                    {Resource.Ore, int.Parse(r[2].Match(line).Groups[1].Value)},
                    {Resource.Clay, int.Parse(r[2].Match(line).Groups[2].Value)}
                }
            };
            blueprint.Machines[Resource.Geode] = new Machine
            {
                Costs = new Dictionary<Resource, int> {
                    {Resource.Ore, int.Parse(r[3].Match(line).Groups[1].Value)},
                    {Resource.Obsidian, int.Parse(r[3].Match(line).Groups[2].Value)}
                }
            };
            blueprints.Add(blueprint);
        }
        return blueprints;
    }
}

internal class Production
{
    public Dictionary<Resource, int> Output { get; set; } = new Dictionary<Resource, int>
    {
        { Resource.Ore, 1 },
        { Resource.Clay, 0 },
        { Resource.Obsidian, 0 },
        { Resource.Geode, 0 },
    };
    public Dictionary<Resource, int> Resources { get; set; } = new Dictionary<Resource, int>
    {
        { Resource.Ore, 0 },
        { Resource.Clay, 0 },
        { Resource.Obsidian, 0 },
        { Resource.Geode, 0 },
    };
}

internal class Blueprint
{
    public Dictionary<Resource, Machine> Machines { get; set; } = new Dictionary<Resource, Machine>();
    public Dictionary<Resource, int> TotalCosts
    {
        get
        {
            return Machines
                .SelectMany(m => m.Value.Costs)
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Sum(g => g.Value));
        }
    }
    public Dictionary<Resource, double> AverageCosts
    {
        get
        {
            return Machines
                .SelectMany(m => m.Value.Costs)
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => (double)g.Sum(g => g.Value) / Machines.Count);
        }
    }
}

internal class Machine
{
    public Dictionary<Resource, int> Costs { get; set; } = new Dictionary<Resource, int>();
}

internal enum Resource
{
    Ore = 0,
    Clay = 1,
    Obsidian = 2,
    Geode = 3
}