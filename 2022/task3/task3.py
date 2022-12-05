def compute_priorities(items):
    sum = 0
    for i in items:
        if i.islower():
            sum += ord(i) - 96
        else:
            sum += ord(i) - 38
    return sum

f = open('./input.txt', 'r')
rucksacks = f.readlines()
f.close()

# task 1
compartments = [(rucksack[:len(rucksack)//2], rucksack[len(rucksack)//2:-1]) for rucksack in rucksacks]
errors = []

for c1, c2 in compartments:
    for i in c1:
        if c2.find(i) != -1:
            errors.append(i)
            break

# task 2
badges = []
for i in range(0, len(rucksacks), 3):
    for item in rucksacks[i][0:-1]:
        if rucksacks[i+1].find(item) != -1 and rucksacks[i+2].find(item) != -1:
            badges.append(item)
            break


print('task 1:', compute_priorities(errors))
print('task 2:', compute_priorities(badges))