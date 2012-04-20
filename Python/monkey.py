from unittest import TestCase

class Coconuts:
    def find_original(self):
        result = -1
        for i in range(2,20000):
          m1 = (i - 1) % 5
          m2 = not (4 * (i - 1) / 5 - 1) % 5
          m3 = not (4 * (4 * (i - 1) / 5 - 1) / 5 - 1) % 5
          m4 = not (4 * (4 * (4 * (i - 1) / 5 - 1) / 5 - 1) / 5 - 1) % 5
          m5 = not (4*(4 * (4 * (4 * (i - 1) / 5 - 1) / 5 - 1) / 5 - 1)/5 - 1) % 5
          m6 = not (4*(4*(4 * (4 * (4 * (i - 1) / 5 - 1) / 5 - 1) / 5 - 1)/5 - 1)/5 -1) % 5
          if not m1 and m2 and m3 and m4 and m5 and m6:
              result = i
              break
        return result

class CoconutsTest(TestCase):
    def test_original(self):
        result = Coconuts().find_original()
        print result


  