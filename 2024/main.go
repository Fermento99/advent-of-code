package main

import (
	task "aoc-24/tasks/task2"
	"aoc-24/utils"
	"fmt"
)

func main() {
	lines := utils.GetInput("02", false)
	firstSolution := task.FirstStar(lines)
	secondSolution := task.SecondStar(lines)

	fmt.Println("First solution: ", firstSolution)
	fmt.Println("Second solution: ", secondSolution)
}
