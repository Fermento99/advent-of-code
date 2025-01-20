package task15

import "regexp"

func SecondStar(lines []string) int {
	warehouseMap, robotInstructions, robotPos := parseWideInput(lines)
	executeRobotMoves(warehouseMap, robotInstructions, robotPos, true)

	// printMap(warehouseMap)
	return calculateGPSSum(warehouseMap)
}

func parseWideInput(lines []string) ([][]byte, []string, Point) {
	robotRegexp := regexp.MustCompile("@")
	robotPos := Point{}

	for i, line := range lines {
		x := robotRegexp.FindStringIndex(line)

		if x != nil {
			robotPos = Point{X: x[0] * 2, Y: i}
		}

		if lines[i] == "" {
			return parseWideWarehouseMap(lines[:i]), lines[i+1:], robotPos
		}
	}

	return [][]byte{}, []string{}, Point{}
}

func parseWideWarehouseMap(lines []string) [][]byte {
	res := make([][]byte, len(lines))

	for y, line := range lines {
		row := make([]byte, len(line)*2)

		for x, char := range line {
			switch char {
			case '.':
				row[x*2] = '.'
				row[x*2+1] = '.'
			case 'O':
				row[x*2] = '['
				row[x*2+1] = ']'
			case '#':
				row[x*2] = '#'
				row[x*2+1] = '#'
			case '@':
				row[x*2] = '@'
				row[x*2+1] = '.'
			}
		}

		res[y] = row
	}

	return res
}

func executeWideMove(warehouseMap [][]byte, instruction rune, robot Point) Point {
	return Point{}
}
