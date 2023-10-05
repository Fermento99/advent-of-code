

class Droplet:
    def __init__(self, coords:str) -> None:
        self.coords = [int(coord) for coord in coords.split(',')]
        self.exposed_sides = 6
    
    def dist(self, other):
        dist = 0
        for i in range(len(self.coords)):
            dist += abs(self.coords[i] - other.coords[i])
        return dist
    
    def cover_side(self):
        self.exposed_sides -= 1
    
    def check_neighbour(self, other):
        if self.dist(other) == 1:
            self.cover_side()
            other.cover_side()
            return True
        return False