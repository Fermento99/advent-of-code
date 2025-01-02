package task6

import (
	"aoc-24/utils"
	"regexp"
)

type Point = utils.Point

func FirstStar(lines []string) int {
	visitedMap := createVisitedMap(len(lines), len(lines[0]))
	guardPos := findGuard(lines)
	guardDirection := 1
	distinctPositions := 1

	for {
		newStep := savePosition(visitedMap, guardPos, guardDirection)
		guardPos, guardDirection = nextGuardPosition(lines, guardPos, guardDirection)
		if newStep == -1 || guardDirection == -1 {
			break
		}

		distinctPositions += newStep
	}

	return distinctPositions
}

func createVisitedMap(height, width int) [][][]int {
	matrix := make([][][]int, height)
	for i := range height {
		row := make([][]int, width)
		for j := range width {
			row[j] = make([]int, 0, 4)
		}
		matrix[i] = row
	}

	return matrix
}

func findGuard(lines []string) Point {
	guardRegex, _ := regexp.Compile(`\^`)

	for y, line := range lines {
		x := guardRegex.FindStringIndex(line)
		if len(x) > 0 {
			return Point{X: x[0], Y: y}
		}
	}

	return Point{X: -1, Y: -1}
}

func nextGuardPosition(lines []string, guardPos Point, direction int) (Point, int) {
	switch direction {
	case 1:
		if guardPos.Y-1 < 0 {
			return Point{X: -1, Y: -1}, -1
		}

		if lines[guardPos.Y-1][guardPos.X] == '#' {
			return nextGuardPosition(lines, guardPos, 2)
		}

		guardPos.Y--
		return guardPos, 1
	case 2:
		if guardPos.X+1 >= len(lines[0]) {
			return Point{X: -1, Y: -1}, -1
		}

		if lines[guardPos.Y][guardPos.X+1] == '#' {
			return nextGuardPosition(lines, guardPos, 3)
		}

		guardPos.X++
		return guardPos, 2
	case 3:
		if guardPos.Y+1 >= len(lines) {
			return Point{X: -1, Y: -1}, -1
		}

		if lines[guardPos.Y+1][guardPos.X] == '#' {
			return nextGuardPosition(lines, guardPos, 4)
		}

		guardPos.Y++
		return guardPos, 3
	case 4:
		if guardPos.X-1 < 0 {
			return Point{X: -1, Y: -1}, -1
		}

		if lines[guardPos.Y][guardPos.X-1] == '#' {
			return nextGuardPosition(lines, guardPos, 1)
		}

		guardPos.X--
		return guardPos, 4
	}

	return Point{X: -1, Y: -1}, -1
}

func savePosition(visitedMap [][][]int, position Point, direction int) int {
	point := &visitedMap[position.Y][position.X]
	if len(*point) > 0 {
		if utils.ArrayContains(*point, direction) {
			return -1
		}

		*point = append(*point, direction)
		return 0
	}

	*point = append(*point, direction)
	return 1
}
