package task9

import (
	"aoc-24/utils"
)

func FirstStar(lines []string) int {
	disk := getDisk(lines[0])
	defragedDisk := defragDisk(disk)
	return getDiskChecksum(defragedDisk)
}

func getDisk(diskMap string) []int {
	frags := utils.MapStringToNumbers(diskMap, "")

	diskLength := getDiskLength(frags)
	disk := make([]int, diskLength)
	i := 0

	for id, frag := range frags {
		if id%2 == 0 {
			for range frag {
				disk[i] = id / 2
				i++
			}
		} else {
			for range frag {
				disk[i] = -1
				i++
			}
		}
	}

	return disk
}

func getDiskLength(fragments []int) int {
	sum := 0

	for _, fragLength := range fragments {
		sum += fragLength
	}

	return sum
}

func defragDisk(disk []int) []int {
	for i, j := 0, len(disk)-1; i < j; i++ {
		if disk[i] == -1 {
			swap(disk, i, j)
			for disk[j] == -1 {
				j--
			}
		}
	}

	return disk
}

func swap(disk []int, i, j int) {
	temp := disk[i]
	disk[i] = disk[j]
	disk[j] = temp
}

func getDiskChecksum(disk []int) int {
	sum := 0

	for i := 1; disk[i] != -1; i++ {
		sum += i * disk[i]
	}

	return sum
}
