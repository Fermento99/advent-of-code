package task5

import (
	"fmt"
	"sort"
)

func SecondStar(lines []string) int {
	breakIndex := findEmptylineIndex(lines)
	orderChecker := createOrderChecker(lines[:breakIndex])

	sum := 0
	for _, instructions := range lines[breakIndex+1:] {
		sum += sortManuals(mapStringToNumbers(instructions), orderChecker)
	}
	fmt.Println(checkCounter)
	return sum
}

func sortManuals(instructions []int, rules [][2]int) int {
	middleValue := checkManual(instructions, rules)
	if middleValue != 0 {
		return 0
	}

	sortedInstructions := sortInstructions(instructions, rules)
	return sortedInstructions[len(instructions)/2]
}

func sortInstructions(instructions []int, rules [][2]int) []int {
	sort.Slice(instructions, func(i, j int) bool {
		for _, rule := range rules {
			if !checkRule([]int{instructions[i], instructions[j]}, rule) {
				return true
			}
		}

		return false
	})

	return instructions
}
