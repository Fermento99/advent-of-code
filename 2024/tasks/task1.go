package task1

import (
	"sort"
	"strconv"
	"strings"
)

func FirstStar(lines []string) int {
	list1, list2 := make([]int, len(lines)), make([]int, len(lines))

	for index, line := range lines {
		elements := strings.Split(line, "   ")
		list1[index], _ = strconv.Atoi(elements[0])
		list2[index], _ = strconv.Atoi(elements[1])
	}

	sort.Ints(list1)
	sort.Ints(list2)

	sum := 0
	for index := range list1 {
		if list1[index] > list2[index] {
			sum += list1[index] - list2[index]
		} else {
			sum += list2[index] - list1[index]
		}
	}

	return sum
}

func SecondStar(lines []string) int {
	list1, list2 := make([]int, len(lines)), make([]int, len(lines))

	for index, line := range lines {
		elements := strings.Split(line, "   ")
		list1[index], _ = strconv.Atoi(elements[0])
		list2[index], _ = strconv.Atoi(elements[1])
	}

	sort.Ints(list1)
	sort.Ints(list2)

	i2, sum, last, counter := 0, 0, 0, 0
	for i1 := range list1 {
		if last == list1[i1] {
			sum += last * counter
		} else {
			counter = 0
			for list2[i2] < list1[i1] && i2 < len(list2)-1 {
				i2++
			}
			for ; list1[i1] == list2[i2] && i2 < len(list2)-1; i2++ {
				counter++
			}
			last = list1[i1]
			sum += last * counter
		}
	}

	return sum
}
