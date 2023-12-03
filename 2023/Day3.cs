using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Day3 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("03");

      Schematic schematic = new Schematic(lines.ToArray());

      return schematic.SumPartNumbers();
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("03");

      Schematic schematic = new Schematic(lines.ToArray());
      
      return schematic.SumGearRatio();
    }
  }

  class Schematic {
    public Symbol[] Symbols = [];
    public Number[] Numbers = [];

    public Schematic (string[] lines) {
      
      for (int row = 0; row < lines.LongCount(); row++) {
        Match[] numbers = Regex.Matches(lines[row], "\\d+").ToArray();
        foreach (Match number in numbers) {
          this.Numbers = this.Numbers.Append(new Number(number.Index, row, number.Length, int.Parse(number.ToString()))).ToArray();
        }

        Match[] symbols = Regex.Matches(lines[row], "[^\\d^\\.]").ToArray();
        foreach (Match symbol in symbols) {
          this.Symbols = this.Symbols.Append(new Symbol(symbol.Index, row, symbol.ToString())).ToArray();
        }
      }
    }

    public int SumPartNumbers () {
      int sum = 0;

      foreach (Number number in this.Numbers) {
        foreach (Symbol symbol in this.Symbols) {
          if (number.IsNeighbour(symbol)) {
            sum += number.value;
            break;
          }
        }
      }

      return sum;
    }

    public int SumGearRatio () {
      int sum = 0;

      foreach (Symbol symbol in this.Symbols) {
        Number[] adjecantNumbers = [];
        foreach (Number number in this.Numbers) {
          if (number.IsNeighbour(symbol)) {
            adjecantNumbers = adjecantNumbers.Append(number).ToArray();
          }
        }
        if (symbol.symbol == "*" && adjecantNumbers.Length == 2) {
          sum += adjecantNumbers[0].value * adjecantNumbers[1].value;
        }
      }

      return sum;
    }
  }

  class Number {
    public int y;
    public int startX;
    public int endX;
    public int value;

    public Number (int x, int y, int length, int value) {
      this.y = y;
      this.startX = x;
      this.endX = x + length - 1;
      this.value = value;
    }

    public bool IsNeighbour (int x, int y) {
      int distY = Math.Abs(this.y - y);
      return distY <= 1 && x >= this.startX - 1 && x <= this.endX + 1;
    }

    public bool IsNeighbour (Symbol symbol) {
      int distY = Math.Abs(this.y - symbol.y);
      return distY <= 1 && symbol.x >= this.startX - 1 && symbol.x <= this.endX + 1;
    }
  }

  class Symbol {
    public int x;
    public int y;
    public string symbol;

     public Symbol (int x, int y, string symbol) {
      this.x = x;
      this.y = y;
      this.symbol = symbol;
    }
  }  
}