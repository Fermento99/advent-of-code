from Rope import Rope

f = open('./input.txt', 'r')
moves = [line.replace('\n', '') for line in f.readlines()]
f.close()

short_rope = Rope()
long_rope = Rope(10)

for move in moves:
    direction, steps = move.split()
    for _ in range(int(steps)):
        short_rope.make_move(direction)
        long_rope.make_move(direction)

print('task 1:', len(short_rope.visited_tail))
print('task 2:', len(long_rope.visited_tail))
