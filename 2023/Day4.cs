namespace Day4 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("04");

      Card[] cards = lines.Select(line => new Card(line)).ToArray();
      int pointSum = cards.Sum(card => card.Points());

      return pointSum;
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("04");

      Card[] cards = lines.Select(line => new Card(line)).ToArray();
      int[] counts = lines.Select(line => 1).ToArray();

      for (int i = 0; i < cards.Length; i++) {
        foreach(int wonCard in cards[i].WonScratchCards()) {
          counts[wonCard - 1] += counts[i];
        }
      }
      
      return counts.Sum();
    }
  }

  class Card {
    public int id;
    public int[] givenNumbers;
    public int[] winningNumbers;
    public int matches = 0;

    public Card(string line) {
      string[] configLine = line.Split(":");
      this.id = int.Parse(configLine[0].Replace("Card", "").Trim());
      string[] numberLists = configLine[1].Split("|");
      
      this.givenNumbers = numberLists[1].Split(" ").Where(number => number.Length > 0).Select(int.Parse).ToArray();
      this.winningNumbers = numberLists[0].Split(" ").Where(number => number.Length > 0).Select(int.Parse).ToArray();


      foreach (int number in givenNumbers) {
        if (winningNumbers.Contains(number)) {
          this.matches++;
        }
      }
    }

    public int Points() {
      if (this.matches > 0) return (int) Math.Pow(2, matches - 1);
      return 0;
    }

    public int[] WonScratchCards() {
      int[] output = [];
      for (int i = 1; i <= this.matches; i++) {
        output = [.. output, this.id + i];
      }
      return output;
    }
  }

}