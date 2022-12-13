f = open('./input.txt', 'r')
packets = [line.replace('\n', '') for line in f.readlines()]
f.close()

packets = [eval(packet) for packet in packets if len(packet) > 0]

def compare(left, right):
    if type(left) == int and type(right) == int:
        return right - left
    elif type(left) == int:
        return compare([left], right)
    elif type(right) == int:
        return compare(left, [right])
    else:
        i = 0
        out = 0
        while out == 0:
            if len(left) > i and len(right) > i:
                out = compare(left[i], right[i])
            elif len(left) == len(right):
                return 0
            elif len(left) < len(right):
                return 1
            else:
                return -1
            i += 1

        return out

sum = 0

for i in range(len(packets)//2):
    index = i * 2
    if compare(packets[index], packets[index+1]) >= 0:
        sum += i+1

print(sum)