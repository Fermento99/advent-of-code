f = open('./input.txt', 'r')
numbers = [line.replace('\n', '') for line in f.readlines()]
f.close()

def parse_SNAFU(digit: str) -> int:
    if digit == '=': return -2
    if digit == '-': return -1
    if digit == '0': return 0
    if digit == '1': return 1
    if digit == '2': return 2

def parse_int(digit: int) -> str:
    if digit == 0: return "0" 
    if digit == 1: return "1" 
    if digit == 2: return "2"
    if digit == 3: return "="
    if digit == 4: return "-"

def encrypt(number: int) -> str:
    power = 0
    out = ''
    while number != 0:
        digit = number % 5
        out += parse_int(digit)
        if digit > 2:
            number += 5
        number = (number // 5)
        power += 1
    return out[-1::-1]

def decrypt(number: str) -> int:
    power = 0
    out = 0
    for digit in reversed(number):
        out += (5**power) * parse_SNAFU(digit)
        power += 1
    return out


int_sum = sum([decrypt(number) for number in numbers])
print(encrypt(int_sum))