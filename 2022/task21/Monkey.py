class Monkey:
    monkies = {}

    def __init__(self, config: str) -> None:
        self.name, self.value = config.split(':')
        Monkey.monkies[self.name] = self
    
    def evaluate_a(self):
        try:
            return int(self.value)
        except:
            m1 = self.value[1:5]
            m2 = self.value[8:12]
            op = self.value[6]
            m1v = Monkey.monkies[m1].evaluate_a()
            m2v = Monkey.monkies[m2].evaluate_a()
            return eval("{} {} {}".format(m1v, op, m2v))
    
    def evaluate_b(self):
        if self.name == 'humn':
            return 'x'
        try:
            return int(self.value)
        except:
            m1 = self.value[1:5]
            m2 = self.value[8:12]
            op = self.value[6]
            m1v = Monkey.monkies[m1].evaluate_b()
            m2v = Monkey.monkies[m2].evaluate_b()
            
            if self.name == 'root':
                def f(x):
                    x = x
                    if type(m1v) == str:
                        return eval('{} - {}'.format(m1v, m2v))
                    else:
                        return eval('{} - {}'.format(m2v, m1v))
                return f
            
            value = "({} {} {})".format(m1v, op, m2v)
            
            if type(m1v) == str or type(m2v) == str:
                return value
            return eval(value)
            