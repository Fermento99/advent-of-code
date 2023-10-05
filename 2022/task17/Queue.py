class Queue:
    def __init__(self, values: list) -> None:
        self.queue = values
        self.iterator = 0
        self.end = len(values)

    def next_el(self):
        out = self.queue[self.iterator]
        self.iterator += 1
        if self.iterator == self.end:
            self.iterator = 0
        return out
    
    def __len__(self):
        return self.end