import random
import string
from unittest import TestCase

class StringCalculator:
    def Add(self, number):
        if not number: return 0
        default_separator = ','
        separator = '\n'
        if len(number)>2 and  number[0] == number[1] == '/':
            separator = number[2]
            number = number[4:]
        if separator in number:
            number = string.replace(number, separator, default_separator)
        return sum([int(val) for val in string.split(number,default_separator)])

class StringCalculatorTests(TestCase):
    def setUp(self):
        self.calculator = StringCalculator()

    def test_empty_string_returns_0(self):
        result = self.calculator.Add('')
        self.assertEqual(0, result)

    def test_one_value_returns_this_value(self):
        result = self.calculator.Add('2')
        self.assertEqual(2, result)

    def test_two_values_returns_their_sum(self):
        result = self.calculator.Add('2,3')
        self.assertEqual(5, result)

    def test_unknown_amount_of_numbers_returns_sum(self):
        numbers = [random.randint(0,100) for val in range(0, random.randint(0,100))]
        str_value = string.join([str(val) for val in numbers], ',')
        result = self.calculator.Add(str_value)
        self.assertEqual(sum(numbers), result)

    def test_new_line_separator_returns_sum(self):
        result = self.calculator.Add('2\n3,4')
        self.assertEqual(9, result)

    def test_one_char_free_delimiter_returns_sum(self):
        result = self.calculator.Add('//;\n2;3;5')
        self.assertEqual(10, result)

    def test_negatives_in_number_throws(self):
        self.assertRaises(Exception, self.calculator.Add, '2,3,-5,-6')
        
        




        





  