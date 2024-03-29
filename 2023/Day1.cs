using System.Text.RegularExpressions;

namespace Day1 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> calibrationLines = FileReader.ReadFile("01");

      int sum = 0;
      foreach (string line in calibrationLines) {
        string calibrationDigits = Regex.Replace(line, "[a-z]", "");
        int calibrationValue = int.Parse(calibrationDigits.Last().ToString()) + 10 * int.Parse(calibrationDigits[0].ToString());
        sum += calibrationValue;
      }

      return sum;
    }

    public static int Task2() {
      IEnumerable<string> calibrationLines = FileReader.ReadFile("01");

      int sum = 0;
      foreach (string line in calibrationLines) {
        var calibrationDigit1 = Regex.Match(line, "^.*?(one|two|three|four|five|six|seven|eight|nine|[1-9]).*$").Groups[1].ToString();
        var calibrationDigit2 = Regex.Match(line, ".*(one|two|three|four|five|six|seven|eight|nine|[1-9]).*?$").Groups[1].ToString();

        int calibrationValue = ParseInt(calibrationDigit1) * 10 + ParseInt(calibrationDigit2);
        sum += calibrationValue;
      }

      return sum;
    }

    public static int ParseInt(string str) {
      switch(str) {
        case "one": return 1;
        case "two": return 2;
        case "three": return 3;
        case "four": return 4;
        case "five": return 5;
        case "six": return 6;
        case "seven": return 7;
        case "eight": return 8;
        case "nine": return 9;
        default: return int.Parse(str);
      }
    } 
  }
}
