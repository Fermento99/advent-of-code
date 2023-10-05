from CircularFile import CircularFile 


f = open('./input.txt')
numbers = [int(line.replace('\n','')) for line in f.readlines()]
f. close()

file = CircularFile(numbers)
for i in range(10):
    file.mix()
    print('mix {}'.format(i+1), end ='\r')
print('                      ')

print(file.get(1000), file.get(2000), file.get(3000))
print(file.get(1000) + file.get(2000) + file.get(3000))