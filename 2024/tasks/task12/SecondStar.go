package task12

import "aoc-24/utils"

func SecondStar(lines []string) int {
	regions := createRegions(lines)
	return evaluateBulkFencesCost(regions)
}

func evaluateBulkFencesCost(regions [][]Point) int {
	cost := 0

	for _, region := range regions {
		area := len(region)
		sideCount := getSidesCount(region)
		cost += area * sideCount
	}

	return cost
}

func getSidesCount(region []Point) int {
	cornerMap := make(map[Point][]Point)

	for _, point := range region {
		corners := getPointCorners(point)
		for _, corner := range corners {
			cornerMap[corner] = append(cornerMap[corner], point)
		}
	}

	sideCount := 0

	for _, value := range cornerMap {
		if len(value) == 2 {
			if utils.Abs(value[0].X-value[1].X) == 1 && utils.Abs(value[0].Y-value[1].Y) == 1 {
				sideCount += 2
			}
		} else {
			sideCount += len(value) % 2
		}
	}

	return sideCount
}

func getPointCorners(point Point) [4]Point {
	corners := [4]Point{}

	for i := range 4 {
		corners[i] = Point{X: point.X + i/2, Y: point.Y + i%2}
	}

	return corners
}
