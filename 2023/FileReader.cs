using System.IO;

class FileReader {
  public static IEnumerable<string> ReadFile(string inputNumber, bool known=false) {
    string prefix = known ? "known_" : "";
    return File.ReadLines($"./{prefix}inputs/{inputNumber}.txt");
  }
}