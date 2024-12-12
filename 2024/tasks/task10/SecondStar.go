package task10

import "aoc-24/utils"

func SecondStar(lines []string) int {
	height, width := len(lines), len(lines[0])
	dimentions := Point{width, height}
	trailMap := make([][]int, height)

	for y, line := range lines {
		trailMap[y] = utils.MapStringToNumbers(line, "")
	}

	sum := 0

	for y, row := range trailMap {
		for x, spot := range row {
			if spot == 0 {
				sum += getTrailheadRating(trailMap, Point{x, y}, dimentions)
			}
		}
	}

	return sum
}

func getTrailheadRating(trailMap [][]int, trailhead, dimentions Point) int {
	value := 0
	previousPoints := make([]Point, 1)
	previousPoints[0] = trailhead

	for value != 9 {
		value++
		nextPoints := make([]Point, 0, utils.Pow(4, value))

		for _, point := range previousPoints {
			nextPoints = append(nextPoints, getOneStepNeighbours(trailMap, point, value, dimentions)...)
		}

		if len(nextPoints) == 0 {
			return 0
		}

		previousPoints = make([]Point, len(nextPoints))
		copy(previousPoints, nextPoints)
	}

	return len(previousPoints)
}
