from Droplet import Droplet


f = open('./input.txt')
droplet_lines = [line.replace('\n','') for line in f.readlines()]
f.close()

droplets = [Droplet(line) for line in droplet_lines]

for i in range(len(droplets)):
    print('{} out of {}'.format(i, len(droplets)), end='\r')
    for j in range(i+1, len(droplets)):
        if droplets[i].exposed_sides == 0:
            break
        droplets[i].check_neighbour(droplets[j])

print('###################################')
out = sum([d.exposed_sides for d in droplets])
print(out)