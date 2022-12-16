class Map:
    def __init__(self, min_x, max_x, min_y, max_y, row) -> None:
        self.min_x = min_x
        self.max_x = max_x
        self.min_y = min_y
        self.max_y = max_y
        self.row = row
        self.state = ['.' for _ in range(max_x - min_x)]
    
    def set_point(self, point: list, val):
        if point[1] == self.row:
            self.state[point[0]-self.min_x] = val
    
    def get_row(self, row):
        if row == self.row:
            return self.state
    
    def print(self):
        for row in self.state:
            print(''.join(row))