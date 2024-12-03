package main

import (
	task1 "aoc-24/tasks"
	"aoc-24/utils"
	"fmt"
)

func main() {
	lines := utils.GetInput("01", false)
	firstSolution := task1.FirstStar(lines)
	secondSolution := task1.SecondStar(lines)

	fmt.Println("First solution: ", firstSolution)
	fmt.Println("Second solution: ", secondSolution)
}
