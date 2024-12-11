package utils

import (
	"os"
	"strconv"
	"strings"
)

func GetInput(taskNumber string, knownInput bool) []string {
	var data []byte
	var err error
	if knownInput {
		data, err = os.ReadFile("./known-inputs/" + taskNumber + ".txt")
	} else {
		data, err = os.ReadFile("./inputs/" + taskNumber + ".txt")
	}

	if err != nil {
		panic(err)
	}
	return strings.Split(string(data), "\n")
}

func Abs(num int) int {
	if num > 0 {
		return num
	}

	return -num
}

func ArrayContains[T comparable](tab []T, val T) bool {
	for _, el := range tab {
		if el == val {
			return true
		}
	}

	return false
}

func MapStringToNumbers(line string, delim string) []int {
	numbers := strings.Split(line, " ")
	tab := make([]int, len(numbers))
	for index, number := range numbers {
		tab[index], _ = strconv.Atoi(number)
	}

	return tab
}
