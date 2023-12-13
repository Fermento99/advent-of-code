namespace Day10 {
  public class Solution {
    public static int Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("10", true);

      Loop loop = new(new([..lines]));

      return loop.FindLargestDistance();
    }

    public static int Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("10");
      
      Loop loop = new(new([..lines]));

      return loop.PartitionPlane();
    }
  }

  class Loop(Map map) {
    private readonly Map MapState = map;
    private readonly Point StartingPoint = map.GetStartingPoint();
    public Point[] VisitedPoints = [];
    public Point[] OuterPlane = [];
    public Point[] InnerPlane = [];

    private bool Filter(Point point) => !VisitedPoints.Contains(point);

    public int FindLargestDistance() {
      int distance = 1;
      Point[] curr = StartingPoint.GetNeighbours(MapState);
      VisitedPoints = [StartingPoint];

      do {
        distance++;
        VisitedPoints = [..VisitedPoints, ..curr];
        curr = curr
          .SelectMany(point => point.GetNeighbours(MapState))
          .Where(point => !VisitedPoints.Contains(point))
          .Distinct()
          .ToArray();
      } while(curr.Length > 1);

      return distance;
    }

    public int PartitionPlane() {
      Point last = StartingPoint; 
      Point curr = StartingPoint.GetNeighbours(MapState).First();
      Point[] LeftPlane = [];
      Point[] RightPlane = [];

      do {
        (Point[], Point[], Point) partition = curr.GetNextSegment(MapState, last);
        LeftPlane = [..LeftPlane, ..partition.Item1];
        RightPlane = [..RightPlane, ..partition.Item2];
        VisitedPoints = [..VisitedPoints, curr];
        last = curr;
        curr = partition.Item3;
      } while (curr.Value != 'S');

      VisitedPoints = [..VisitedPoints, StartingPoint];

      LeftPlane = LeftPlane.Distinct().Where(Filter).ToArray();
      RightPlane = RightPlane.Distinct().Where(Filter).ToArray();

      LeftPlane = ExtendPlane(LeftPlane);
      RightPlane = ExtendPlane(RightPlane);

      if (RightPlane.Any(point => 
            point.X == 0 ||
            point.X == MapState.Width-1 ||
            point.Y == 0 ||
            point.Y == MapState.Height-1
        )) {
        OuterPlane = RightPlane;
        InnerPlane = LeftPlane;
      } else {
        OuterPlane = LeftPlane;
        InnerPlane = RightPlane;
      }

      return InnerPlane.Length;
    }

    private Point[] ExtendPlane(Point[] plane) {
      Point[] newPoints = [];

      foreach(Point point in plane) {
        newPoints = [..newPoints, ..point.GetPlaneNeighbours(MapState).Where(point => !plane.Contains(point)).Where(Filter)];
      }

      while (newPoints.Length > 0) {
        plane = [..plane, ..newPoints];
        Point[] temp = [];
        foreach(Point point in newPoints) {
          temp = [..temp, ..point.GetPlaneNeighbours(MapState).Where(point => !plane.Contains(point)).Where(Filter)];
        }
        newPoints = temp.Distinct().ToArray();
      }

      return plane.Distinct().ToArray();
    }



    public void PrintLoop() {
      for (int y = 0; y < MapState.Height; y++) {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write($"{(y+1).ToString().PadLeft(3, '0')} ");
        for (int x = 0; x < MapState.Width; x++) {
          Point point = new(x, y, MapState.GetPoint(x, y));

          if (point.Value == 'S') {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Write(point.Value);
            continue;
          }

          if (VisitedPoints.Contains(point)) {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(point.Value);
            continue;
          }

          if (OuterPlane.Contains(point)) {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(point.Value);
            continue;
          }

          if (InnerPlane.Contains(point)) {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(point.Value);
            continue;
          }
          
          Console.BackgroundColor = ConsoleColor.Black;
          Console.Write(point.Value);
        
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine();
      }
    }
  } 

  class Map(string[] lines) {
    private readonly string[] State = lines;
    public int Width = lines.First().Length;
    public int Height = lines.Length; 

    public char GetPoint(int x, int y) {
      try {
        return State[y][x];
      } catch(IndexOutOfRangeException) {
        return '.';
      }
      
    }

    public Point GetStartingPoint() {
      int x = -1, y;
      for (y = 0; y < State.Length; y++) {
        x = State[y].IndexOf('S');
        if (x != -1) {
          break;
        }
      }

      return new(x, y, 'S');
    }
  }

  class Point(int x, int y, char value) {
    public int X = x;
    public int Y = y;
    public char Value = value;

    public Point[] GetPlaneNeighbours(Map map) {
      Point[] output = [];
      if (X-1 >= 0) {
        output = [..output, new(X-1, Y, map.GetPoint(X-1, Y))];
        if (Y-1 >= 0) output = [..output, new(X-1, Y-1, map.GetPoint(X, Y-1))];
        if (Y+1 < map.Height) output = [..output, new(X-1, Y+1, map.GetPoint(X, Y+1))];
      }
      if (Y-1 >= 0) output = [..output, new(X, Y-1, map.GetPoint(X, Y-1))];
      if (Y+1 < map.Height) output = [..output, new(X, Y+1, map.GetPoint(X, Y+1))];
      if (X+1 < map.Width) {
        output = [..output, new(X+1, Y, map.GetPoint(X+1, Y))];
        if (Y-1 >= 0) output = [..output, new(X+1, Y-1, map.GetPoint(X, Y-1))];
        if (Y+1 < map.Height) output = [..output, new(X+1, Y+1, map.GetPoint(X, Y+1))];
      }

      return output;
    }

    public Point[] GetNeighbours(Map map) {
      return Value switch
      {
        '-' => [new(X-1, Y, map.GetPoint(X-1, Y)), new(X+1, Y, map.GetPoint(X+1, Y))],
        '|' => [new(X, Y-1, map.GetPoint(X, Y-1)), new(X, Y+1, map.GetPoint(X, Y+1))],
        'L' => [new(X, Y-1, map.GetPoint(X, Y-1)), new(X+1, Y, map.GetPoint(X+1, Y))],
        'J' => [new(X, Y-1, map.GetPoint(X, Y-1)), new(X-1, Y, map.GetPoint(X-1, Y))],
        'F' => [new(X+1, Y, map.GetPoint(X+1, Y)), new(X, Y+1, map.GetPoint(X, Y+1))],
        '7' => [new(X-1, Y, map.GetPoint(X-1, Y)), new(X, Y+1, map.GetPoint(X, Y+1))],
        'S' => GetStartingNeighbours(map),
        _ => [],
      };
    }

    public (Point[], Point[], Point) GetNextSegment(Map map, Point prevPoint) {
      int diffX = prevPoint.X - X;
      int diffY = prevPoint.Y - Y;

      return (diffX, diffY) switch {
        (-1, 0) => FromLeft(map),
        (1, 0) => FromRight(map),
        (0, -1) => FromTop(map),
        (0, 1) => FromBottom(map),
        _ => ([], [], new(-1, -1, '.')),
      };

    }

    private (Point[], Point[], Point) FromLeft(Map map) {
      return Value switch {
        '-' => (
          [new(X, Y-1, map.GetPoint(X, Y-1))],
          [new(X, Y+1, map.GetPoint(X, Y+1))],
          new(X+1, Y, map.GetPoint(X+1, Y))
        ),
        '7' => (
          [
            new(X+1, Y, map.GetPoint(X+1, Y)),
            new(X+1, Y-1, map.GetPoint(X-1, Y-1)),
            new(X, Y-1, map.GetPoint(X, Y-1)),
          ],
          [],
          new(X, Y+1, map.GetPoint(X, Y+1))
        ),
        'J' => (
          [],
          [
            new(X+1, Y, map.GetPoint(X+1, Y)),
            new(X+1, Y+1, map.GetPoint(X+1, Y+1)),
            new(X, Y+1, map.GetPoint(X, Y+1)),
          ],
          new(X, Y-1, map.GetPoint(X, Y-1))),
        _ => ([], [], new(-1, -1, '.')),
      };
    }
    private (Point[], Point[], Point) FromRight(Map map) {
      return Value switch {
        '-' => (
          [new(X, Y+1, map.GetPoint(X, Y+1))],
          [new(X, Y-1, map.GetPoint(X, Y-1))],
          new(X-1, Y, map.GetPoint(X-1, Y))
        ),
        'F' => (
          [],
          [
            new(X-1, Y, map.GetPoint(X-1, Y)),
            new(X-1, Y-1, map.GetPoint(X-1, Y-1)),
            new(X, Y-1, map.GetPoint(X, Y-1)),
          ],
          new(X, Y+1, map.GetPoint(X, Y+1))
        ),
        'L' => (
          [
            new(X-1, Y, map.GetPoint(X-1, Y)),
            new(X-1, Y+1, map.GetPoint(X-1, Y+1)),
            new(X, Y+1, map.GetPoint(X, Y+1)),
          ],
          [],
          new(X, Y-1, map.GetPoint(X, Y-1))
        ),
        _ => ([], [], new(-1, -1, '.')),
      };
    }
    private (Point[], Point[], Point) FromTop(Map map) {
      return Value switch {
        '|' => (
          [new(X+1, Y, map.GetPoint(X+1, Y))],
          [new(X-1, Y, map.GetPoint(X-1, Y))],
          new(X, Y+1, map.GetPoint(X, Y+1))
        ),
        'L' => (
          [], 
          [
            new(X-1, Y, map.GetPoint(X-1, Y)),
            new(X-1, Y+1, map.GetPoint(X-1, Y+1)),
            new(X, Y+1, map.GetPoint(X, Y+1)),
          ],
          new(X+1, Y, map.GetPoint(X+1, Y))
        ),
        'J' => (
          [
            new(X+1, Y, map.GetPoint(X+1, Y)),
            new(X+1, Y+1, map.GetPoint(X+1, Y+1)),
            new(X, Y+1, map.GetPoint(X, Y+1)),
          ],
          [],
          new(X-1, Y, map.GetPoint(X-1, Y))
        ),
        _ => ([], [], new(-1, -1, '.')),
      };
    }
    private (Point[], Point[], Point) FromBottom(Map map) {
      return Value switch {
        '|' => (
          [new(X-1, Y, map.GetPoint(X-1, Y))],
          [new(X+1, Y, map.GetPoint(X+1, Y))],
          new(X, Y-1, map.GetPoint(X, Y-1))
        ),
        'F' => (
          [
            new(X-1, Y, map.GetPoint(X-1, Y)),
            new(X-1, Y-1, map.GetPoint(X-1, Y-1)),
            new(X, Y-1, map.GetPoint(X, Y-1)),
          ],
          [],
          new(X+1, Y, map.GetPoint(X+1, Y))
        ),
        '7' => (
          [],
          [
            new(X+1, Y, map.GetPoint(X+1, Y)),
            new(X+1, Y-1, map.GetPoint(X+1, Y-1)),
            new(X, Y-1, map.GetPoint(X, Y-1)),
          ],
          new(X-1, Y, map.GetPoint(X-1, Y))
        ),
        _ => ([], [], new(-1, -1, '.')),
      };
    }

    private Point[] GetStartingNeighbours(Map map) {
      Point[] potentialNeighbours = [
        new(X-1, Y, map.GetPoint(X-1, Y)),
        new(X+1, Y, map.GetPoint(X+1, Y)),
        new(X, Y-1, map.GetPoint(X, Y-1)),
        new(X, Y+1, map.GetPoint(X, Y+1)),
      ];

      Point[] output = [];

      foreach(Point point in potentialNeighbours) {
        if (point.GetNeighbours(map).Contains(this)) {
          output = [..output, point]; 
        }
      }

      return output;
    }

    public override bool Equals(object? obj) {
      if (obj == null || GetType() != obj.GetType()) {
        return false;
      }

      Point other = (Point) obj;

      return other.X == X && other.Y == Y;
    }

    public override int GetHashCode() {
      return ((X + Y) * (X + Y + 1) / 2) + X;
    }
  }
}