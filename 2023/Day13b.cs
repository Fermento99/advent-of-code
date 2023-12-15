using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace Day13b {
  public class Solution {
    public static int Task2() {
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
      int rowSymmetry = CheckRowSymmetry() + 1;
      int columnSymmetry = CheckColumnSymmetry() + 1;

      if (rowSymmetry != 0) {
        return rowSymmetry * 100;
      }

      return columnSymmetry;
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
      bool smudge = false;

      for (int i = 1; aboveRows + i < Height && aboveRows - i + 1 >= 0; i++) {
        int diff = StringXor(Row(aboveRows + i), Row(aboveRows - i + 1));

        if (diff == 0) continue;

        if (diff == 2) return false;

        if (smudge) return false;

        smudge = true;
      }

      return smudge;
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
      bool smudge = false;

      for (int i = 1; leftColumns + i < Width && leftColumns - i + 1 >= 0; i++) {
        int diff = StringXor(Column(leftColumns + i), Column(leftColumns - i + 1));

        if (diff == 0) continue;

        if (diff == 2) return false;

        if (smudge) return false;

        smudge = true;
      }

      return smudge;
    }

    static int StringXor(string a, string b) {
      int diff = 0;

      for (int i = 0; i < a.Length; i++) {
        if (a[i] != b[i]) {
          diff++;
          if (diff > 1) return 2;
        }
      }

      return diff;
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