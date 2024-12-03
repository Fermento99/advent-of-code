package utils

import (
	"os"
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
