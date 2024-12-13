package task5

import (
	"aoc-24/utils"
	"strconv"
	"strings"
)

func findEmptylineIndex(lines []string) int {
	for index, line := range lines {
		if line == "" {
			return index
		}
	}
	return -1
}

func FirstStar(lines []string) int {
	breakIndex := findEmptylineIndex(lines)
	orderChecker := createOrderChecker(lines[:breakIndex])

	sum := 0
	for _, instructions := range lines[breakIndex+1:] {
		sum += checkManual(utils.MapStringToNumbers(instructions, ","), orderChecker)
	}

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
	}

	if indices[0] == -1 || indices[1] == -1 || indices[0] < indices[1] {
		return true
	}

	return false
}
