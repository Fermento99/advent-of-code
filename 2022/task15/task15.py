from Sensor import Sensor
from Map import Map

f = open('./input.txt', 'r')
sensors = [line.replace('\n', '') for line in f.readlines()]
f.close()

sensors = [Sensor(sensor.replace('x=', '').replace('y=', '').replace('Sensor at', '').replace('closest beacon is at', ''), 2000000) for sensor in sensors ]

map = Map(Sensor.min_x, Sensor.max_x, Sensor.min_y, Sensor.max_y, 2000000)
print('map initiated')

for index, sensor in enumerate(sensors):
    print('index', index, end='\r')
    covered_points = sensor.cover()
    for point in covered_points:
        map.set_point(point, '#')

for sensor in sensors:
    map.set_point(sensor.sensor, 'S')
    map.set_point(sensor.beacon, 'B')


row = map.get_row(2000000)
count = len(list(filter(lambda x: x == '#', row)))
print('Not possible places in row 2000000')
print(count)

