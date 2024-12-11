package task7

import (
	"aoc-24/utils"
	"strconv"
	"strings"
)

func FirstStar(lines []string) int {
	sum := 0

	for _, line := range lines {
		sum += testLine(line, []string{"+", "*"})
	}

	return sum
}

func testLine(line string, operators []string) int {
	parts := strings.Split(line, ": ")
	result, _ := strconv.Atoi(parts[0])
	components := utils.MapStringToNumbers(parts[1], " ")

	for _, combination := range getCombinations(len(components)-1, operators) {
		if result == evalEquation(components, combination) {
			return result
		}
	}

	return 0
}

func evalEquation(components []int, operators []string) int {
	result := components[0]

	for i, operator := range operators {
		result = evalOperation(result, components[i+1], operator)
	}

	return result
}

func evalOperation(a, b int, operator string) int {
	switch operator {
	case "+":
		return a + b
	case "*":
		return a * b
	case "||":
		return a*pow(10, intLength(b)) + b
	}
	return 0
}

func getCombinations(length int, operators []string) [][]string {
	if length == 1 {
		combinations := make([][]string, len(operators))

		for index, operator := range operators {
			combinations[index] = make([]string, 1)
			combinations[index][0] = operator
		}

		return combinations
	}

	combinations := getCombinations(length-1, operators)
	operatorCount := len(operators)
	nextCombinations := make([][]string, pow(operatorCount, length))

	for index, combination := range combinations {
		newCombinations := make([][]string, operatorCount)

		for i, operator := range operators {
			newCombinations[i] = make([]string, length)
			newCombinations[i][length-1] = operator
		}

		for i, newCombination := range newCombinations {
			for j := range length - 1 {
				newCombination[j] = combination[j]
			}

			nextCombinations[index*operatorCount+i] = newCombination
		}
	}

	return nextCombinations
}

func pow(base, exponent int) int {
	out := base

	for range exponent - 1 {
		out *= base
	}

	return out
}

func intLength(n int) int {
	digits := 1
	reminder := n / 10

	for reminder > 0 {
		digits++
		reminder /= 10
	}

	return digits
}
