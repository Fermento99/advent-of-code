f = open('./input.txt', 'r')
pairs = f.readlines()
f.close()

count = 0

for pair in pairs:
    pair = pair[:-1]
    e1, e2 = pair.split(',')
    e1_start, e1_end = e1.split('-')
    e2_start, e2_end = e2.split('-')
    if ((int(e1_start) <= int(e2_start) and int(e1_end) >= int(e2_end)) 
        or (int(e2_start) <= int(e1_start) and int(e2_end) >= int(e1_end))):
        count += 1

print(count)