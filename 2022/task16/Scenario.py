from copy import deepcopy


class Scenario:
    def __init__(self, valves, current_time = 30, current_valve = 'AA', pressure_relieved=0) -> None:
        self.valves = valves
        self.current_time = current_time
        self.current_valve = current_valve
        self.pressure_relieved = pressure_relieved
        
    def evaluate_steps(self):
        queue = [(self.valves[self.current_valve], 0)]
        visited = []

        while len(queue) > 0:
            visited_names = list(map(lambda valve: valve[0], visited))
            current_valve, current_distance = queue.pop(0)
            visited.append([
                current_valve.name,
                0 if current_valve.open else (self.current_time - current_distance - 1)*current_valve.rate,
                current_distance
            ])
            for neighbour in current_valve.neighbours:
                if neighbour not in visited_names:
                    queue.append([self.valves[neighbour], current_distance + 1])
        
        visited = sorted(visited, key=lambda node: node[1], reverse=True)

        new_scenarios = []
        for node in visited:
            if node[1] > 0:
                new_valves = deepcopy(self.valves)
                new_valves[node[0]].open = True
                new_scenarios.append(Scenario(
                    new_valves,
                    self.current_time - node[2] - 1,
                    node[0],
                    node[1] + self.pressure_relieved
                ))
        return new_scenarios
    
    def __repr__(self) -> str:
        return '\nyou\'re in valve {} and have {} minutes left\nyou will releieve {} of pressure\n'.format(self.current_valve, self.current_time, self.pressure_relieved)
        
