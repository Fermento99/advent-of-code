package task14

import (
	"aoc-24/utils"
	"regexp"
	"strconv"
)

type Point = utils.Point

type Robot struct {
	Position Point
	Velocity Point
}

func FirstStar(lines []string) int {
	quadrants := make([]int, 4)
	boundries := Point{X: 101, Y: 103}
	midWidth := boundries.X / 2
	midHeight := boundries.Y / 2

	for _, line := range lines {
		robot := createBot(line)
		newPos := getRobotPosition(robot, 100, boundries)

		if newPos.X < midWidth && newPos.Y < midHeight {
			quadrants[0]++
			continue
		}
		if newPos.X > midWidth && newPos.Y < midHeight {
			quadrants[1]++
			continue
		}
		if newPos.X > midWidth && newPos.Y > midHeight {
			quadrants[2]++
			continue
		}
		if newPos.X < midWidth && newPos.Y > midHeight {
			quadrants[3]++
			continue
		}
	}

	safety := 1

	for _, quadrant := range quadrants {
		safety *= quadrant
	}

	return safety
}

func createBot(line string) Robot {
	numberRegexp := regexp.MustCompile(`-?\d+`)
	numbers := numberRegexp.FindAllString(line, 4)
	nums := make([]int, 4)

	for i, number := range numbers {
		nums[i], _ = strconv.Atoi(number)
	}

	return Robot{
		Position: Point{X: nums[0], Y: nums[1]},
		Velocity: Point{X: nums[2], Y: nums[3]},
	}
}

func getRobotPosition(robot Robot, time int, boundries Point) Point {
	newPos := Point{
		X: robot.Position.X + robot.Velocity.X*time,
		Y: robot.Position.Y + robot.Velocity.Y*time,
	}

	newPos.X %= boundries.X
	newPos.Y %= boundries.Y

	if newPos.X < 0 {
		newPos.X += boundries.X
	}

	if newPos.Y < 0 {
		newPos.Y += boundries.Y
	}

	return newPos
}
