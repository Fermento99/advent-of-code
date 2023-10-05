from Droplet import Droplet
from Block import Block


f = open('./input.txt')
droplet_lines = [line.replace('\n','') for line in f.readlines()]
f.close()

droplets = [Droplet(line) for line in droplet_lines]

boundries = []
for i in range(3):
    coord_list = [d.coords[i] for d in droplets]
    boundries.append((min(coord_list), max(coord_list)))

boundries = [range(*b) for b in boundries]

# for i in range(len(droplets)):
#     undone = round((len(droplets)-i)/len(droplets) * 40)
#     done = 40 - undone
#     print('{}{}'.format(done*'#', undone*'_'), end='\r')
#     for j in range(i+1, len(droplets)):
#         if droplets[i].exposed_sides == 0:
#             break
#         droplets[i].check_neighbour(droplets[j])

# block = Block(droplets, boundries)
# block.check_bubbles()
print(boundries)


print('#'*40)
out = sum([d.exposed_sides for d in droplets])
print(out)