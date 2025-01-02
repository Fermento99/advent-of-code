package task13

import (
	"aoc-24/utils"
	"regexp"
	"strconv"
)

type Point = utils.Point

type ClawMachine struct {
	ButtonA Point
	ButtonB Point
	Prize   Point
}

func FirstStar(lines []string) int {
	machines := createClawMachines(lines, false)
	tokens := 0

	for _, machine := range machines {
		aClicks := getAClicks(machine)
		bClicks := getBClicks(machine)
		if aClicks >= 0 && bClicks >= 0 {
			tokens += aClicks*3 + bClicks
		}
	}

	return tokens
}

func createClawMachines(lines []string, withError bool) []ClawMachine {
	machineCount := (len(lines) + 1) / 4
	machines := make([]ClawMachine, machineCount)

	for i := range machineCount {
		buttonA := extractPoint(lines[i*4], false)
		buttonB := extractPoint(lines[i*4+1], false)
		prize := extractPoint(lines[i*4+2], withError)

		machines[i] = ClawMachine{ButtonA: buttonA, ButtonB: buttonB, Prize: prize}
	}

	return machines
}

func extractPoint(line string, withError bool) Point {
	numberRegexp := regexp.MustCompile(`\d+`)
	numbers := numberRegexp.FindAllString(line, 2)

	x, _ := strconv.Atoi(numbers[0])
	y, _ := strconv.Atoi(numbers[1])

	if withError {
		return Point{X: x + 10000000000000, Y: y + 10000000000000}
	}

	return Point{X: x, Y: y}
}

func getBClicks(machine ClawMachine) int {
	num := float64(machine.ButtonA.Y*machine.Prize.X - machine.ButtonA.X*machine.Prize.Y)
	denum := float64(machine.ButtonA.Y*machine.ButtonB.X - machine.ButtonA.X*machine.ButtonB.Y)

	res := num / denum

	if res == float64(int(res)) {
		return int(res)
	}

	return -1
}

func getAClicks(machine ClawMachine) int {
	num := float64(machine.ButtonB.Y*machine.Prize.X - machine.ButtonB.X*machine.Prize.Y)
	denum := float64(machine.ButtonB.Y*machine.ButtonA.X - machine.ButtonB.X*machine.ButtonA.Y)

	res := num / denum

	if res == float64(int(res)) {
		return int(res)
	}

	return -1
}
