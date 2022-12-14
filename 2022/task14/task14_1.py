f = open('./input.txt', 'r')
lines = [line.replace('\n', '') for line in f.readlines()]
f.close()

lines = [[[int(coord) for coord in point.split(',')] for point in line.split("->")] for line in lines]
point_list = [point for line in lines for point in line]

height = max(point_list, key=lambda x: x[1])[1] + 1
offset_x = min(point_list, key=lambda x: x[0])[0]
width = max(point_list, key=lambda x: x[0])[0] - offset_x + 1

cave = [['.' for _ in range(width)] for _ in range(height)]
cave[0][500-offset_x] = '+'

for line in lines:
    last_point = (-1,-1)
    while len(line) > 0:
        point = line.pop()
        cave[point[1]][point[0]-offset_x] = '#'
        if last_point != (-1,-1):
            if last_point[0] == point[0]:
                step = 1 if point[1] - last_point[1] > 0 else -1
                for i in range(last_point[1], point[1], step):
                    cave[i][point[0]-offset_x] = '#'
            else:
                step = 1 if point[0] - last_point[0] > 0 else -1
                for i in range(last_point[0], point[0], step):
                    cave[point[1]][i-offset_x] = '#'
        last_point = point

infinity = False
count = 0
while not infinity:
    sand = [500-offset_x, 1]
    stable = False
    while not stable:
        try:
            if cave[sand[1]+1][sand[0]] == '.':
                sand[1] += 1
            elif cave[sand[1]+1][sand[0]-1] == '.':
                sand[1] += 1
                sand[0] -= 1
            elif cave[sand[1]+1][sand[0]+1] == '.':
                sand[1] += 1
                sand[0] += 1
            else:
                stable = True
                count += 1
                cave[sand[1]][sand[0]] = 'o'
        except:
            infinity = True
            break

print(count)
