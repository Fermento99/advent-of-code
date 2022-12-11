from Monkey import Monkey

f = open('./input.txt', 'r')
monkeys_config = [line.replace('\n', '') for line in f.readlines()]
f.close()

def get_value(line): return line.split(':')[1]

for monkey_id in range((len(monkeys_config)+1)//7):
    start = monkey_id * 7
    args = {}
    args['starting_items'] = get_value(monkeys_config[start+1])
    args['operation'] = get_value(monkeys_config[start+2])
    args['test'] = get_value(monkeys_config[start+3])
    args['monkey_true'] = get_value(monkeys_config[start+4])
    args['monkey_false'] = get_value(monkeys_config[start+5])
    Monkey(**args)

for round in range(20):
    for monkey in Monkey.monkeys:
        monkey.round()

inspections = [monkey.inspection_count for monkey in Monkey.monkeys]
top_2, top_1 = sorted(inspections)[-2:]
print('monkey buisness level:', top_1 * top_2)