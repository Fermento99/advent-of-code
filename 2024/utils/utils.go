package utils

import (
	"os"
	"strconv"
	"strings"
)

type Point struct {
	X int
	Y int
}

func (a Point) AddPoints(b Point) Point {
	return Point{X: a.X + b.X, Y: a.Y + b.Y}
}

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
	numbers := strings.Split(line, delim)
	tab := make([]int, len(numbers))
	for index, number := range numbers {
		tab[index], _ = strconv.Atoi(number)
	}

	return tab
}

func Pow(base, exponent int) int {
	out := base

	for range exponent - 1 {
		out *= base
	}

	return out
}

func IntLength(n int) int {
	digits := 1
	reminder := n / 10

	for reminder > 0 {
		digits++
		reminder /= 10
	}

	return digits
}
