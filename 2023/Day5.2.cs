using System.Text.RegularExpressions;

namespace Day5b {
  public class Solution {
    public static long Task1() {
      return 0;
    }

    public static long Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("05");

      Pipeline pipeline = new(lines);
      pipeline.RunAllSeeds();
            
      return pipeline.MinSeed();
    }
  }

  class Pipeline {
    public SeedRange[] seedRanges = [];
    private Map? OpenMap;
    public Map[] maps = [];

    public Pipeline (IEnumerable<string> configLines) {
      foreach (string line in configLines) {
        AnalyseLine(line);
      }
      if (OpenMap != null) {
        maps = [.. maps, OpenMap];
      }
    }

    private void AnalyseLine(string line) {
      if (line == "") {
        if (OpenMap != null) {
          maps = [.. maps, OpenMap];
        }
        return;
      }

      Group seedGroup = Regex.Match(line, "seeds: ((\\d+\\s?)+)").Groups[1];
      if (seedGroup.Length > 0) {
        long[] seeds = seedGroup.ToString().Split(" ").Select(long.Parse).ToArray();
        for (int i = 0; i < seeds.Length; i += 2) {
          seedRanges = [.. seedRanges, new(seeds[i], seeds[i] + seeds[i + 1] - 1)];
        }
        return;
      }

      GroupCollection mapGroups = Regex.Match(line, "(\\w+)-to-(\\w+) map:").Groups;
      if (mapGroups[1].Length > 0 && mapGroups[2].Length > 0) {
        OpenMap = new Map(mapGroups[1].Value, mapGroups[2].Value);
        return;
      }

      GroupCollection mappingGroups = Regex.Match(line, "(\\d+) (\\d+) (\\d+)").Groups;
      if (mappingGroups[1].Length > 0 && mappingGroups[2].Length > 0 && mappingGroups[3].Length > 0) {
        OpenMap?.AddMapping(long.Parse(mappingGroups[1].Value), long.Parse(mappingGroups[2].Value), long.Parse(mappingGroups[3].Value));
        return;
      }
    }

    public void RunAllSeeds() {
      foreach (Map map in maps) {
        SeedRange[] temp = [];
        foreach (SeedRange seedRange in seedRanges) {
          temp = [.. temp, ..map.MapElement(seedRange)];
        }
        seedRanges = temp;
      }
    }

    public long MinSeed() {
      long min = long.MaxValue;
      foreach(SeedRange seedRange in seedRanges) {
        if (min > seedRange.StartIndex) {
          min = seedRange.StartIndex;
        }
      }

      return min;
    }
  }

  class Map(string sourceName, string destinationName) {
    public string sourceName = sourceName;
    public string destinationName = destinationName;
    public Mapping[] mappings = [];

    public void AddMapping(long destStartIndex, long srcStartIndex, long length) {
      mappings = [.. mappings, new Mapping(destStartIndex, srcStartIndex, length)];
    }

    public SeedRange[] MapElement(SeedRange src) {
      foreach (Mapping mapping in mappings) {
        if (src.LastIndex < mapping.startIndex || src.StartIndex > mapping.endIndex) {
          continue;
        }

        if (src.StartIndex >= mapping.startIndex && src.LastIndex <= mapping.endIndex) {
          return [src.Move(mapping.modifier)];
        }

        if (src.StartIndex >= mapping.startIndex && src.LastIndex > mapping.endIndex) {
          SeedRange[] partitions = src.Partition(mapping.endIndex);
          return [partitions[0].Move(mapping.modifier), ..MapElement(partitions[1])];
        }

        if (src.StartIndex < mapping.startIndex && src.LastIndex <= mapping.endIndex) {
          SeedRange[] partitions = src.Partition(mapping.startIndex - 1);
          return [..MapElement(partitions[0]), partitions[1].Move(mapping.modifier)];
        }

        if (src.StartIndex < mapping.startIndex && src.LastIndex > mapping.endIndex) {
          SeedRange[] partitions = src.Partition(mapping.startIndex - 1, mapping.endIndex);
          return [..MapElement(partitions[0]), partitions[1].Move(mapping.modifier), ..MapElement(partitions[2])];
        }
      }
      return [src];
    }
  }



  class SeedRange(long startIndex, long lastIndex) {
    public long StartIndex = startIndex;
    public long LastIndex = lastIndex;

    public SeedRange[] Partition(long pivot) {
      return [
        new SeedRange(StartIndex, pivot),
        new SeedRange(pivot + 1, LastIndex),
      ];
    }

    public SeedRange[] Partition(long pivot1, long pivot2) {
      return [
        new SeedRange(StartIndex, pivot1),
        new SeedRange(pivot1 + 1, pivot2),
        new SeedRange(pivot2 + 1, LastIndex),
      ];
    }

    public SeedRange Move(long modifier) {
      StartIndex += modifier;
      LastIndex += modifier;
      return this;
    }

    public void Print() {
      Console.WriteLine($"({StartIndex}, {LastIndex})");
    }
  }

  struct Mapping (long destStartIndex, long srcStartIndex, long length) {
    public long startIndex = srcStartIndex, endIndex = srcStartIndex + length, modifier = destStartIndex - srcStartIndex;
  }
}