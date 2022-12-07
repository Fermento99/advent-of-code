f = open('./input.txt', 'r')
commands = f.readlines()
f.close()

os = {'/':[0, {}]}
pwd = ['/']
ls = []
ls_mode = False
sizes = []

def get_deep(dictionary, keys):
    for key in keys:
        dictionary = dictionary[key][1]
    return dictionary

def comm_cd(value, pwd):
    if value == '..':
        pwd.pop()
    elif value == '/':
        pwd = ['/']
    else:
        pwd.append(value)
    return pwd

def comm_ls(content, os, pwd):
    for line in content:
        size, name = line.split(' ')
        if size == 'dir':
            get_deep(os, pwd)[name] = [0, {}]
        else:
            get_deep(os, pwd)[name] = [size, {}]
    return os

def calculate_size(dir):
    if len(dir[1]) > 0:
        for sub_dir in dir[1]:
            dir[0] += int(calculate_size(dir[1][sub_dir]))
        sizes.append(dir[0])
    return dir[0]
 


for command in commands:
    command = command.replace('\n', '')
    line = command.split(' ')
    if line[0] != '$':
        if ls_mode:
            ls.append(command)
        else:
            print('unknown output:', command)
    else:
        if ls_mode:
            os = comm_ls(ls, os, pwd)
            ls = []
            ls_mode = False
        
        if line[1] == 'cd':
            pwd = comm_cd(line[2], pwd)
        elif line[1] == 'ls':
            ls_mode = True
        else:
            print('unknown command: ', command)

if ls_mode:
    os = comm_ls(ls, os, pwd)

minimal = 30000000 - 70000000 + calculate_size(os['/'])
print('task 1:', sum(filter(lambda dir: dir < 100000, sizes)))
print('task 2:', min(filter(lambda dir: dir > minimal, sizes)))
