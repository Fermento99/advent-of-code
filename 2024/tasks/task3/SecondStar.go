package task3

import (
	"regexp"
)

func SecondStar(lines []string) int {
	return sumEnabledMuls(lines[0])
}

func sumEnabledMuls(line string) int {
	doReg, _ := regexp.Compile(`^.*?don\'t\(\)|do\(\).*?don\'t\(\)|do\(\).*?$`)
	mulReg, _ := regexp.Compile(`mul\(\d{1,3},\d{1,3}\)`)

	sum := 0
	for _, doCode := range doReg.FindAllString(line, -1) {
		muls := mulReg.FindAllString(doCode, -1)
		sum += getSumOfMuls(muls)
	}

	return sum
}
