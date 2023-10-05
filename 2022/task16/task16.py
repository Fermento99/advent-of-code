from Valve import Valve
from Scenario import Scenario
        

f = open('./input.txt')
valve_lines = f.readlines()
f.close()

valves = {}

for line in valve_lines:
    valve = Valve(line)
    valves[valve.name] = valve

scenario = Scenario(valves)

max_preassure = 0
scenarios = scenario.evaluate_steps()
while len(scenarios) > 1:
    print('max_pressure', max_preassure)
    new_scenarios = []
    for scenario in scenarios:
        if scenario.pressure_relieved > max_preassure: max_preassure = scenario.pressure_relieved
        new_scenarios.extend(scenario.evaluate_steps())
    print('scenarios len', len(new_scenarios))
    scenarios = sorted(new_scenarios, key=lambda scenario: scenario.pressure_relieved)[-1000:]
print(max_preassure)