using System.Text.RegularExpressions;

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
        TreeNode root = new TreeNode("root");
        TreeNode? currentNode = null;
        var previousNodes = new Stack<TreeNode>();
        int trueIt = 0;
        for (int i = 0; i < input.Length; i++)
        {
            trueIt++;
            var line = input[i];
            if ((match = cdMatch.Match(line)).Success)
            {
                targetDir = match.Groups["name"].Value;
                if (targetDir == "/")
                {
                    previousNodes.Push(root);
                    currentNode = root;
                }
                else if (targetDir == ".." && previousNodes.Any())
                    currentNode = previousNodes.Count == 1 ? previousNodes.First() : previousNodes.Pop();
                else
                {
                    if (currentNode == root)
                        currentNode = root.Nodes.FirstOrDefault(n => n.Name == targetDir);
                    else if (currentNode != null)
                    {
                        previousNodes.Push(currentNode);
                        currentNode = currentNode.Nodes.FirstOrDefault(n => n.Name == targetDir);
                    }
                }
            }
            else if ((match = lsMatch.Match(line)).Success)
            {
                i++;
                while (i < input.Length && commandMatch.Match((line = input[i])).Success == false)
                {
                    trueIt++;
                    if ((match = fileMatch.Match(line)).Success && currentNode != null)
                    {
                        currentNode.Nodes.Add(new TreeNode(match.Groups["name"].Value, int.Parse(match.Groups["size"].Value)));
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

    internal static List<TreeNode> GetMaximumSizeNodes(this TreeNode treeNode, int maximumSize)
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
    
    internal static List<TreeNode> GetMinimumTreeSize(this TreeNode treeNode, int minimumSize)
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