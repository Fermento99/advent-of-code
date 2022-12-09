class Rope:
    def __init__(self, knot_count=2):
        self.knots = [[0,0] for i in range(knot_count)]
        self.visited_tail = set([(0,0)])

    def move_knot(self, index):
        dx = self.knots[index - 1][0] - self.knots[index][0]
        dy = self.knots[index - 1][1] - self.knots[index][1]

        if abs(dx) < 2 and abs(dy) < 2:
            return 
        
        dd = dx * dy
        if dd > 0:
            if dx > 0:
                self.knots[index][0] += 1
                self.knots[index][1] += 1
            else:
                self.knots[index][0] -= 1
                self.knots[index][1] -= 1
        elif dd < 0:
            if dx > 0:
                self.knots[index][0] += 1
                self.knots[index][1] -= 1
            else:
                self.knots[index][0] -= 1
                self.knots[index][1] += 1
        else:
            if dx > 0:
                self.knots[index][0] += 1
            elif dx < 0:
                self.knots[index][0] -= 1
            elif dy > 0:
                self.knots[index][1] += 1
            else:
                self.knots[index][1] -= 1
        
        if index == len(self.knots) - 1:
            self.visited_tail.add(tuple(self.knots[index]))
        else:
            self.move_knot(index + 1)

    def make_move(self, direction):
        if direction == 'U': self.move_up()
        elif direction == 'D': self.move_down()
        elif direction == 'R': self.move_right()
        elif direction == 'L': self.move_left()

    def move_up(self):
        self.knots[0][1] += 1
        self.move_knot(1)
    
    def move_down(self):
        self.knots[0][1] -= 1
        self.move_knot(1)
    
    def move_right(self):
        self.knots[0][0] += 1
        self.move_knot(1)
    
    def move_left(self):
        self.knots[0][0] -= 1
        self.move_knot(1)
