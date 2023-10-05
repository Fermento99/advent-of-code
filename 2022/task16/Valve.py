class Valve:
    def __init__(self, line:str) -> None:
        config = line.replace('Valve ', '').replace(' has flow rate=', ';').replace(" tunnels lead to valves ", '').replace(" tunnel leads to valve ", '').replace('\n', '')
        name, rate, valves = config.split(';')
        self.name = name
        self.rate = int(rate) 
        self.neighbours = valves.split(', ')
        self.open = False

    def __repr__(self) -> str:
        return 'Valve {} with rate {} is {}; neighbours: {}'.format(self.name, self.rate, 'open' if self.open else 'closed', self.neighbours)