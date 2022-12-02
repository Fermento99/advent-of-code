f = open('./input.txt', 'r')
strategy = f.readlines()
f.close()

strategy = [e.split() for e in strategy]
score = 0
figure_points = {'X': 1, 'Y':2, 'Z':3}
translate_o = {'A':'X', 'B':'Y', 'C':'Z'}

for round in strategy:
    o, m = round
    score += figure_points[m]
    if translate_o[o] == m: score += 3
    elif ((o =='A' and m == 'Y') or
        (o =='B' and m == 'Z') or
        (o =='C' and m == 'X')): score += 6

print(score)