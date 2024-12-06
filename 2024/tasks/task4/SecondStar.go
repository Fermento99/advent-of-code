package task4

import "slices"

var properRings = []string{"SSMM", "SMMS", "MMSS", "MSSM"}

func SecondStar(lines []string) int {
	return countXMas(lines)
}

func countXMas(lines []string) int {
	count := 0

	for y, line := range lines {
		for x, point := range line {
			if point == 'M' || point == 'S' {
				count += searchXMas(x, y, lines)
			}
		}
	}

	return count
}

func searchXMas(x, y int, lines []string) int {
	horizontal := x+2 < len(lines[0])
	vertical := y+2 < len(lines)

	if !horizontal || !vertical || lines[y+1][x+1] != 'A' {
		return 0
	}

	ring := string([]byte{lines[y][x], lines[y][x+2], lines[y+2][x+2], lines[y+2][x]})
	return validateXMas(ring)
}

func validateXMas(ring string) int {
	if slices.Contains(properRings, ring) {
		return 1
	}

	return 0
}
