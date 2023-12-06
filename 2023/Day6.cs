using System.ComponentModel.DataAnnotations;
using Day4;

namespace Day6 {
  public class Solution {
    public static long Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("06");

      int[] times = lines.ElementAt(0).Replace("Time:", "").Split(" ").Where(element => element.Length > 0).Select(int.Parse).ToArray();
      int[] records = lines.ElementAt(1).Replace("Distance:", "").Split(" ").Where(element => element.Length > 0).Select(int.Parse).ToArray();

      long allWaysToBeat = 1;
      for (int i = 0; i < times.Length; i++) {
        Race race = new(times[i], records[i]);
        allWaysToBeat *= race.waysToBeat;
      }

      return allWaysToBeat;
    }

    public static long Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("06");
      
      long time = long.Parse(lines.ElementAt(0).Replace("Time:", "").Replace(" ", ""));
      long record = long.Parse(lines.ElementAt(1).Replace("Distance:", "").Replace(" ", ""));

      Race race = new(time, record);

      return race.waysToBeat;
    }
  }

  class Race {
    public long waysToBeat = 0;

    public Race (long time, long reckord) {
      long first = 1;
      while ((time - first) * first <= reckord) {
        first++;
      }

      long last = time - 1;
      while ((time - last) * last <= reckord) {
        last--;
      }

      waysToBeat = last - first + 1;
    }
  }
}