f = open('./input.txt', 'r')
grid = [[int(tree) for tree in list(line.replace('\n', ''))] for line in f.readlines()]
f.close()

def is_visible(x, y, grid, height, width):
    if x == 0 or y == 0 or x == width or y == height:
        return True 
    
    curr_tree = grid[y][x]

    row = grid[y]

    higher = False
    for tree in row[:x]:
        if tree >= curr_tree:
            higher = True
            break
    if not higher: return True

    higher = False
    for tree in row[x+1:]:
        if tree >= curr_tree:
            higher = True
            break
    if not higher: return True

    column = [row[x] for row in grid]

    higher = False
    for tree in column[:y]:
        if tree >= curr_tree:
            higher = True
            break
    if not higher: return True

    higher = False
    for tree in column[y+1:]:
        if tree >= curr_tree:
            higher = True
            break
    if not higher: return True

    return False

height = len(grid)
width = len(grid[0])
visible_count = 0

for row in range(height):
    for column in range(width):
        if is_visible(column, row, grid, height, width):
            visible_count += 1

print(visible_count)