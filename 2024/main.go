package main

import (
	task "aoc-24/tasks/task14"
	"aoc-24/utils"
	"fmt"
	"time"
)

var knownInput bool = false
var taskNumber string = "14"

func main() {
	lines := utils.GetInput(taskNumber, knownInput)

	firstStarStart := time.Now()
	firstSolution := task.FirstStar(lines)
	firstStarElapsedTime := time.Since(firstStarStart)

	secondStarStart := time.Now()
	secondSolution := task.SecondStar(lines)
	secondStarElapsedTime := time.Since(secondStarStart)

	fmt.Println("First solution time: ", firstStarElapsedTime)
	fmt.Println("First solution: ", firstSolution)
	fmt.Println("Second solution time: ", secondStarElapsedTime)
	fmt.Println("Second solution: ", secondSolution)
}
