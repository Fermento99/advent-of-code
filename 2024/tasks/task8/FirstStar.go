package task8

import (
	"aoc-24/utils"
	"regexp"
	"strings"
)

var anthenaRegexString = `[A-z0-9]`

type Anthena struct {
	X, Y      int
	Frequency byte
}

type Point struct {
	X, Y int
}

func FirstStar(lines []string) int {
	anthenas := getAnthenas(lines)
	distinctFreq := getDistinctFrequencies(anthenas)
	antinodes := calculateAntinodes(anthenas, distinctFreq, Point{len(lines[0]), len(lines)})

	return countDistinctAntinodes(antinodes)
}

func getAnthenas(lines []string) []Anthena {
	anthenaRegex, _ := regexp.Compile(anthenaRegexString)
	anthenas := make([]Anthena, getAnthenaCount(lines))
	index := 0

	for y, line := range lines {
		xCoords := anthenaRegex.FindAllStringIndex(line, -1)
		for _, x := range xCoords {
			anthenas[index] = Anthena{x[0], y, line[x[0]]}
			index++
		}
	}

	return anthenas
}

func getAnthenaCount(lines []string) int {
	anthenaRegex, _ := regexp.Compile(anthenaRegexString)
	oneLine := strings.Join(lines, "")

	return len(anthenaRegex.FindAllString(oneLine, -1))
}

func getDistinctFrequencies(anthenas []Anthena) []byte {
	frequencies := make([]byte, 0)

	for _, anthena := range anthenas {
		if !utils.ArrayContains(frequencies, anthena.Frequency) {
			frequencies = append(frequencies, anthena.Frequency)
		}
	}

	return frequencies
}

func calculateAntinodes(anthenas []Anthena, distinctFreq []byte, dimentions Point) []Point {
	antinodes := make([]Point, 0)

	for _, freq := range distinctFreq {
		sameFreqAnthenas := filterAnthenas(anthenas, freq)
		for i := range len(sameFreqAnthenas) - 1 {
			for j := i + 1; j < len(sameFreqAnthenas); j++ {
				newAntinodes := getAntinodes(sameFreqAnthenas[i], sameFreqAnthenas[j], dimentions)
				antinodes = append(antinodes, newAntinodes...)
			}
		}
	}

	return antinodes
}

func filterAnthenas(anthenas []Anthena, freq byte) []Anthena {
	filtered := make([]Anthena, 0, len(anthenas))

	for _, anthena := range anthenas {
		if anthena.Frequency == freq {
			filtered = append(filtered, anthena)
		}
	}

	return filtered
}

func getAntinodes(a, b Anthena, dimentions Point) []Point {
	antinodes := make([]Point, 0, 2)
	dx := a.X - b.X
	dy := a.Y - b.Y

	firstPoint := Point{a.X + dx, a.Y + dy}
	secondPoint := Point{b.X - dx, b.Y - dy}

	if isPointInDimentions(firstPoint, dimentions) {
		antinodes = append(antinodes, firstPoint)
	}
	if isPointInDimentions(secondPoint, dimentions) {
		antinodes = append(antinodes, secondPoint)
	}

	return antinodes
}

func isPointInDimentions(point, dimentions Point) bool {
	return point.X >= 0 && point.Y >= 0 && point.X < dimentions.X && point.Y < dimentions.Y
}

func countDistinctAntinodes(antinodes []Point) int {
	distinctAntinodes := make([]Point, 0, len(antinodes))

	for _, antinode := range antinodes {
		if !utils.ArrayContains(distinctAntinodes, antinode) {
			distinctAntinodes = append(distinctAntinodes, antinode)
		}
	}

	return len(distinctAntinodes)
}
