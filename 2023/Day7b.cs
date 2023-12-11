namespace Day7b {
  public class Solution {
    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("07");
      

      HandList hands = new();
      foreach(string line in lines) {
        hands.AddHand(new(line));
      }

      return hands.CalculateValue();
    }
  }

  class HandList {
    Hand[] Hands = [];

    public void AddHand(Hand hand) {
      Hands = [..Hands, hand];
    }

    Hand[] SortHands() {
      List<Hand> handsList = [.. Hands];
      handsList.Sort((a, b) => a.Compare(b));
      return [.. handsList];
    }

    public int CalculateValue() {
      var sorted = SortHands();
      int length = sorted.Length;
      int value = 0;
      for(int i = 0; i < length; i ++) {
        value += sorted[i].bid * (length - i);
      }

      return value;
    }
  }

  class Hand {
    public string cards;
    public int bid;
    public int rank;
    
    public Hand(string configLine) {
      string[] parts = configLine.Split(" ");
      cards = parts[0];
      bid = int.Parse(parts[1]);
      rank = DefineRank();
    }

    private int DefineRank() {
      CardCounter cardCounter = new();
      foreach (char value in cards) {
        cardCounter.AddCard(value);
      }
      
      int[] cardCounts = cardCounter.GetCardCounts();

      if (cardCounts.Length == 1) {
        return 6;
      }

      if (cardCounts.Length == 2) {
        if (cardCounts.Contains(4)) {
          return 5;
        } 

        return 4;
      }

      if (cardCounts.Length == 3) { 
        if (cardCounts.Contains(3)) {
          return 3;
        }

        return 2;
      }

      if (cardCounts.Length == 4) {
        return 1;
      }
      
      return 0;
    }

    public int Compare(Hand other) {
      int rankDiff = other.rank - rank;
      if (rankDiff != 0) {
        return rankDiff;
      }

      for(int i = 0; i < 5; i++) {
        int chardiff = CardCounter.TranslateCardToValue(other.cards[i]) 
          - CardCounter.TranslateCardToValue(cards[i]);
        if (chardiff != 0) {
          return chardiff;
        }
      }

      return 0;
    }
  }

  class CardCounter {
    private int[] CardValues = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

    public void AddCard(char card) {
      CardValues[TranslateCardToValue(card)]++;
    }

    public static int TranslateCardToValue(char card) {
      return card switch {
        'J' => 0,
        '2' => 1,
        '3' => 2,
        '4' => 3,
        '5' => 4,
        '6' => 5,
        '7' => 6,
        '8' => 7,
        '9' => 8,
        'T' => 9,
        'Q' => 10,
        'K' => 11,
        'A' => 12,
        _ => -1,
      };
    }

    public int[] GetCardCounts() {
      int[] output = [];
      int jokers = CardValues[0];
      if (jokers == 5) {
        return [5];
      }

      for (int i = 1; i < 13; i++) {
        if (CardValues[i] > 0) {
          output = [..output, CardValues[i]];
        }
      }
      
      output = [..output.Order().Reverse()];
      output[0] += jokers;

      return [..output];
    }
  }
}