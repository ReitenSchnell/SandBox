import random
import re
import string
from unittest import TestCase

class StringCalculator:
   def add(self, number):
       if number.startswith('//'):
           delimiters = re.findall(r"//\[(.+)\]\n", number)
           number = re.findall(r"//\[.+\]\n(.+)", number)[0]
           for delimiter in delimiters:
               number = number.replace(delimiter, ',')
       pattern = r"(\-?\d+)[,|\n]?"
       result = re.findall(pattern, number)
       values = [int(val) for val in result if int(val) <= 1000]
       negatives = [val for val in values if val<0]
       if negatives:
           raise StandardError('negatives are not allowed: %s'%(string.join([str(val) for val in negatives], ', ')))
       return sum(values)

class StringCalculatorTests(TestCase):
    def setUp(self):
        self.calc = StringCalculator()

    def test_add_empty_string_should_return_0(self):
        result = self.calc.add('')
        self.assertEqual(0, result)

    def test_one_value_returns_this_int_value(self):
        result = self.calc.add('2')
        self.assertEqual(2, result)

    def test_two_values_with_comma_sep_returns_sum(self):
        result = self.calc.add('2,3')
        self.assertEqual(5, result)

    def test_unknown_amount_of_values_return_sum(self):
        values = [random.randint(0,100) for val in range(0, random.randint(0,100))]
        str_values = string.join([str(val) for val in values], ',')
        result = self.calc.add(str_values)
        self.assertEqual(sum(values), result)

    def test_newline_separator_returns_sum(self):
        result = self.calc.add('2\n3,1')
        self.assertEqual(6, result)

    def test_negatives_inNumber_should_throw_error(self):
        self.assertRaises(StandardError, self.calc.add, '2,-4,5-6')

    def test_negatives_inNumber_error_message_should_contain_negatives(self):
        try:
            self.calc.add('2,-3,4,-5')
        except Exception as ex:
            message = ex.args[0]
        self.assertEqual('negatives are not allowed: -3, -5', message)

    def test_different_delimiters_should_return_sum(self):
        result = self.calc.add('//[***]\n2***3***4')
        self.assertEqual(result, 9)

    def test_numbers_bigger_then_1000_should_be_ignored(self):
        result = self.calc.add('2,1001,3,1005, 1000')
        self.assertEqual(1005, result)

    def test_multiple_delimiters_should_return_sum(self):
        result = self.calc.add('//[***][;;]\n2***3;;4***1')
        self.assertEqual(result, 10)
        




         

        


            
        
        




        





  