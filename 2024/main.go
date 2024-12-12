package main

import (
	task "aoc-24/tasks/task9"
	"aoc-24/utils"
	"fmt"
)

var knownInput bool = false
var taskNumber string = "09"

func main() {
	lines := utils.GetInput(taskNumber, knownInput)
	firstSolution := task.FirstStar(lines)
	secondSolution := task.SecondStar(lines)

	fmt.Println("First solution: ", firstSolution)
	fmt.Println("Second solution: ", secondSolution)
}
