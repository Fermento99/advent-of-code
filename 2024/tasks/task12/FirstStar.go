package task12

import "aoc-24/utils"

type Point = utils.Point

func FirstStar(lines []string) int {
	regions := createRegions(lines)
	return evaluateFencesCost(regions)
}

func createRegions(lines []string) [][]Point {
	regionMap := make(map[rune][][]Point)

	for y, row := range lines {
		for x, char := range row {
			appendPoint(regionMap, Point{X: x, Y: y}, char)
		}
	}

	regions := make([][]Point, 0)

	for _, value := range regionMap {
		regions = append(regions, value...)
	}

	return regions
}

func appendPoint(regionMap map[rune][][]Point, point Point, id rune) {
	validRegions := regionMap[id]

	if len(validRegions) == 0 {
		regionMap[id] = [][]Point{createNewRegion(point)}
	} else {
		alreadyAppended := -1
		regionsToDelete := make([]int, 0, len(validRegions))

		for index, region := range validRegions {
			for _, regPoint := range region {
				if areNeighbours(regPoint, point) {
					if alreadyAppended == -1 {
						regionMap[id][index] = append(region, point)
						alreadyAppended = index
					} else {
						regionMap[id][alreadyAppended] = append(regionMap[id][alreadyAppended], region...)
						regionsToDelete = append(regionsToDelete, index)
					}
					break
				}
			}
		}

		if alreadyAppended == -1 {
			regionMap[id] = append(regionMap[id], createNewRegion(point))
		} else {
			regionMap[id] = filterArray(regionMap[id], regionsToDelete)
		}
	}
}

func createNewRegion(point Point) []Point {
	region := make([]Point, 1)
	region[0] = point
	return region
}

func filterArray[T any](tab []T, indices []int) []T {
	newTab := make([]T, len(tab)-len(indices))

	for i, offset := 0, 0; i < len(tab); i++ {
		if utils.ArrayContains(indices, i) {
			offset--
		} else {
			newTab[i+offset] = tab[i]
		}
	}

	return newTab
}

func areNeighbours(a, b Point) bool {
	return (a.X == b.X && utils.Abs(a.Y-b.Y) == 1) ||
		(a.Y == b.Y && utils.Abs(a.X-b.X) == 1)
}

func getNeighboursCount(point Point, region []Point) int {
	count := 0

	for _, regPoint := range region {
		if areNeighbours(regPoint, point) {
			count++
		}

		if count == 4 {
			break
		}
	}

	return count
}

func evaluateFencesCost(regions [][]Point) int {
	cost := 0

	for _, region := range regions {
		area := len(region)
		perimiter := getPerimiter(region)
		cost += area * perimiter
	}

	return cost
}

func getPerimiter(region []Point) int {
	perimiter := 0

	for _, point := range region {
		perimiter += 4 - getNeighboursCount(point, region)
	}

	return perimiter
}
