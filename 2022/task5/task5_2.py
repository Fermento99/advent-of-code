f_i = open('./input_instructions.txt', 'r')
instructions = f_i.readlines()
f_i.close()

f_s = open('./input_stacks.txt', 'r')
stacks = f_s.readlines()
f_s.close()

def make_move(crates, start_stack, end_stack):
    stacks[end_stack-1].extend(stacks[start_stack-1][-crates:])
    stacks[start_stack-1] = stacks[start_stack-1][0:-crates]

stacks = [stack.replace('\n', '').split(',') for stack in stacks]

for instruction in instructions:
    parts = instruction.replace('\n', '').split(' ')
    make_move(int(parts[1]), int(parts[3]), int(parts[5]))

for stack in stacks:
    print('{}'.format(stack[-1]), end='')
print()