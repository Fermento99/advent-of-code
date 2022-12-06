f = open('./input.txt', 'r')
stream = f.readlines()[0]
f.close()

pointer = -1
found = False
while not found:
    found = True
    pointer += 1
    for i in range(4):
        for j in range(i+1, 4):
            if stream[pointer + i] == stream[pointer + j]:
                found = False
                break

print(pointer + 4)