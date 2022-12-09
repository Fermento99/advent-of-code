f = open('./input.txt', 'r')
grid = [[int(tree) for tree in list(line.replace('\n', ''))] for line in f.readlines()]
f.close()

def scenic_score(x, y, grid, height, width):
    if x == 0 or y == 0 or x == width or y == height:
        return 0
    curr_tree = grid[y][x]

    row = grid[y]
    column = [row[x] for row in grid]

    w = 0
    for tree in reversed(row[:x]):
        w += 1
        if tree >= curr_tree:
            break
    
    e = 0
    for tree in row[x+1:]:
        e += 1
        if tree >= curr_tree:
            break

    n = 0
    for tree in reversed(column[:y]):
        n += 1
        if tree >= curr_tree:
            break

    s = 0
    for tree in column[y+1:]:
        s += 1
        if tree >= curr_tree:
            break

    return n * e * s * w

height = len(grid)
width = len(grid[0])
max_score = 0

for row in range(height):
    for column in range(width):
        score = scenic_score(column, row, grid, height, width)
        if score > max_score:
            max_score = score

print(max_score)