
class Monkey:
    monkeys = []
    group_size = 1

    def __init__(self, starting_items: str, operation: str, test: str, monkey_true: str, monkey_false: str):
        self.items = [int(item) for item in starting_items.split(',')]
        self.operation = lambda old: eval(operation.split('=')[1].replace('old', str(old)))
        self.divider = int(test.split()[-1])
        self.monkey_true = int(monkey_true.split()[-1])
        self.monkey_false = int(monkey_false.split()[-1])
        self.inspection_count = 0

        Monkey.group_size *= self.divider
        Monkey.monkeys.append(self)
    
    def round(self, relief=True):
        while len(self.items) > 0:
            item = self.items.pop(0)
            item = self.operation(item)
            if relief: item //= 3
            item %= Monkey.group_size
            self.inspection_count += 1
            self.throw(item)
                
    def catch(self, item):
        self.items.append(item)
    
    def throw(self, item):
        if item % self.divider == 0:
            Monkey.monkeys[self.monkey_true].catch(item)
        else:
            Monkey.monkeys[self.monkey_false].catch(item)
