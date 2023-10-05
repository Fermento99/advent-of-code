from Monkey import Monkey


f = open('./input.txt')
monkey_lines = [line.replace('\n','') for line in f.readlines()]
f.close()

for line in monkey_lines:
    monkey = Monkey(line)

out = Monkey.monkies['root'].evaluate_a()
print(out)
