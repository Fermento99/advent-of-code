package task5

import (
	"fmt"
	"strconv"
	"strings"
)

var checkCounter int = 0

func findEmptylineIndex(lines []string) int {
	for index, line := range lines {
		if line == "" {
			return index
		}
	}
	return -1
}

func mapStringToNumbers(line string) []int {
	numbers := strings.Split(line, ",")
	tab := make([]int, len(numbers))
	for index, number := range numbers {
		tab[index], _ = strconv.Atoi(number)
	}

	return tab
}

func FirstStar(lines []string) int {
	breakIndex := findEmptylineIndex(lines)
	orderChecker := createOrderChecker(lines[:breakIndex])

	sum := 0
	for _, instructions := range lines[breakIndex+1:] {
		sum += checkManual(mapStringToNumbers(instructions), orderChecker)
	}
	fmt.Println(checkCounter)
	return sum
}

func createOrderChecker(rules []string) [][2]int {
	ruleSet := make([][2]int, len(rules))
	for i, rule := range rules {
		var parsedRule [2]int
		for j, number := range strings.Split(rule, "|") {
			parsedRule[j], _ = strconv.Atoi(number)
		}
		ruleSet[i] = parsedRule
	}

	return ruleSet
}

func checkManual(instructions []int, rules [][2]int) int {
	for _, rule := range rules {
		if !checkRule(instructions, rule) {
			return 0
		}
	}

	return instructions[len(instructions)/2]
}

func checkRule(instructions []int, rule [2]int) bool {
	indices := [2]int{-1, -1}
	for index, instruction := range instructions {
		if instruction == rule[0] {
			indices[0] = index
		} else if instruction == rule[1] {
			indices[1] = index
		}

		checkCounter++
	}

	if indices[0] == -1 || indices[1] == -1 || indices[0] < indices[1] {
		return true
	}

	return false
}
