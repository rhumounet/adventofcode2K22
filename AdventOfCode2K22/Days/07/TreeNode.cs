internal class TreeNode
{
    public enum NodeType {
        DIR, FILE
    }
    public string Name { get; set; }
    public readonly IList<TreeNode> Nodes = new List<TreeNode>();
    private readonly int? _fileSize;
    public NodeType Type => _fileSize.HasValue ? NodeType.FILE : NodeType.DIR;
    public int Size => Nodes.Any() ? Nodes.Sum(n => n.Size) : _fileSize ?? 0;

    public TreeNode(string name, int? size = null)
    {
        Name = name;
        _fileSize = size;
    }
}