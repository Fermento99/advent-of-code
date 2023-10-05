# h, p, l, v, b

class Rock:
    def __init__(self, type, position) -> None:
        self.type = type
        self.starting_position(position)

    def starting_position(self, point):
        if self.type == 'h':
            self.position = (point[0], point[1] + 1)
        elif self.type == 'b':
            self.position = (point[0], point[1] + 2)
        elif self.type == 'p' or self.type == 'l':
            self.position = (point[0], point[1] + 3)
        elif self.type == 'v':
            self.position = (point[0], point[1] + 4)

    def move_right(self):
        self.position = (self.position[0] + 1, self.position[1])
    
    def move_left(self):
        self.position = (self.position[0] - 1, self.position[1])

    def move_down(self):
        self.position = (self.position[0], self.position[1] - 1)


    def check_right(self):
        if self.type == 'h':
            return [
                (self.position[0] + 4, self.position[1]),
            ]
        elif self.type == 'p': 
            return [
                (self.position[0] + 2, self.position[1]),
                (self.position[0] + 3, self.position[1] - 1),
                (self.position[0] + 2, self.position[1] - 2),
            ]
        elif self.type == 'l':
            return [
                (self.position[0] + 3, self.position[1]),
                (self.position[0] + 3, self.position[1] - 1),
                (self.position[0] + 3, self.position[1] - 2),
            ]
        elif self.type == 'v': 
            return [
                (self.position[0] + 1, self.position[1]),
                (self.position[0] + 1, self.position[1] - 1),
                (self.position[0] + 1, self.position[1] - 2),
                (self.position[0] + 1, self.position[1] - 3),
            ]
        elif self.type == 'b':
            return [
                (self.position[0] + 2, self.position[1]),
                (self.position[0] + 2, self.position[1] - 1),
            ]
        
    def check_left(self):
        if self.type == 'h':
            return [
                (self.position[0] - 1, self.position[1]),
            ]
        elif self.type == 'p': 
            return [
                (self.position[0], self.position[1]),
                (self.position[0] - 1, self.position[1] - 1),
                (self.position[0], self.position[1] - 2),
            ]
        elif self.type == 'l':
            return [
                (self.position[0] + 1, self.position[1]),
                (self.position[0] + 1, self.position[1] - 1),
                (self.position[0] - 1, self.position[1] - 2),
            ]
        elif self.type == 'v': 
            return [
                (self.position[0] - 1, self.position[1]),
                (self.position[0] - 1, self.position[1] - 1),
                (self.position[0] - 1, self.position[1] - 2),
                (self.position[0] - 1, self.position[1] - 3),
            ]
        elif self.type == 'b':
            return [
                (self.position[0] - 1, self.position[1]),
                (self.position[0] - 1, self.position[1] - 1),
            ]

    def check_bottom(self):
        if self.type == 'h':
            return [
                (self.position[0], self.position[1] - 1),
                (self.position[0] + 1, self.position[1] - 1),
                (self.position[0] + 2, self.position[1] - 1),
                (self.position[0] + 3, self.position[1] - 1),
            ]
        elif self.type == 'p': 
            return [
                (self.position[0], self.position[1] - 2),
                (self.position[0] + 1, self.position[1] - 3),
                (self.position[0] + 2, self.position[1] - 2),
            ]
        elif self.type == 'l':
            return [
                (self.position[0], self.position[1] - 3),
                (self.position[0] + 1, self.position[1] - 3),
                (self.position[0] + 2, self.position[1] - 3),
            ]
        elif self.type == 'v': 
            return [
                (self.position[0], self.position[1] - 4),
            ]
        elif self.type == 'b':
            return [
                (self.position[0], self.position[1] - 2),
                (self.position[0] + 1, self.position[1] - 2),
            ]
    
    def sattle(self):
        if self.type == 'h':
            return [
                (self.position[0], self.position[1]),
                (self.position[0] + 1, self.position[1]),
                (self.position[0] + 2, self.position[1]),
                (self.position[0] + 3, self.position[1]),
            ]
        elif self.type == 'p': 
            return [
                (self.position[0] + 1, self.position[1]),
                (self.position[0], self.position[1] - 1),
                (self.position[0] + 1, self.position[1] - 1),
                (self.position[0] + 2, self.position[1] - 1),
                (self.position[0] + 1, self.position[1] - 2),
            ]
        elif self.type == 'l':
            return [
                (self.position[0] + 2, self.position[1]),
                (self.position[0] + 2, self.position[1] - 1),
                (self.position[0], self.position[1] - 2),
                (self.position[0] + 1, self.position[1] - 2),
                (self.position[0] + 2, self.position[1] - 2),
            ]
        elif self.type == 'v': 
            return [
                (self.position[0], self.position[1]),
                (self.position[0], self.position[1] - 1),
                (self.position[0], self.position[1] - 2),
                (self.position[0], self.position[1] - 3),
            ]
        elif self.type == 'b':
            return [
                (self.position[0], self.position[1]),
                (self.position[0] + 1, self.position[1]),
                (self.position[0], self.position[1] - 1),
                (self.position[0] + 1, self.position[1] - 1),
            ]
