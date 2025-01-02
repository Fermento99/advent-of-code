package task10

import (
	"aoc-24/utils"
)

type Point = utils.Point

func FirstStar(lines []string) int {
	height, width := len(lines), len(lines[0])
	dimentions := Point{X: width, Y: height}
	trailMap := make([][]int, height)

	for y, line := range lines {
		trailMap[y] = utils.MapStringToNumbers(line, "")
	}

	sum := 0

	for y, row := range trailMap {
		for x, spot := range row {
			if spot == 0 {
				sum += getTrailheadScore(trailMap, Point{X: x, Y: y}, dimentions)
			}
		}
	}

	return sum
}

func getTrailheadScore(trailMap [][]int, trailhead, dimentions Point) int {
	value := 0
	previousPoints := make([]Point, 1)
	previousPoints[0] = trailhead

	for value != 9 {
		value++
		nextPoints := make([]Point, 0, utils.Pow(4, value))

		for _, point := range previousPoints {
			newNeighbours := getOneStepNeighbours(trailMap, point, value, dimentions)
			for _, neighbour := range newNeighbours {
				if !utils.ArrayContains(nextPoints, neighbour) {
					nextPoints = append(nextPoints, neighbour)
				}
			}
		}

		if len(nextPoints) == 0 {
			return 0
		}

		previousPoints = make([]Point, len(nextPoints))
		copy(previousPoints, nextPoints)
	}

	return len(previousPoints)
}

func getOneStepNeighbours(trailMap [][]int, point Point, value int, dimentions Point) []Point {
	neighbours := make([]Point, 0, 4)
	x, y := point.X, point.Y
	maxX, maxY := dimentions.X-1, dimentions.Y-1

	if x > 0 && trailMap[y][x-1] == value {
		neighbours = append(neighbours, Point{X: x - 1, Y: y})
	}
	if y > 0 && trailMap[y-1][x] == value {
		neighbours = append(neighbours, Point{X: x, Y: y - 1})
	}
	if x < maxX && trailMap[y][x+1] == value {
		neighbours = append(neighbours, Point{X: x + 1, Y: y})
	}
	if y < maxY && trailMap[y+1][x] == value {
		neighbours = append(neighbours, Point{X: x, Y: y + 1})
	}

	return neighbours
}
