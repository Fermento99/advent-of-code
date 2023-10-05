from Monkey import Monkey


f = open('./input.txt')
monkey_lines = [line.replace('\n','') for line in f.readlines()]
f.close()

for line in monkey_lines:
    Monkey(line)

f = Monkey.monkies['root'].evaluate_b()

x = 0
while f(x) != 0:
    a = f(x+1) - f(x)
    b = f(x) - (a*x)
    x = -b/a

print(x)
