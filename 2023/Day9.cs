namespace Day9 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("09");

      int sum = 0;
      foreach(string line in lines) {
        Sequence sequence = new(line);
        sum += sequence.NextInSequence();
      }

      return sum;
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("09");
      
      int sum = 0;
      foreach(string line in lines) {
        Sequence sequence = new(line);
        sum += sequence.PreviousInSequence();
      }

      return sum;
    }
  }

  class Sequence {
    private int[] OriginalValues;
    private int[][] HelperValues = [];

    public Sequence(string line) {
      OriginalValues = [..line.Split(" ").Select(int.Parse)];
    }

    public int NextInSequence() {
      HelperValues = [OriginalValues];
      int i = 0;
      do {
        int[] temp = [];
        for (int j = 0; j < HelperValues[i].Length - 1; j++) {
          temp = [..temp, HelperValues[i][j+1] - HelperValues[i][j]];
        }
        HelperValues = [..HelperValues, temp];
        i++;
      } while (HelperValues.Last().Any(value => value != 0));

      int output = 0;
      while (i >= 0) {
        output += HelperValues[i].Last();
        i--;
      }

      return output;
    }
  
    public int PreviousInSequence() {
      HelperValues = [OriginalValues];
      int i = 0;
      do {
        int[] temp = [];
        for (int j = 0; j < HelperValues[i].Length - 1; j++) {
          temp = [..temp, HelperValues[i][j+1] - HelperValues[i][j]];
        }
        HelperValues = [..HelperValues, temp];
        i++;
      } while (HelperValues.Last().Any(value => value != 0));

      int output = 0;
      while (i >= 0) {
        output = HelperValues[i].First() - output;
        i--;
      }

      return output;
    }
  }
}