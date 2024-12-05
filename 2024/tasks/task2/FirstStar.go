package task2

func FirstStar(lines []string) int {
	sum := 0
	for _, line := range lines {
		if isLineSafe(parseLine(line)) {
			sum++
		}
	}

	return sum
}

func getSign(first, second int) int {
	if first < second {
		return 1
	}
	return -1
}

func isLineSafe(line []int) bool {
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
			return false
		}

		last = step
	}

	return true
}
