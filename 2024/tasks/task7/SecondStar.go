package task7

func SecondStar(lines []string) int {
	sum := 0

	for _, line := range lines {
		sum += testLine(line, []string{"+", "*", "||"})
	}

	return sum
}
