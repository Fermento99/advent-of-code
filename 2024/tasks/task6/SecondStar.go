package task6

func SecondStar(lines []string) int {
	height, width := len(lines), len(lines[0])
	visitedMap := createVisitedMap(height, width)
	obstructionMap := createObstructionMap(height, width)
	firstPos := findGuard(lines)
	guardPos := firstPos
	guardDirection := 1

	savePosition(visitedMap, guardPos, guardDirection)

	possibleLoops := 0
	for {
		nextGuardPos, nextGuardPosition := nextGuardPosition(lines, guardPos, guardDirection)

		if nextGuardPosition == -1 {
			break
		}

		if !obstructionTested(obstructionMap, nextGuardPos) {
			aleternativeMap := createAlternativeMap(lines, nextGuardPos, height)
			possibleLoops += testForLoops(aleternativeMap, guardPos, guardDirection)
		}

		guardPos, guardDirection = nextGuardPos, nextGuardPosition

	}

	return possibleLoops
}

func createObstructionMap(height, width int) [][]bool {
	matrix := make([][]bool, height)
	for i := range height {
		row := make([]bool, width)
		matrix[i] = row
	}

	return matrix

}

func createAlternativeMap(lines []string, obstruction Point, height int) []string {
	newMap := make([]string, height)
	for i := range height {
		row := lines[i]
		if i == obstruction.Y {
			byteRow := []byte(row)
			byteRow[obstruction.X] = '#'
			row = string(byteRow)
		}
		newMap[i] = row
	}

	return newMap
}

func obstructionTested(obstructionMap [][]bool, point Point) bool {
	if obstructionMap[point.Y][point.X] {
		return true
	}

	obstructionMap[point.Y][point.X] = true
	return false
}

func testForLoops(lines []string, guardPos Point, direction int) int {
	visitedMap := createVisitedMap(len(lines), len(lines[0]))

	for {
		newStep := savePosition(visitedMap, guardPos, direction)
		guardPos, direction = nextGuardPosition(lines, guardPos, direction)
		if newStep == -1 {
			return 1
		} else if direction == -1 {
			return 0
		}
	}
}
