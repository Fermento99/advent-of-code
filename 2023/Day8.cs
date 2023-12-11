namespace Day8 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("08");

      Instructions instructions = new(lines.First());

      foreach (string line in lines.Skip(2)) {
        instructions.AddNode(new(line));
      }

      return instructions.EscapeWasteland();
    }
  }

  class Instructions(string queue) {
    private readonly string Queue = queue;
    private readonly string FinalNode = "ZZZ";
    private string CurrentNode = "AAA";
    private int Index = 0;
    private Dictionary<string, Node> Nodes = [];

    public void AddNode(Node node) {
      Nodes.Add(node.Name, node);
    }

    private char NextInstruction() {
      if (Index >= Queue.Length) {
        Index = 0;
      }

      return Queue[Index++];
    }

    private string NextNode() {
      return Nodes[CurrentNode].GetNextNode(NextInstruction());
    }

    public int EscapeWasteland() {
      int steps = 0;
      while (CurrentNode != FinalNode) {
        CurrentNode = NextNode();
        steps++;
      }
      return steps;
    }

  }

  class Node {
    private readonly string Left;
    private readonly string Right;
    public string Name;

    public Node (string line) {
      string[] parts = line.Replace("(", "").Replace(")", "").Split(" = ");
      Name = parts[0];
      string[] sides = parts[1].Split(", ");
      Left = sides[0];
      Right = sides[1];
    }

    public string GetNextNode(char direction) {
      if (direction == 'L') {
        return Left;
      }

      return Right;
    }
  }
}