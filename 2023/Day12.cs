using System.Text.RegularExpressions;

namespace Day12 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("12");

      int arrangementSum = 0;
      foreach (string line in lines) {
        Record record = new(line);
        arrangementSum += record.PossibleArangements();
      }

      return arrangementSum;
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("12", true);
      
      int arrangementSum = 0;
      foreach (string line in lines) {
        Record record = new(line, true);
        arrangementSum += record.PossibleArangements();
      }

      return arrangementSum;
    }
  }

  class Record {
    readonly string Springs;
    readonly int[] DamagedGroups;
    readonly int DamagedCount;

    public Record(string line, bool folded=false) {
      string[] parts = line.Split(" ");
      if (folded) {
        Springs = String.Concat(Enumerable.Repeat(parts[0], 5));
        
        DamagedGroups = Enumerable.Repeat(parts[1].Split(",").Select(int.Parse).ToArray(), 5).SelectMany(x => x).ToArray();
      } else {
        Springs = parts[0];
        DamagedGroups = parts[1].Split(",").Select(int.Parse).ToArray();
      }
      
      
      DamagedCount = DamagedGroups.Sum();
    }

    public int PossibleArangements() {
      int sum = 0;
      foreach (string variant in PossibleStrings(Springs)) {
        // Console.WriteLine(variant);

        if (CheckVariant(variant)) {
          sum++;
          // Console.WriteLine($"{variant}    {string.Join(", ", GetVariantGroups(variant))} = {string.Join(", ", DamagedGroups)}");
        }
      }

      return sum;
    }

    private bool CheckVariant(string variant) {
      int[] variantDamagedGroups = GetVariantGroups(variant);
      if (!Regex.Match(variant, Springs.Replace(".", "\\.").Replace("?", "[#\\.]?")).Success) {
        return false;
      }

      if (variantDamagedGroups.Length != DamagedGroups.Length) {
        return false;
      }

      for (int i = 0; i < DamagedGroups.Length; i++){
        if (variantDamagedGroups[i] != DamagedGroups[i]) {
          return false;
        }
      }
      return true;
    }

    static int[] GetVariantGroups(string variant) {
      return Regex.Matches(variant, "(#+)").Select(match => match.Groups[1].Length).ToArray();
    }

    static int GetDamagedCount(string variant) {
      return Regex.Matches(variant, "(#+)").Select(match => match.Groups[1].Length).Sum();
    }

    static int GetWorkingCount (string variant) {
      return Regex.Matches(variant, "(\\.+)").Select(match => match.Groups[1].Length).Sum();
    }

    string[] PossibleStrings(string variant) {
      Match match = Regex.Match(variant, "\\?");
      if (match.Success) {
        char[] temp;
        char[][] output = [];
        int variantDamagedCount = GetDamagedCount(variant);

        if(GetWorkingCount(variant) < variant.Length - DamagedCount) {
          temp = variant.ToCharArray();
          temp[match.Index] = '.';
          output = [temp];
        }

        if (GetDamagedCount(variant) < DamagedCount) {
          temp = variant.ToCharArray();
          temp[match.Index] = '#';
          output = [..output, temp];
        }

        return output.SelectMany(charArray => PossibleStrings(new string (charArray))).ToArray();
      }
      
      return [variant];
    }
  }
}