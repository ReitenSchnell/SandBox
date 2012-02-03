import random
import string
from unittest import TestCase

class StringCalculator:
    def Add(self, number):
        if not number:
            return 0
        separator = '\n'
        if number.startswith('//'):
            separator = number[2]
            number = number[4:]
        number = string.replace(number, separator, ',')
        numbers = [int(val) for val in string.split(number, ',')]
        negatives = filter(lambda x: x<0, numbers)
        if negatives:
            raise Exception('negatives not allowed')
        return sum(numbers)

class StringCalculatorTests(TestCase):
    def setUp(self):
        self.calculator = StringCalculator()

    def test_empty_string_should_return_0(self):
        result = self.calculator.Add('')
        self.assertEqual(0, result)

    def test_one_number_should_return_this_number(self):
        result = self.calculator.Add('1')
        self.assertEqual(1,result)

    def test_two_numbers_should_return_sum(self):
        result = self.calculator.Add('1,2')
        self.assertEqual(3, result)

    def test_unknown_amount_of_numbers_should_return_sum(self):
        values = []
        for val in range(0, random.randint(0, 100)):
            values.append(random.randint(0, 100))
        expected = sum(values)
        number = string.join([str(val) for val in values], ',')
        result = self.calculator.Add(number)
        self.assertEqual(expected, result)

    def test_new_line_separator_returns_sum(self):
        result = self.calculator.Add('1\n2,3')
        self.assertEqual(6, result)

    def test_one_char_separator_returns_sum(self):
        result = self.calculator.Add('//;\n2;3;4')
        self.assertEqual(9, result)

    def test_negatives_should_raise_exception(self):
        self.assertRaises(Exception, self.calculator.Add('2,-5,3,-4'))
        

        





  