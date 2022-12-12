f = open('./input.txt', 'r')
map = [line.replace('\n', '') for line in f.readlines()]
f.close()

height = len(map)
width = len(map[0])
start = (-1, -1)
end = (-1, -1)

for y in range(height):
    start_index = -1
    end_index = -1
    row = map[y]
    start_index = row.find('S')
    end_index = row.find('E')
    if start_index != -1:
        start = (start_index, y)
        map[y] = map[y].replace('S', 'a')
    if end_index != -1:
        end = (end_index, y)
        map[y] = map[y].replace('E', 'z')
    if start != (-1, -1) and end != (-1, -1):
        break

converted_map = []

for y in range(height):
    row = []
    for x in range(width):
        row.append(ord(map[y][x]) - 97)
    converted_map.append(row) 

queue = [(end,0)]
visited = [end]
distance = 0

while len(queue) > 0:
    node = min(queue, key=lambda n: n[1])
    queue.remove(node)
    coverage = 100 * len(visited) // (height * width)
    print('\rcoverage: [{}{}] {}%'.format('#'*(coverage//10), '.'*(10-(coverage//10)), coverage), end='')

    x, y = node[0]
    d = node[1]
    h = converted_map[y][x]

    if h == 0:
        distance = d
        break

    if x > 0:
        if converted_map[y][x-1] - h >= -1 and (x-1, y) not in visited:
            queue.append(((x-1, y), d+1))
            visited.append(((x-1, y)))
    if x < width-1:
        if converted_map[y][x+1] - h >= -1 and (x+1, y) not in visited:
            queue.append(((x+1, y), d+1))
            visited.append(((x+1, y)))
    if y > 0:
        if converted_map[y-1][x] - h >= -1 and (x, y-1) not in visited:
            queue.append(((x, y-1), d+1))
            visited.append(((x, y-1)))
    if y < height-1:
        if converted_map[y+1][x] - h >= -1 and (x, y+1) not in visited:
            queue.append(((x, y+1), d+1))
            visited.append(((x, y+1)))

print()
print(distance)