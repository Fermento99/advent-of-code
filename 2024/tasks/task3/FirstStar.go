package task3

import (
	"regexp"
	"strconv"
)

func FirstStar(lines []string) int {
	muls := findMuls(lines[0])
	return getSumOfMuls(muls)
}

func getSumOfMuls(muls []string) int {
	sum := 0
	for _, mul := range muls {
		sum += executeMul(mul)
	}

	return sum
}

func executeMul(mul string) int {
	reg, _ := regexp.Compile(`\d{1,3}`)
	numbers := reg.FindAllString(mul, 2)

	product := 1
	for _, number := range numbers {
		parsedInt, _ := strconv.Atoi(number)
		product *= parsedInt
	}

	return product
}

func findMuls(line string) []string {
	reg, _ := regexp.Compile(`mul\(\d{1,3},\d{1,3}\)`)

	return reg.FindAllString(line, -1)
}
