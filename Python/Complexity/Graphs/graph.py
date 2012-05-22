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

    def add_regular_edges(self, degree):
        vertices = self.vertices()
        n = len(vertices)
        if degree >= n or (degree%2 and n%2):
            raise StandardError('Impossible to build regular graph')
        m = degree/2 if not degree%2 else (degree - 1)/2
        for i in range(n):
            self.add_edge(Edge(vertices[i], vertices[(i+1)%len(vertices)]))
            for j in range(1, m+1):
                self.add_edge(Edge(vertices[i], vertices[(i+j)%len(vertices)]))
                self.add_edge(Edge(vertices[i], vertices[(i-j)%len(vertices)]))
            if not n%2 and degree%2:
                self.add_edge(Edge(vertices[i], vertices[(i+n/2)%len(vertices)]))



