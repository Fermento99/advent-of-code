from Rock import Rock
from Queue import Queue


class Chamber:
    width = 7

    def __init__(self, rock_queue: Queue, jet_queue: Queue) -> None:
        self.rock_queue = rock_queue
        self.jet_queue = jet_queue
        self.reocurrence_step = len(jet_queue) * len(rock_queue)
        self.reoccurence_list = []
        self.chamber_height = 4
        self.chamber = [['.' for _ in range(Chamber.width)] for _ in range(self.chamber_height)]
        self.max_height = -1
        self.count_rocks = 0
        self.flying_rock = False

    def __repr__(self) -> str:
        out = ''
        for row in self.chamber[-1:-50:-1]:
            out += (''.join(row))
            out += '\n'
        return out

    def new_rock(self):
        rock_type = self.rock_queue.next_el()
        rock_start_position = (2, self.max_height + 3)
        self.rock = Rock(rock_type, rock_start_position)
        if self.rock.position[1] >= self.chamber_height:
            diff = self.rock.position[1] - self.chamber_height + 1
            self.chamber.extend([['.' for _ in range(Chamber.width)] for _ in range(diff)])
            self.chamber_height += diff
        self.flying_rock = True

    def move_rock_horizontal(self):
        jet = self.jet_queue.next_el()
        if jet == '>':
            for point in self.rock.check_right():
                if point[0] >= Chamber.width or self.chamber[point[1]][point[0]] != '.': return
            self.rock.move_right()
        elif jet == '<':
            for point in self.rock.check_left():
                if point[0] < 0 or self.chamber[point[1]][point[0]] != '.': return
            self.rock.move_left()
    
    def move_rock_vertical(self):
        for point in self.rock.check_bottom():
            if point[1] < 0 or self.chamber[point[1]][point[0]] != '.':
                for sattle_point in self.rock.sattle():
                    self.chamber[sattle_point[1]][sattle_point[0]] = '#'
                self.max_height = self.max_height if self.max_height > self.rock.position[1] else self.rock.position[1]
                self.flying_rock = False
                self.count_rocks += 1
                return
        self.rock.move_down()
        
    
    def loop(self):
        while self.flying_rock:
            self.move_rock_horizontal()
            self.move_rock_vertical()

    def check_reoccurence(self, new_position:str):
        if new_position.find('#'*Chamber.width) != -1:
            print('zero ground discovered {}'.format(len(self.reoccurence_list)))
            return True
        for saved in self.reoccurence_list:
            if saved == new_position:
                print('ladies and gentleman, we got \'em, {} {}'.format(self.reoccurence_list.index(saved), len(self.reoccurence_list)))
                self.reoccurence_list.append(new_position)
                return True 
            
        self.reoccurence_list.append(new_position)
        return False


    def loop_until(self, rock_count):
        while self.count_rocks < rock_count:
            self.new_rock()
            self.loop()
            if (self.count_rocks % self.reocurrence_step == 0):
                if self.check_reoccurence(self.__repr__()):
                    break
            if self.count_rocks % 10000 == 0:
                print("{:f}%".format(self.count_rocks*100/rock_count), end='\r')