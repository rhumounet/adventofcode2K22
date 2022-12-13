using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;

namespace Day7;

internal static class Helper
{
    internal static TreeNode CreateTree(string[] input)
    {
        Regex
            commandMatch = new Regex(@"^\$"),
            cdMatch = new Regex(@"^\$ cd (?<name>.*)"),
            lsMatch = new Regex(@"^\$ ls"),
            fileMatch = new Regex(@"^(?<size>\d*) (?<name>.*)"),
            dirMatch = new Regex(@"^dir (?<name>.*)");
        Match match;
        var targetDir = "";
        var root = new TreeNode("/");
        TreeNode? currentNode = null;
        var previousNodes = new Stack<TreeNode>();
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];
            if ((match = cdMatch.Match(line)).Success)
            {
                targetDir = match.Groups["name"].Value;
                switch (targetDir)
                {
                    case "/":
                        previousNodes.Push(root);
                        currentNode = root;
                        break;
                    case ".." when previousNodes.Any():
                        currentNode = previousNodes.Count == 1 ? previousNodes.First() : previousNodes.Pop();
                        break;
                    default:
                    {
                        if (currentNode == root)
                            currentNode = root.Nodes.FirstOrDefault(n => n.Name == targetDir);
                        else if (currentNode != null)
                        {
                            previousNodes.Push(currentNode);
                            currentNode = currentNode.Nodes.FirstOrDefault(n => n.Name == targetDir);
                        }

                        break;
                    }
                }
            }
            else if ((match = lsMatch.Match(line)).Success)
            {
                i++;
                while (i < input.Length && commandMatch.Match((line = input[i])).Success == false)
                {
                    if ((match = fileMatch.Match(line)).Success && currentNode != null)
                    {
                        currentNode.Nodes.Add(new TreeNode(match.Groups["name"].Value,
                            int.Parse(match.Groups["size"].Value)));
                    }
                    else if ((match = dirMatch.Match(line)).Success && currentNode != null)
                    {
                        currentNode.Nodes.Add(new TreeNode(match.Groups["name"].Value));
                    }

                    i++;
                }

                i--;
            }
        }

        return root;
    }

    internal static void Print(this TreeNode treeNode, int depth = 0)
    {
        var addendum = treeNode.Type switch
        {
            TreeNode.NodeType.DIR => "dir",
            TreeNode.NodeType.FILE => $"file, size={treeNode.Size}",
            _ => ""
        };
        Console.WriteLine($"{new string(Enumerable.Repeat(' ', depth).ToArray())}- {treeNode.Name} ({addendum})");
        if (!treeNode.Nodes.Any()) return;
        foreach (var child in treeNode.Nodes)
        {
            Print(child, depth + 1);
        }
    }

    internal static IEnumerable<TreeNode> GetMaximumSizeNodes(this TreeNode treeNode, int maximumSize)
    {
        var list = new List<TreeNode>();
        if (treeNode.Size <= maximumSize)
        {
            list.Add(treeNode);
        }

        list.AddRange(treeNode.Nodes
            .Where(n => n.Type == TreeNode.NodeType.DIR)
            .SelectMany(n => n.GetMaximumSizeNodes(maximumSize)));
        return list;
    }

    internal static IEnumerable<TreeNode> GetMinimumTreeSize(this TreeNode treeNode, int minimumSize)
    {
        var list = new List<TreeNode>();
        if (treeNode.Size >= minimumSize)
        {
            list.Add(treeNode);
        }

        list.AddRange(treeNode.Nodes
            .Where(n => n.Type == TreeNode.NodeType.DIR)
            .SelectMany(n => n.GetMinimumTreeSize(minimumSize)));
        return list;
    }
}