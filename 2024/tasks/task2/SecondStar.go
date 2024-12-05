package task2

import (
	"strconv"
	"strings"
)

func SecondStar(lines []string) int {
	sum := 0
	for _, line := range lines {
		if isLineSafeWithFault(parseLine(line), false) {
			sum++
		}
	}

	return sum
}

func parseLine(line string) []int {
	elems := strings.Split(line, " ")
	out := make([]int, len(elems))

	for i, elem := range elems {
		out[i], _ = strconv.Atoi(elem)
	}

	return out
}

func isLineSafeWithFault(line []int, withFault bool) bool {
	sign := 0
	last := 0

	for index, step := range line {
		if index == 0 {
			last = step
			continue
		} else if index == 1 {
			sign = getSign(last, step)
		}

		diff := (step - last) * sign
		if diff < 1 || diff > 3 {
			if !withFault {
				if index <= 2 {
					return isLineSafe(line[1:]) || isLineSafe(append(line[:1], line[2:]...))
				}
				withFault = true
				continue
			} else {
				return false
			}
		}

		last = step
	}

	return true
}
