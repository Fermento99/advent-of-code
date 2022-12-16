class Sensor:
    max_x = float('-inf')
    max_y = float('-inf')
    min_x = float('inf')
    min_y = float('inf')

    def __init__(self, config: str, row: int):
        sensor, beacon = config.split(':')
        s_x, s_y = [int(coord) for coord in sensor.split(',')]
        b_x, b_y = [int(coord) for coord in beacon.split(',')]
        self.sensor = (s_x, s_y)
        self.beacon = (b_x, b_y)
        self.radius = abs(s_x - b_x) + abs(s_y - b_y) + 1
        self.row = row
        
        self.update_boundries()
    
    def update_boundries(self):
        if self.sensor[0] + self.radius > Sensor.max_x: 
            Sensor.max_x = self.sensor[0] + self.radius
        if self.sensor[0] - self.radius< Sensor.min_x:
            Sensor.min_x = self.sensor[0] - self.radius 
        
        if self.sensor[1] + self.radius > Sensor.max_y:
            Sensor.max_y = self.sensor[1] + self.radius
        if self.sensor[1] - self.radius < Sensor.min_y:
            Sensor.min_y = self.sensor[1] - self.radius
        
        if self.beacon[0] > Sensor.max_x: 
            Sensor.max_x = self.beacon[0]
        if self.beacon[0] < Sensor.min_x:
            Sensor.min_x = self.beacon[0]
        
        if self.sensor[1] > Sensor.max_y:
            Sensor.max_y = self.sensor[1]
        if self.sensor[1] < Sensor.min_y:
            Sensor.min_y = self.sensor[1]
    
    def distance(self, point: list):
        return abs(self.sensor[0] - point[0]) + abs(self.sensor[1] - point[1])
    
    def check_point_in_range(self, point: list) -> bool:
        return self.distance(point) <= self.radius and self.beacon[0] != point[0] and self.beacon[1] != point[1]

    def cover(self):
        points = []
        diff = self.sensor[1] - self.row
        if 0 < diff <= self.radius:
            points.append([self.sensor[0], self.sensor[1]-diff])
            for j in range(self.radius - diff):
                points.append([self.sensor[0]+j, self.sensor[1]-diff])
                points.append([self.sensor[0]-j, self.sensor[1]-diff])
        elif 0 > diff >= -self.radius:
            points.append([self.sensor[0], self.sensor[1]-diff])
            for j in range(self.radius + diff):
                points.append([self.sensor[0]-j, self.sensor[1]-diff])
                points.append([self.sensor[0]+j, self.sensor[1]-diff])
        return points