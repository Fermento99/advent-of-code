using System.Security.Cryptography;

namespace Day16 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("16");

      Map map = new(lines.ToArray());

      return map.RunBeam(new(new(-1, 0), 0));
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("16");

      Map map = new(lines.ToArray());
      
      return map.RunAllBeams();
    }
  }

  class Map(string[] lines) {
    private string[] State = lines;
    public readonly int Width = lines.First().Length;
    public readonly int Height = lines.Length;

    public char GetPoint(Point point) {
      return State[point.Y][point.X];
    }

    public int RunAllBeams() {
      int maxScore = 0;
      int temp;

      for (int i = 0; i < Width; i++) {
        temp = RunBeam(new(new(i, -1), 1));
        if (temp > maxScore) maxScore = temp;
        temp = RunBeam(new(new(i, Height), 3));
        if (temp > maxScore) maxScore = temp;
      }

      for (int i = 0; i < Height; i++) {
        temp = RunBeam(new(new(-1, i), 0));
        if (temp > maxScore) maxScore = temp;
        temp = RunBeam(new(new(Width, i), 2));
        if (temp > maxScore) maxScore = temp;
      }

      return maxScore;
    }

    public int RunBeam(Beam beam) {
      Beam[] beams = [beam];
      HashSet<Point> energized = [];

      for (int i = 0; beams.Length > i; i++) {
        Beam current = beams[i];
        Beam[] newBeams = current.Move(this);
        foreach (Beam newBeam in newBeams) {
          if (beams.Where(beam => newBeam.StartingPosition.X == beam.StartingPosition.X &&
              newBeam.StartingPosition.Y == beam.StartingPosition.Y &&
              beam.Direction == newBeam.Direction).Count() == 0) {
            beams = [..beams, newBeam];
          };
        }
        foreach(Point point in current.EnergizedPoints) {
          energized.Add(point);
        }
      }

      return energized.Count - 1;
    }
  }

  class Beam(Point position, int direction) {
    public readonly Point StartingPosition = position;
    Point Position = position;
    public Point[] EnergizedPoints = [position];
    public readonly int Direction = direction;

    public Beam[] Move(Map map) {
      Point? nextPosition = NextPoint(map);
      if (nextPosition == null) return [];

      Position = (Point) nextPosition;
      EnergizedPoints = [..EnergizedPoints, Position];
      char value = map.GetPoint(Position);
      Beam[] nextBeams = CheckNextValue(value);

      if (nextBeams.Length == 0) return Move(map);
      return nextBeams;
    }

    Point? NextPoint(Map map) {
      if (Direction == 0 && Position.X < map.Width - 1) return new(Position.X+1, Position.Y);
      if (Direction == 1 && Position.Y < map.Height - 1) return new(Position.X, Position.Y+1);
      if (Direction == 2 && Position.X > 0) return new(Position.X-1, Position.Y);
      if (Direction == 3 && Position.Y > 0) return new(Position.X, Position.Y-1);
      return null;
    }

    Beam[] CheckNextValue(char value) {
      return value switch {
        '.' => [],
        '/' => ForwardMirror(),
        '\\' => BackwardMirror(),
        '-' => HorizontalSpliter(),
        '|' => VerticalSpliter(),
        _ => [],
      };
    }

    Beam[] ForwardMirror() {
      return Direction switch {
        0 => [new(Position, 3)],
        1 => [new(Position, 2)],
        2 => [new(Position, 1)],
        3 => [new(Position, 0)],
        _ => [],
      };
    }

    Beam[] BackwardMirror() {
      return Direction switch {
        0 => [new(Position, 1)],
        1 => [new(Position, 0)],
        2 => [new(Position, 3)],
        3 => [new(Position, 2)],
        _ => [],
      };
    }

    Beam[] VerticalSpliter() {
      if (Direction % 2 == 0) return [new(Position, 1), new(Position, 3)];
      return [];
    }

    Beam[] HorizontalSpliter() {
      if (Direction % 2 == 1) return [new(Position, 0), new(Position, 2)];
      return [];
    }
  }

  struct Point(int x, int y) {
    public int X = x;
    public int Y = y;
  }
}