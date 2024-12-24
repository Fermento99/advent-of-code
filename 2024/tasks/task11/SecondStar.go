package task11

import "aoc-24/utils"

var stoneBlinkMap = map[[2]int]int{}

func SecondStar(lines []string) int {
	stones := utils.MapStringToNumbers(lines[0], " ")
	sum := 0

	for _, stone := range stones {
		sum += recursiveStoneBlink(stone, 75)
	}

	return sum
}

func recursiveStoneBlink(stone, blinksLeft int) int {
	cachedValue, cacheExists := stoneBlinkMap[[2]int{stone, blinksLeft}]
	if cacheExists {
		return cachedValue
	}

	if blinksLeft == 1 {
		value := len(changeStone(stone))
		stoneBlinkMap[[2]int{stone, 1}] = value
		return value
	}

	newStones := changeStone(stone)
	sum := 0
	for _, stone := range newStones {
		sum += recursiveStoneBlink(stone, blinksLeft-1)
	}

	stoneBlinkMap[[2]int{stone, blinksLeft}] = sum

	return sum
}
