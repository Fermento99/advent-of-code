f = open('./input.txt', 'r')
packets = [line.replace('\n', '') for line in f.readlines()]
f.close()

packets = [eval(packet) for packet in packets if len(packet) > 0]
packets.extend([[[2]],[[6]]])

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

def bubble_sort(array):
    for cycle in range(len(array)):
        switched = False
        for i in range(len(array)-1-cycle):
            if compare(array[i], array[i+1]) < 0:
                array[i], array[i+1] = array[i+1], array[i]
                switched = True
        if not switched: return

bubble_sort(packets)
signal = (packets.index([[2]])+1) * (packets.index([[6]])+1)
print(signal)