f = open('./input.txt', 'r')
stream = f.readlines()[0]
f.close()

pointer = -1
found = False
while not found:
    pointer += 1
    found = True
    for i in range(14):
        for j in range(i+1, 14):
            if stream[pointer + i] == stream[pointer + j]:
                found = False
                break

print(pointer+ 14)