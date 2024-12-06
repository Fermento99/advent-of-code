package task4

func FirstStar(lines []string) int {
	return countXmas(lines)
}

func countXmas(lines []string) int {
	count := 0

	for y, line := range lines {
		for x, point := range line {
			if point == 'X' {
				count += searchKeywords("XMAS", x, y, lines)
			} else if point == 'S' {
				count += searchKeywords("SAMX", x, y, lines)
			}
		}
	}

	return count
}

func searchKeywords(keyword string, x, y int, lines []string) int {
	horizontal := x+4 <= len(lines[0])
	vertical := y+4 <= len(lines)
	topVertical := y-3 >= 0 && x+4 <= len(lines[0])
	count := 0

	if horizontal {
		count += searchHorizontal(keyword, x, y, lines)
	}

	if vertical {
		count += searchVartical(keyword, x, y, lines)
	}

	if horizontal && vertical {
		count += searchDiagonal(keyword, x, y, lines)
	}

	if topVertical {
		count += searchTopDiagonal(keyword, x, y, lines)
	}

	return count
}

func searchHorizontal(keyword string, x, y int, lines []string) int {
	for i := range keyword {
		if lines[y][x+i] != keyword[i] {
			return 0
		}
	}

	return 1
}

func searchVartical(keyword string, x, y int, lines []string) int {
	for i := range keyword {
		if lines[y+i][x] != keyword[i] {
			return 0
		}
	}

	return 1
}

func searchDiagonal(keyword string, x, y int, lines []string) int {
	for i := range keyword {
		if lines[y+i][x+i] != keyword[i] {
			return 0
		}
	}

	return 1
}

func searchTopDiagonal(keyword string, x, y int, lines []string) int {
	for i := range keyword {
		if lines[y-i][x+i] != keyword[i] {
			return 0
		}
	}

	return 1
}
