import itertools

class Vertex(object):
    def __init__(self, label = ''):
        self.label = label

    def __repr__(self):
        return 'Vertex(%s)'% repr(self.label)

    __str__ = __repr__

class Edge(tuple):
    def __new__(cls, e1, e2):
        return tuple.__new__(cls, (e1, e2))

    def __repr__(self):
        return 'Edge(%s, %s)'% (repr(self[0]), repr(self[1]))

    __str__ = __repr__

class Graph(dict):
    def __init__(self, vs = [], es = []):
        for v in vs:
            self.add_vertex(v)
        for e in es:
            self.add_edge(e)

    def add_vertex(self, v):
        self[v] = {}

    def add_edge(self, e):
        v, w = e
        self[v][w] = e
        self[w][v] = e

    def get_edge(self, v1, v2):
        try:
            return self[v1][v2]
        except Exception :
            return None

    def remove_edge(self, e):
        for node in self.keys():
            for inner in self[node].keys():
                if e == self[node][inner]:
                    self[node].pop(inner)

    def vertices(self):
        return self.keys()

    def edges(self):
        result = []
        for node in self:
            for inner in self[node]:
                if self[node][inner] not in result:
                    result.append(self[node][inner])
        return result

    def out_vertices(self, v):
        try:
            return self[v].keys()
        except  Exception:
            return None

    def out_edges(self, v):
        try:
            return self[v].values()
        except  Exception:
            return None

    def add_all_edges(self):
        for node in self:
            for inner_node in self:
                if node != inner_node and inner_node not in self[node]:
                    self.add_edge(Edge(node, inner_node))
