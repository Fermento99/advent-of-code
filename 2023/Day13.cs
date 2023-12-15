namespace Day13 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("13");
      
      Map map;
      int sum = 0;
      string[] mapRows = [];
      foreach (string line in lines) {
        if (line != ""){
          mapRows = [..mapRows, line];
        } else {
          map = new(mapRows);
          sum += map.GetMirrorCoeficient();
          mapRows = [];
        }
      }

      map = new(mapRows);
      sum += map.GetMirrorCoeficient();

      return sum;
    }
  }

  class Map(string[] lines) {
    private readonly string[] State = lines;
    private readonly int Width = lines.First().Length;
    private readonly int Height = lines.Length;

    public int GetMirrorCoeficient() {
      int rowSymmetry = CheckRowSymmetry();

      if (rowSymmetry != -1) {
        return (rowSymmetry + 1) * 100;
      }

      return CheckColumnSymmetry() + 1;
    }

    int CheckRowSymmetry() {
      for (int i = 0; i < Height - 1; i++) {
        if (ConfirmRowSymmetry(i)) {
          return i;
        }
      }
      return -1;
    }

    bool ConfirmRowSymmetry(int aboveRows) {
      for (int i = 1; aboveRows + i < Height && aboveRows - i + 1 >= 0; i++) {
        if (Row(aboveRows + i) != Row(aboveRows - i + 1)) {
          return false;
        }
      }
      return true;
    }

    int CheckColumnSymmetry() {
      for (int i = 0; i < Width - 1; i++) {
        if (ConfirmColumnSymmetry(i)) {
          return i;
        }
      }
      return -1;
    }
    
    bool ConfirmColumnSymmetry(int leftColumns) {
      for (int i = 1; leftColumns + i < Width && leftColumns - i + 1 >= 0; i++) {
        if (Column(leftColumns + i) != Column(leftColumns - i + 1)) {
          return false;
        }
      }
      return true;
    }

    string Row(int i) {
      return State[i];
    }

    string Column(int i) {
      char[] output = [];
      
      foreach(string row in State) {
        output = [..output, row[i]];
      }

      return new string (output);
    }
  }
}