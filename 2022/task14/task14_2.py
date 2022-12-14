f = open('./input.txt', 'r')
lines = [line.replace('\n', '') for line in f.readlines()]
f.close()

def streach_cave(cave, side):
    if side == -1:
        for row in range(len(cave)):
            cave[row] = ['.'] + cave[row]
    elif side == 1:
        for row in cave:
            row.append('.')

lines = [[[int(coord) for coord in point.split(',')] for point in line.split("->")] for line in lines]
point_list = [point for line in lines for point in line]

height = max(point_list, key=lambda x: x[1])[1] + 3
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

clogged = False
count = 0
while not clogged:
    sand = [500-offset_x, 0]
    stable = False
    while not stable:
        if sand[1]+1 == height:
            stable = True
            cave[sand[1]][sand[0]] = 'o'
            break
        if sand[0]-1 < 0:
            streach_cave(cave, -1)
            width += 1
            offset_x -= 1
            sand[0] += 1
        elif sand[0]+1 >= width-1:
            streach_cave(cave, 1)
            width += 1

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
            cave[sand[1]][sand[0]] = 'o'
            count += 1
            if sand == [500-offset_x, 0]:
                clogged = True
            
print(count)
