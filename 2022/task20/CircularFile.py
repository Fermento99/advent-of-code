class CircularFile:
    def __init__(self, numbers) -> None:
        self.length = len(numbers)
        self.content = [0 for _ in range(len(numbers))]
        self.start_index = 0
        for i in range(self.length):
            self.content[i] = { 'value': numbers[i] * 811589153, 'original': i }
            if numbers[i] == 0:
                self.start_index = i
        
    def mix_number(self, curr_index):
        entry = list(filter(lambda entry: entry['original'] == curr_index ,self.content))[0]
        index = self.content.index(entry)

        new_index = index + entry['value']
        
        self.content.pop(index)
        self.content.insert(new_index % (self.length-1), entry)

    def mix(self):
        for i in range(self.length):
            self.mix_number(i)
        
        for i in range(self.length):
            if self.content[i]['value'] == 0:
                self.start_index = i
        
    
    def get(self, index):
        return self.content[(self.start_index + index) % self.length]['value']
    
    def __repr__(self) -> str:
        return ', '.join([str(number['value']) for number in self.content])