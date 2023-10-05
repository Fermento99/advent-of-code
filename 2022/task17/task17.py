from Queue import Queue
from Chamber import Chamber


f = open('./known_input.txt')
jets = f.readline()
f.close()

jets_list = list(jets.replace('\n', ''))

print(len(jets))

jets_queue = Queue(jets_list)
rocks_queue = Queue(['h', 'p', 'l', 'v', 'b'])

chamber = Chamber(rocks_queue, jets_queue)

chamber.loop_until(1000000000000)

# for state in chamber.reoccurence_list:
    # print(state)

print(len(chamber.reoccurence_list))
