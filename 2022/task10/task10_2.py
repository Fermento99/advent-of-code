f = open('./input.txt', 'r')
operations = [line.replace('\n', '') for line in f.readlines()]
f.close()

def display_crt(crt, height, width):
    for y in range(height):
        for x in range(width):
            print(crt[y*width + x], end='')
        print()

height = 6
width = 40

crt = ['.' for _ in range(height * width)]
operation = ''
wait = 0
v = 0
x = 1

for pixel in range(height * width):
    if wait == 0:
        operation = operations.pop(0)
        x += v
        if operation == 'noop':
            v = 0
            wait = 1
        else:
            v = int(operation.split()[1])
            wait = 2
    
    if pixel % width >= x - 1 and pixel % width <= x + 1:
        crt[pixel] = '#'
    
    wait -= 1

display_crt(crt, height, width)