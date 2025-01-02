package task14

import "aoc-24/utils"

func SecondStar(lines []string) int {
	boundries := Point{X: 101, Y: 103}
	robots := make([]Robot, len(lines))

	for i, line := range lines {
		robots[i] = createBot(line)
	}

	seconds := 55
	positions := make([]Point, len(robots))

	for {
		for i, robot := range robots {
			positions[i] = getRobotPosition(robot, seconds, boundries)
		}

		if isTreeShape(positions) {
			break
		}

		seconds++
	}

	return seconds
}

func isTreeShape(robots []Point) bool {
	score := 0

	for i := 0; i < len(robots)-1; i++ {
		for j := i + 1; j < len(robots); j++ {
			if utils.Abs(robots[i].X-robots[j].X) == 1 && utils.Abs(robots[i].Y-robots[j].Y) == 1 {
				score++
			}
		}
	}

	return score > 160
}
