using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Day8b {
  public class Solution {
    public static long Task2() {
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
    private string[] FinalNodes = [];
    private string[] CurrentNodes = [];
    private long[] CycleLength = []; 
    private int Index = 0;
    private long Steps = 0;
    private Dictionary<string, Node> Nodes = [];

    public void AddNode(Node node) {
      Nodes.Add(node.Name, node);
      if (node.Name.EndsWith('Z')) {
        FinalNodes = [..FinalNodes, node.Name];
      }

      if (node.Name.EndsWith('A')) {
        CurrentNodes = [..CurrentNodes, node.Name];
        CycleLength = [..CycleLength, 0];
      }
    }

    private char NextInstruction() {
      if (Index >= Queue.Length) {
        Index = 0;
      }

      return Queue[Index++];
    }

    private string[] NextNodes() {
      char nextInstruction = NextInstruction();
      return CurrentNodes.Select(nodeName => Nodes[nodeName].GetNextNode(nextInstruction)).ToArray();
    }

    private bool FinishCondition() {
      return CycleLength.All(cycleLength => cycleLength > 0);
    }

    private void AnalyzeHarmony() {
      
      for (int i = 0; i < CycleLength.Length; i++) {
        if (FinalNodes.Contains(CurrentNodes[i])) {
          if (CycleLength[i] == 0) {
            CycleLength[i] = Steps;
          } 
        }
      }
    }

    private void FindCycles() {
      Steps = 0;
      while (!FinishCondition()) {
        CurrentNodes = NextNodes();
        Steps++;
        AnalyzeHarmony();
      }
    }

    public long EscapeWasteland() {
      FindCycles();

      long shortestCycle = CycleLength.Min();
      long steps = shortestCycle;
      while(!CycleLength.All(cycle => steps % cycle == 0)) {
        steps += shortestCycle;
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