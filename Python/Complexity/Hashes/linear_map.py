from unittest import TestCase

class LinearMap:
    def __init__(self):
        self.items = []

    def add(self, k, v):
        self.items.append((k,v))

    def get(self, key):
        for k, v in self.items:
            if key == k:
                return v
        raise KeyError

class LinearMapTests(TestCase):
    def setUp(self):
        self.map = LinearMap()

    def test_should_add_key_value_pair(self):
        key = 'key'
        value = 18

        self.map.add(key, value)

        self.assertEqual(1, len([k for k,v in self.map.items if k == key and v == value]))

    def test_should_get_value_by_key(self):
        key = 'key'
        value = 18
        self.map.add(key, value)

        result = self.map.get(key)

        self.assertEqual(value, result)

    def test_should_raise_on_get_if_key_not_exist(self):
        self.assertRaises(KeyError, self.map.get, 'key')

