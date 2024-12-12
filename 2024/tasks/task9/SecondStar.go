package task9

import (
	"aoc-24/utils"
	"sort"
)

type DiskEntity struct {
	Id, Start, Length int
}

func SecondStar(lines []string) int {
	disk := getEntityDisk(lines[0])
	defragedDisk := defragEntityDisk(disk, len(lines[0]))
	return getEntityDiskChecksum(defragedDisk)
}

func getEntityDisk(diskMap string) []DiskEntity {
	frags := utils.MapStringToNumbers(diskMap, "")
	disk := make([]DiskEntity, len(frags))
	index := 0

	for id, frag := range frags {
		if frag > 0 {
			var entityId int

			if id%2 == 0 {
				entityId = id / 2
			} else {
				entityId = -1
			}

			disk[id] = DiskEntity{entityId, index, frag}
			index += frag
		}
	}

	return deleteEmptyFrags(disk)
}

func deleteEmptyFrags(disk []DiskEntity) []DiskEntity {
	for i := len(disk) - 1; i > 0; i-- {
		if disk[i].Length == 0 {
			disk = deleteEntity(disk, i)
		}
	}

	return disk
}

func deleteEntity(disk []DiskEntity, index int) []DiskEntity {
	return append(disk[:index], disk[index+1:]...)
}

func filterEntities(disk []DiskEntity, maxEntities int) []DiskEntity {
	filtered := make([]DiskEntity, maxEntities)

	for _, entity := range disk {
		if entity.Id != -1 {
			filtered[entity.Id] = entity
		}
	}

	return filtered
}

func defragEntityDisk(disk []DiskEntity, maxLength int) []DiskEntity {
	defragedDisk := make([]DiskEntity, 0, maxLength)
	files := filterEntities(disk, maxLength/2+1)

	for _, entity := range disk {
		if entity.Id == -1 {
			replacement := make([]DiskEntity, 0, entity.Length)
			fileIndices := make([]int, 0, entity.Length)

			for i := len(files) - 1; i >= 0 && files[i].Start > entity.Start && entity.Length > 0; i-- {
				if files[i].Length <= entity.Length {
					replacement = append(replacement, files[i])
					fileIndices = append(fileIndices, i)
					entity.Length -= files[i].Length
				}
			}

			files = removeMovedFiles(files, fileIndices)

			replacementLength := 0
			for i := range replacement {
				replacement[i].Start = entity.Start + replacementLength
				replacementLength += replacement[i].Length
			}

			if entity.Length > 0 {
				entity.Start += replacementLength
				replacement = append(replacement, entity)
			}

			defragedDisk = append(defragedDisk, replacement...)
		} else if utils.ArrayContains(files, entity) {
			defragedDisk = append(defragedDisk, entity)
			files = removeUnmovedFile(files, entity.Id)
		}
	}

	return defragedDisk
}

func removeUnmovedFile(files []DiskEntity, id int) []DiskEntity {
	for index, file := range files {
		if file.Id == id {
			return deleteEntity(files, index)
		}
	}

	return files
}

func removeMovedFiles(files []DiskEntity, indices []int) []DiskEntity {
	sort.Sort(sort.Reverse(sort.IntSlice(indices)))

	for _, index := range indices {
		files = deleteEntity(files, index)
	}

	return files
}

func getEntityDiskChecksum(disk []DiskEntity) int {
	sum := 0

	for _, frag := range disk {
		if frag.Id > 0 {
			for i := range frag.Length {
				sum += frag.Id * (frag.Start + i)
			}
		}
	}

	return sum
}
