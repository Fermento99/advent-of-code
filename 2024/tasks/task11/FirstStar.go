package task11

import (
	"aoc-24/utils"
)

func FirstStar(lines []string) int {
	stones := utils.MapStringToNumbers(lines[0], " ")

	for range 25 {
		stones = changeStoneLine(stones)
	}

	return len(stones)
}

func changeStoneLine(stones []int) []int {
	newStones := make([]int, 0, len(stones)*2)

	for _, stone := range stones {
		newStones = append(newStones, changeStone(stone)...)
	}

	return newStones
}

func changeStone(stone int) []int {
	if stone == 0 {
		return []int{1}
	}

	stoneDigits := utils.IntLength(stone)
	if stoneDigits%2 == 0 {
		spliter := utils.Pow(10, stoneDigits/2)
		return []int{stone / spliter, stone % spliter}
	}

	return []int{stone * 2024}
}
