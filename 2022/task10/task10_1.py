f = open('./input.txt', 'r')
operations = [line.replace('\n', '') for line in f.readlines()]
f.close()

curr_cycle = 1
signal_cycle = 20
signal_sum = 0
x = 1

for operation in operations:
    if operation == 'noop':
        curr_cycle += 1
        if curr_cycle == signal_cycle:
            signal_sum == signal_cycle * x
            signal_cycle += 40
    else:
        curr_cycle += 2
        if curr_cycle > signal_cycle:
            signal_sum += signal_cycle * x
            signal_cycle += 40
        x += int(operation.split()[1])
        if curr_cycle == signal_cycle:
            signal_sum += signal_cycle * x
            signal_cycle += 40


print(signal_sum)