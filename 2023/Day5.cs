using System.Text.RegularExpressions;

namespace Day5 {
  public class Solution {
    public static long Task1() {
      IEnumerable<string> lines = FileReader.ReadFile("05");

      Pipeline pipeline = new(lines);
      long[] positions = pipeline.RunAllSeeds();

      return positions.Min();
    }

    public static long Task2() {
      IEnumerable<string> lines = FileReader.ReadFile("05");
      
      return 0;
    }
  }

  class Pipeline {
    public long[] seeds = [];
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

    public void AnalyseLine(string line) {
      if (line == "") {
        if (OpenMap != null) {
          maps = [.. maps, OpenMap];
        }
        return;
      }

      Group seedGroup = Regex.Match(line, "seeds: ((\\d+\\s?)+)").Groups[1];
      if (seedGroup.Length > 0) {
        seeds = seedGroup.ToString().Split(" ").Select(long.Parse).ToArray();
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

    public long RunThroughSeed(long seed) {
      MappedValue typedSeed = new MappedValue(seed, "seed");
      while (typedSeed.type != "location") {
        Map map = maps.Where(map => map.sourceName == typedSeed.type).First();
        typedSeed = map.MapElement(typedSeed);
      }
      return typedSeed.value;
    }

    public long[] RunAllSeeds() {
      return seeds.Select(RunThroughSeed).ToArray();
    }
  }

  class Map {
    public string sourceName;
    public string destinationName;
    public Mapping[] mappings = [];
    
    public Map (string sourceName, string destinationName) {
      this.sourceName = sourceName;
      this.destinationName = destinationName;
    }

    public void AddMapping(long destStartIndex, long srcStartIndex, long length) {
      mappings = [.. mappings, new Mapping(destStartIndex, srcStartIndex, length)];
    }

    public MappedValue MapElement(MappedValue src) {
      foreach (Mapping mapping in mappings) {
        if (src.value >= mapping.startIndex && src.value < mapping.endIndex) {
          return new MappedValue(mapping.modifier + src.value, destinationName);
        }
      }
      return new MappedValue(src.value, destinationName);
    }
  }

  struct Mapping (long destStartIndex, long srcStartIndex, long length) {
    public long startIndex = srcStartIndex, endIndex = srcStartIndex + length, modifier = destStartIndex - srcStartIndex;
  }

  struct MappedValue (long value, string type) {
    public long value = value;
    public string type = type;
  }
}