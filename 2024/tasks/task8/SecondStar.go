package task8

import "aoc-24/utils"

func SecondStar(lines []string) int {
	anthenas := getAnthenas(lines)
	distinctFreq := getDistinctFrequencies(anthenas)
	antinodes := calculateHarmonicAntinodes(anthenas, distinctFreq, Point{X: len(lines[0]), Y: len(lines)})

	return countDistinctAntinodes(antinodes)
}

func calculateHarmonicAntinodes(anthenas []Anthena, distinctFreq []byte, dimentions Point) []Point {
	antinodes := make([]Point, 0)

	for _, freq := range distinctFreq {
		sameFreqAnthenas := filterAnthenas(anthenas, freq)
		for i := range len(sameFreqAnthenas) - 1 {
			for j := i + 1; j < len(sameFreqAnthenas); j++ {
				newAntinodes := getHarmonicAntinodes(sameFreqAnthenas[i], sameFreqAnthenas[j], dimentions)
				antinodes = append(antinodes, newAntinodes...)
			}
		}
	}

	return antinodes
}

func getHarmonicAntinodes(a, b Anthena, dimentions Point) []Point {
	dx := a.X - b.X
	dy := a.Y - b.Y
	antinodes := make([]Point, 0, min(utils.Abs(dimentions.X/dx), utils.Abs(dimentions.Y/dy)))

	for i := 1; ; i++ {
		point := Point{X: a.X + dx*i, Y: a.Y + dy*i}
		if isPointInDimentions(point, dimentions) {
			antinodes = append(antinodes, point)
		} else {
			break
		}
	}

	for i := 0; ; i-- {
		point := Point{X: a.X + dx*i, Y: a.Y + dy*i}
		if isPointInDimentions(point, dimentions) {
			antinodes = append(antinodes, point)
		} else {
			break
		}
	}

	return antinodes
}
