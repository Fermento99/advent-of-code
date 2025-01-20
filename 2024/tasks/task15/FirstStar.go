package task15

import (
	"aoc-24/utils"
	"fmt"
	"regexp"
)

type Point = utils.Point

func FirstStar(lines []string) int {
	warehouseMap, robotInstructions, robotPos := parseInput(lines)
	executeRobotMoves(warehouseMap, robotInstructions, robotPos, false)

	// printMap(warehouseMap)
	return calculateGPSSum(warehouseMap)
}

func parseWarehouseMap(lines []string) [][]byte {
	res := make([][]byte, len(lines))

	for y, line := range lines {
		res[y] = []byte(line)
	}

	return res
}

func parseInput(lines []string) ([][]byte, []string, Point) {
	robotRegexp := regexp.MustCompile("@")
	robotPos := Point{}

	for i, line := range lines {
		x := robotRegexp.FindStringIndex(line)

		if x != nil {
			robotPos = Point{X: x[0], Y: i}
		}

		if lines[i] == "" {
			return parseWarehouseMap(lines[:i]), lines[i+1:], robotPos
		}
	}

	return [][]byte{}, []string{}, Point{}
}

func executeRobotMoves(warehouseMap [][]byte, instructions []string, robot Point, wide bool) {
	for _, instructionList := range instructions {
		for _, instruction := range instructionList {
			if wide {
				robot = executeWideMove(warehouseMap, instruction, robot)
			} else {
				robot = executeMove(warehouseMap, instruction, robot)
			}
		}
	}
}

func executeMove(warehouseMap [][]byte, instruction rune, robot Point) Point {
	switch instruction {
	case '>':
		return tryMove(warehouseMap, robot, Point{X: 1, Y: 0})
	case '<':
		return tryMove(warehouseMap, robot, Point{X: -1, Y: 0})
	case '^':
		return tryMove(warehouseMap, robot, Point{X: 0, Y: -1})
	case 'v':
		return tryMove(warehouseMap, robot, Point{X: 0, Y: 1})
	}

	return Point{}
}

func tryMove(warehouseMap [][]byte, robot, velocity Point) Point {
	nextStep := robot.AddPoints(velocity)
	switch getPoint(warehouseMap, nextStep) {
	case '.':
		setPoint(warehouseMap, robot, '.')
		setPoint(warehouseMap, nextStep, '@')
		return nextStep
	case 'O':
		firstEmpty := pushBoxes(warehouseMap, nextStep, velocity)
		if nextStep != firstEmpty {
			setPoint(warehouseMap, robot, '.')
			setPoint(warehouseMap, nextStep, '@')
			setPoint(warehouseMap, firstEmpty, 'O')
			return nextStep
		}
	}

	return robot
}

func pushBoxes(warehouseMap [][]byte, firstBox, velocity Point) Point {
	firstEmpty := firstBox.AddPoints(velocity)
	for {
		switch getPoint(warehouseMap, firstEmpty) {
		case '.':
			return firstEmpty
		case 'O':
			firstEmpty = firstEmpty.AddPoints(velocity)
		case '#':
			return firstBox
		}
	}
}

func setPoint(warehouse [][]byte, point Point, value byte) {
	warehouse[point.Y][point.X] = value
}

func getPoint(warehouse [][]byte, point Point) byte {
	return warehouse[point.Y][point.X]
}

func calculateGPSSum(warehouse [][]byte) int {
	sum := 0

	for y, line := range warehouse {
		for x, char := range line {
			if char == 'O' {
				sum += 100*y + x
			}
		}
	}

	return sum
}

func printMap(warehouseMap [][]byte) {
	for _, line := range warehouseMap {
		fmt.Println(string(line))
	}
}
