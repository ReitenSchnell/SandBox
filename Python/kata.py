import random
import string
from unittest import TestCase

class StringCalculator:
    def add(self, number):
        if not number:
            return 0
        return sum([int(val) for val in string.split(number, ',')])

class StringCalculatorTests(TestCase):
    def setUp(self):
        self.calculator = StringCalculator()

    def act(self, number):
        return self.calculator.add(number)

    def test_add_emptystring_returns0(self):
        result = self.act('')
        self.assertEqual(0, result)

    def test_add_one_value_returns_this_value(self):
        result = self.act('5')
        self.assertEqual(5, result)

    def test_add_two_values_returns_sum(self):
        result = self.act('3,5')
        self.assertEqual(8, result)

    def test_add_unknown_amount_of_values_returns_sum(self):
        values = [random.randint(1,100) for val in range(0, random.randint(1,100))]
        expected = sum(values)
        join = string.join([str(val) for val in values], ',')
        result = self.act(join)
        self.assertEqual(expected, result)
        
        





  