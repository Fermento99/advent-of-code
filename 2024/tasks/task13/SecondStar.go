package task13

import "fmt"

func SecondStar(lines []string) int {
	machines := createClawMachines(lines, true)
	tokens := 0

	for _, machine := range machines {
		aClicks := getAClicks(machine)
		bClicks := getBClicks(machine)
		if aClicks >= 0 && bClicks >= 0 {
			tokens += aClicks*3 + bClicks
		}
		if tokens < 0 {
			fmt.Println("error")
		}
	}

	return tokens
}
