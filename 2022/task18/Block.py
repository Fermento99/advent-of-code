class Block:
    def __init__(self, droplets, boundries) -> None:
        self.block = [[['.' for _ in boundries[2]] for _ in boundries[1]] for _ in boundries[0]]
        self.boundries = boundries
        for droplet in droplets:
            self.block[droplet.coords[0]][droplet.coords[1]][droplet.coords[2]] = droplet

    def check_bubbles(self):
        diff = 1
        while diff > 0:
            diff = 0
            for x in self.boundries[0]:
                for y in self.boundries[1]:
                    for z in self.boundries[2]:
                        if self.block[x][y][z] == '.':
                            if x - 1 not in self.boundries[0] :
                                self.block[x][y][z] = 's'
                                if self.block[x - 1][y][z] == 's':
                                    pass
                                    
    def edge_air(self, x, y, z):
        pass