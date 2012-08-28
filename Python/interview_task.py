from collections import defaultdict
import random
import string
from unittest import TestCase
import math
import uuid

def generate_mess(str, size):
    known_chars = list(string.lower(str)+string.upper(str))
    other_chars = set(string.printable) - set(string.whitespace) - set(known_chars)
    size_of_piece = size/len(str)
    mess = []
    for i in range(len(str)):
        random_piece = [random.choice(list(other_chars)) for j in range(random.randint(1, size_of_piece))]
        mess += random_piece + [str[i]]
    tail_len = size - len(mess)
    fill_chars = [random.choice(list(other_chars)) for j in range(tail_len)]
    divider = random.randint(0, tail_len - 1)
    result = fill_chars[0:divider] + mess + fill_chars[divider:]
    return string.join(result, '')

def generate_names(size):
    names = ['a','b','c']
    lst = []
    for n in names:
        for i in range(size):
            age = i + 10
            name = n
            guid = uuid.uuid4()
            lst.append("%s, %s, %s"%(name, age, guid))
    random.shuffle(lst)
    f = open('names.txt', 'w')
    f.write("\n".join(lst))
    f.close()

class Tests(TestCase):
    def test_mess_should_contain_str(self):
        str = 'https://raw.github.com/gist/4652672a7d80c979c831/193761d5d89fd87d3c145c1f09325d0426ea10c6/names'
        result = generate_mess(str, 50000)

        char_dict = defaultdict(int)
        for ch in result:
            char_dict[ch] += 1
        mean = sum(char_dict.values())/len(char_dict)
        std_dev = math.sqrt(sum((v - mean)**2 for v in char_dict.values())/len(char_dict))
        rare_chars = [ch for ch in char_dict if char_dict[ch] < mean - std_dev]
        link = [ch for ch in result if ch in rare_chars]

        self.assertEqual(str, string.join(link, ''))

    def test_mess_should_be_size_of_size(self):
        str = 'some strange link'
        size = 1000
        result = generate_mess(str, size)
        self.assertEqual(size, len(result))

    def _test_names(self):
        f = open('names.txt', 'r')
        lines = f.readlines()
        f.close()
        names = [line.split(', ') for line in lines]
        sorted_list = sorted(names, key=lambda x: (x[0], -int(x[1])))
        for val in sorted_list:
            print val
        print sorted_list[12][2]

