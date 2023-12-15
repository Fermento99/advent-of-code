using System.Text.RegularExpressions;

namespace Day14 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("14");

      Map map = new(lines.ToArray());
      map.Tilt(0);

      return map.GetLoad();
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("14");

      Map map = new(lines.ToArray());
      map.TiltInCycles(1000000000);

      return map.GetLoad();
    }
  }

  class Map {
    Rock[] Rocks = [];
    readonly int Width;
    readonly int Height;

    public Map (string[] lines) {
      Height = lines.Length;
      Width = lines.First().Length;
      for (int y = 0; y < Height; y++) {
        foreach(Match match in Regex.Matches(lines[y], "(#|O)")) {
          Rocks = [..Rocks, new(match.Index, y, match.Value == "O")];
        }
      }
    }

    public void TiltInCycles(long count) {
      List<string> shorthandedPositions = [];
      bool cycleFound = false;

      for (long i = 0; i < count; i++) {
        for (int j = 0; j < 4; j++) {
          Tilt(j);
        }
        string shorhand = GetShorthand();
        if (!cycleFound) {
          if (shorthandedPositions.Contains(shorhand)) {
            long firstOccurence = shorthandedPositions.IndexOf(shorhand);
            long cycleLength = i - firstOccurence;
            i = count - ((count - firstOccurence) % cycleLength);
            cycleFound = true;
          } else {
            shorthandedPositions.Add(shorhand);
          }
        }
      }
    }


    public void Tilt(int direction) {
      SortRocks(direction);
      for (int i = 0; i < Rocks.Length; i++) {
        if (!Rocks[i].Rounded || AtTheEdge(Rocks[i], direction)) continue;

        bool moved = false;
        for (int j = i-1; j >= 0; j--) {
          if (Rocks[j].OnTheSameLine(Rocks[i], direction)) {
            Rocks[i].MoveUpTo(Rocks[j], direction);
            moved = true;
            break;
          }
        }

        if (!moved) MoveToEdge(Rocks[i], direction);
      }
    }

    bool AtTheEdge(Rock rock, int direction) {
      return direction switch {
        0 => rock.Y == 0,
        1 => rock.X == 0,
        2 => rock.Y == Height - 1,
        3 => rock.X == Width - 1,
        _ => false,
      };
    }

    void SortRocks(int direction) {
      switch (direction) {
        case 0: Rocks = Rocks.OrderBy(rock => rock.Y).ToArray();
          break;
        case 1: Rocks = Rocks.OrderBy(rock => rock.X).ToArray();
          break;
        case 2: Rocks = Rocks.OrderBy(rock => rock.Y).Reverse().ToArray();
          break;
        case 3: Rocks = Rocks.OrderBy(rock => rock.X).Reverse().ToArray();
          break;
      }
    }

    void MoveToEdge(Rock rock, int direction) {
      switch (direction) {
        case 0: rock.Y = 0;
          break;
        case 1: rock.X = 0;
          break;
        case 2: rock.Y = Height - 1;
          break;
        case 3: rock.X = Width - 1;
          break;
      }
    }

    public int GetLoad() {
      int sum = 0;

      foreach (Rock rock in Rocks) {
        if (rock.Rounded) sum += Height - rock.Y;
      }

      return sum;
    }

    public string GetShorthand() {
      return string.Join(';', Rocks.Where(rock => rock.Rounded).Select(rock => $"{rock.X}-{rock.Y}"));
    }
    public void Print() {
      string line = new('.', Width); 
      int offset = Console.GetCursorPosition().Top;
      for (int i = 0; i < Height; i++) {
        Console.WriteLine(line);
      }
      foreach (Rock rock in Rocks) {
        rock.Print(offset);
      }
      Console.WriteLine();
      Console.WriteLine("\n");
    }
  }

  class Rock (int x, int y, bool rounded) {
    public int X = x;
    public int Y = y;
    public readonly bool Rounded = rounded;

    public void Print(int offset) {
      Console.SetCursorPosition(X, Y + offset);
      if (Rounded) Console.Write("O");
      else Console.Write("#");
    }

    public void MoveToRow(int rowIndex) {
      Y = rowIndex;
    }

    public bool OnTheSameLine(Rock other, int direction) {
      return (direction % 2) switch {
        0 => X == other.X,
        1 => Y == other.Y,
        _ => false,
      };
    }

    public void MoveUpTo(Rock target, int direction) {
      switch (direction) {
        case 0: Y = target.Y + 1;
          break;
        case 1: X = target.X + 1;
          break;
        case 2: Y = target.Y - 1;
          break;
        case 3: X = target.X - 1;
          break;
      }
    }
  }
}