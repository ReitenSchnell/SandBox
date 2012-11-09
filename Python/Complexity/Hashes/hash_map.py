from better_map import BetterMap

class HashMap:
    def __init__(self):
        self.maps = BetterMap(2)
        self.num = 0

    def get(self, k):
        return self.maps.get(k)

    def add(self, k, v):
        if self.num == len(self.maps):
            self.resize()
        self.maps.add(k,v)
        self.num += 1

    def resize(self):
        new_maps = BetterMap(self.num * 2)
        for k, v in self.maps.iteritems():
            new_maps.add(k,v)


def test():
    map = HashMap()
    map.add("1", "a")
    map.add("2", "b")
    map.add("3", "c")
    map.add("4", "d")
    t = map.get("4")
    print t

test()




