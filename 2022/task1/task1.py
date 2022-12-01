f = open('./input.txt', 'r')
line = f.readline()
calories = [0]
while line:
    if line == '\n':
        calories.append(0)
    else:
        calories[-1] += int(line)
    line = f.readline()
f.close()

sortet_calories = sorted(calories, reverse=True)
print('task 1:', sortet_calories[0])
print('task 2:', sum(sortet_calories[0:3]))
