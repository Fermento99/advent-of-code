namespace Day11 {
  public class Solution {
    public static long Task1() {
      string[] lines = FileReader.ReadFile("11").ToArray();

      Map map = new(lines);

      return map.GetDistanceSum();
    }

    public static long Task2() {
      string[] lines = FileReader.ReadFile("11").ToArray();
      
      Map map = new(lines, 999999);

      return map.GetDistanceSum();
    }
  }

  class Map {
    private Galaxy[] Galaxies = [];
    private readonly string[] State;
    private readonly int Width;
    private readonly int Height;
    private readonly int Age;

    public Map(string[] lines, int age=1) {
      State = lines;
      Height = lines.Length;
      Width = lines.First().Length;
      Age = age;
      InitializeGalaxies();
      AdjustGalaxies();
    }

    private void InitializeGalaxies() {
      for (int y = 0; y < Height; y++) {
        for (int x = 0; x < Width; x++) {
          if (State[y][x] == '#') {
            Galaxies = [..Galaxies, new(x, y)];
          } 
        }
      }
    }

    private void AdjustGalaxies() {
      int[] emptyColumns = EmptyColumns();
      int[] emptyRows = EmptyRows();

      foreach(Galaxy galaxy in Galaxies) {
        int xDiff = emptyColumns.Count(column => column < galaxy.X);
        int yDiff = emptyRows.Count(row => row < galaxy.Y);

        galaxy.AdjustToGravityEffects(Age * xDiff, Age * yDiff);
      }
    }

    private int[] EmptyRows() {
      int[] output = [];
      for (int i = 0; i < Height; i++) {
        if (!State[i].Contains('#')) {
          output = [..output, i];
        }
      }
      return output;
    }

    private int[] EmptyColumns() {
      int[] output = [];

      for (int i = 0; i < Width; i++) {
        bool empty = true;
        for (int j = 0; j < Height; j++) {
          empty = State[j][i] != '#';
          if(!empty) break;
        }
        if (empty) output = [..output, i];
      }

      return output;
    }

    public long GetDistanceSum() {
      long sum = 0;

      for (int i = 0; i < Galaxies.Length - 1; i++) {
        for (int j = i + 1; j < Galaxies.Length; j++) {
          sum += Galaxies[i].DistanceTo(Galaxies[j]);
        }
      }

      return sum;
    }

    public void Draw() {
      Console.Clear();
      int[] emptyColumns = EmptyColumns();
      int[] emptyRows = EmptyRows();
      
      int height = Height + emptyRows.Length * Age;
      int width = Width + emptyColumns.Length * Age;
      for (int i = 0; i < emptyRows.Length; i++) {
        for (int j = 0; j <= Age; j++) {
          Console.SetCursorPosition(0, emptyRows[i] + i*Age + j);
          Console.Write(new string('.', width));
        }
      }

      for (int i = 0; i < emptyColumns.Length; i++) {
        for (int j = 0; j <= Age; j++) {
          for (int k = 0; k < height; k++) {
            Console.SetCursorPosition(emptyColumns[i] + i*Age + j, k);
            Console.Write(".");
          }
        }
      }
      
      foreach (Galaxy galaxy in Galaxies) {
        Console.SetCursorPosition((int) galaxy.X, (int) galaxy.Y);
        Console.Write("#");
      }
      Console.WriteLine();
      Console.WriteLine();
    }
  }

  class Galaxy (long X, long Y) {
    public long X = X;
    public long Y = Y;

    public long DistanceTo(Galaxy other) {
      return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public void AdjustToGravityEffects(long xDiff, long yDiff) {
      X += xDiff;
      Y += yDiff;
    }
  }
}