f = open('./input.txt', 'r')
strategy = f.readlines()
f.close()

strategy = [e.split() for e in strategy]
score = 0
result_points = {'X': 0, 'Y':3, 'Z':6}

for round in strategy:
    o, r = round
    score += result_points[r]
    if o == 'A':
        if r == 'X': score += 3
        elif r == 'Y': score += 1
        else: score += 2
    elif o == 'B':
        if r == 'X': score += 1
        elif r == 'Y': score += 2
        else: score += 3
    else:
        if r == 'X': score += 2
        elif r == 'Y': score += 3
        else: score += 1

print(score)