using System.ComponentModel;

namespace Day15 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("15");

      InstructionSet instructionSet = new(lines.First());

      return instructionSet.GetIntructionHashSum();
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("15");
      
      InstructionSet instructionSet = new(lines.First());
      instructionSet.RunInstructions();
      // instructionSet.Print();

      return instructionSet.GetFocusingPower();
    }
  }

  class InstructionSet {
    readonly string[] Instructions;
    readonly Box[] Boxes;

    public InstructionSet (string line) {
      Instructions = line.Split(",");
      Boxes = new Box[256];

      for (int i = 0; i < 256; i++) {
        Boxes[i] = new Box(i);
      }
    }

    static int Hash(string instruction) {
      char output = (char) 0;

      foreach (char character in instruction) {
        output += character;
        output *= (char) 17;
        output %= (char) 256;
      }

      return output;
    }

    public int GetIntructionHashSum() {
      int output = 0;
      foreach (string instruction in Instructions) {
        output += Hash(instruction);
      }
      return output;
    }

    void RunInstruction(string instruction) {
      string[] parts = instruction.Split(['=', '-']);
      int boxNumber = Hash(parts[0]);

      if (parts[1].Length == 0) {
        Boxes[boxNumber].RemoveLense(parts[0]);
      } else {
        Boxes[boxNumber].AddLense(new (parts[0], int.Parse(parts[1])));
      }
    }

    public void RunInstructions() {
      foreach(string instruction in Instructions) {
        RunInstruction(instruction);
      }
    }

    public int GetFocusingPower() {
      int sum = 0;

      foreach(Box box in Boxes) {
        sum += box.BoxFocusingPower();
      }

      return sum;
    }

    public void Print() {
      foreach(Box box in Boxes) {
        box.Print();
      }
    }
  }

  class Box(int index) {
    Lense[] Lenses = [];
    readonly int BoxIndex = index;

    public void AddLense(Lense lense) {
      for (int i = 0; i < Lenses.Length; i++) {
        if (Lenses[i].Label == lense.Label) {
          Lenses[i].FocalLength = lense.FocalLength;
          return;
        }
      }

      Lenses = [..Lenses, lense];
    }

    public void RemoveLense(string label){
      Lenses = Lenses.Where(old => old.Label != label).ToArray();
    }

    public int BoxFocusingPower() {
      int sum = 0;

      for (int i = 0; i < Lenses.Length; i++) {
        sum += Lenses[i].FocalLength * (i + 1) * (BoxIndex + 1);
      }

      return sum;
    }

    public void Print() {
      if (Lenses.Length > 0) {
        Console.Write($"Box {BoxIndex}: [ ");
        foreach (Lense lense in Lenses){
          Console.Write($"{lense.Label} {lense.FocalLength}, ");
        }
        Console.Write("\b\b  ]\n");
      }
    }
  }

  struct Lense (string label, int focalLength) {
    public readonly string Label = label;
    public int FocalLength = focalLength;
  }
}