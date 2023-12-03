using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Day2 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("02");

      int idSum = 0;
      foreach (string line in lines) {
        Game game = new Game(line);
        if (game.colors[0] <= 12 && game.colors[1] <= 13 && game.colors[2] <= 14) {
          idSum += game.id;
        }
      }

      return idSum;
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("02");

      int powerSum = 0;
      foreach (string line in lines) {
        Game game = new Game(line);
        int gamePower = 1;
        foreach (int color in game.colors) {
          gamePower *= color;
        }
        powerSum += gamePower;
      }

      return powerSum;
    }
  }

  class Game {
    public int id = 0;
    public int[] colors = [0, 0, 0];

    public Game (string configLine) {
      string[] line = configLine.Split(':');
      this.id = int.Parse(line[0].Replace("Game ", ""));
      string[] sets = line[1].Split(";");
      foreach(string set in sets) {
        analyseSet(set);
      }
    }

    public void analyseSet(string set) {
      string[] colors = ["", "", ""]; 
      colors[0] = Regex.Match(set, "(\\d+) red").Groups[1].ToString();
      colors[1] = Regex.Match(set, "(\\d+) green").Groups[1].ToString();
      colors[2] = Regex.Match(set, "(\\d+) blue").Groups[1].ToString();
      for (int i = 0; i < 3; i++) {
        if (colors[i] != "") {
          int colorVal = int.Parse(colors[i]);
          if (colorVal > this.colors[i]) {
            this.colors[i] = colorVal;
          }
        }
      }
    }
  }
}