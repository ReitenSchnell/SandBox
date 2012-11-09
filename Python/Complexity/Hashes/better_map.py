import linear_map

class BetterMap:
    def __init__(self, n = 100):
        self.maps = []
        for i in range(n):
            self.maps.append(linear_map.LinearMap())

    def find_map(self, k):
        index = hash(k)%len(self.maps)
        return self.maps[index]

    def add(self, k, v):
        map = self.find_map(k)
        map.add(k, v)

    def get(self, k):
        map = self.find_map(k)
        map.get(k)

    def __len__(self):
        return len(self.maps)

    def iteritems(self):
        for m in self.maps:
            for k,v in m.items:
                yield k,v


