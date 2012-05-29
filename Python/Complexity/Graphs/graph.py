from unittest import TestCase

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

    def is_connected(self):
        v_list = self.vertices()
        if not len(v_list):
            return False
        frontier = [v_list[0]]
        marked = []
        while len(frontier):
            current = frontier.pop()
            marked.append(current)
            for v in self.out_vertices(current):
                if v not in marked and v not in frontier:
                    frontier.append(v)
        return len(marked) == len(v_list)

class VertexTests(TestCase):
    def arrange(self):
        self.lbl = 'lbl'
        self.vertex = Vertex(self.lbl)

    def test_label_should_be_initialised_in_constructor(self):
        self.arrange()
        self.assertEqual(self.lbl, self.vertex.label)

    def test_check_repr(self):
        self.arrange()
        self.assertEqual("Vertex('lbl')", repr(self.vertex))

    def test_str_should_return_the_same_as_repr(self):
        self.arrange()
        self.assertEqual(repr(self.vertex), str(self.vertex))

class EdgeTests(TestCase):
    def arrange(self):
        self.vertex1 = Vertex('v1')
        self.vertex2 = Vertex('v2')
        self.edge = Edge(self.vertex1, self.vertex2)

    def test_tuple_should_be_initialised_in_constructor(self):
        self.arrange()
        self.assertEqual(self.vertex1, self.edge[0])
        self.assertEqual(self.vertex2, self.edge[1])

    def test_check_repr(self):
        self.arrange()
        self.assertEqual("Edge(Vertex('v1'), Vertex('v2'))", repr(self.edge))

    def test_str_should_return_the_same_as_repr(self):
        self.arrange()
        self.assertEqual(repr(self.edge), str(self.edge))

class GraphTests(TestCase):
    def arrange(self):
        self.vs = [Vertex('v1'), Vertex('v2'), Vertex('v3')]
        self.es = [Edge(self.vs[0], self.vs[1]), Edge(self.vs[0], self.vs[2])]
        self.graph = Graph(self.vs, self.es)

    def test_all_vertexes_should_be_added_to_dict(self):
        self.arrange()
        self.assertEqual(3, len(self.graph))
        [self.assertTrue(v in self.graph) for v in self.vs]

    def test_vertex1_should_contain_edge1_and_edge2(self):
        self.arrange()
        self.assertEqual(self.es[0], self.graph[self.vs[0]][self.vs[1]])
        self.assertEqual(self.es[1], self.graph[self.vs[0]][self.vs[2]])

    def test_vertex2_should_contain_edge1(self):
        self.arrange()
        self.assertEqual(self.es[0], self.graph[self.vs[1]][self.vs[0]])

    def test_vertex3_should_contain_edge2(self):
        self.arrange()
        self.assertEqual(self.es[1], self.graph[self.vs[2]][self.vs[0]])

    def test_get_edge_edge_is_found_should_return_edge(self):
        self.arrange()
        result = self.graph.get_edge(self.vs[0], self.vs[1])
        self.assertEqual(self.es[0], result)

    def test_get_edge_edge_not_found_should_return_None(self):
        self.arrange()
        result = self.graph.get_edge(self.vs[2], self.vs[1])
        self.assertEqual(None, result)

    def test_remove_edge_should_remove_edge(self):
        self.arrange()
        self.graph.remove_edge(self.es[0])
        self.assertTrue(len([inner_node for node in self.graph for inner_node in self.graph[node] if self.graph[node][inner_node] == self.es[0]]) == 0)

    def test_vertices_should_return_graph_vertices(self):
        self.arrange()
        result = self.graph.vertices()
        self.assertEqual(self.vs, result)

    def test_edges_should_return_list_of_edges(self):
        self.arrange()
        result = self.graph.edges()
        self.assertEqual(self.es, result)

    def test_out_vertices(self):
        self.arrange()
        result = self.graph.out_vertices(self.vs[0])
        self.assertEqual([self.vs[1], self.vs[2]], result)

    def test_out_edges(self):
        self.arrange()
        result = self.graph.out_edges(self.vs[0])
        self.assertEqual([self.es[0], self.es[1]], result)

    def test_add_all_edges(self):
        self.arrange()
        self.graph = Graph(self.vs, [])
        self.graph.add_all_edges()
        for node_from in self.graph:
            for node_to in self.graph:
                if node_from != node_to:
                    self.assertTrue(self.graph[node_from][node_to] == self.graph[node_to][node_from])

    def test_add_regular_edges_should_raise_if_degree_equal_to_vertex_count(self):
        self.arrange()
        degree = len(self.vs)
        self.assertRaises(StandardError, self.graph.add_regular_edges, degree)

    def test_add_regular_edges_should_raise_if_degree_greater_than_vertex_count(self):
        self.arrange()
        degree = len(self.vs) + 1
        self.assertRaises(StandardError, self.graph.add_regular_edges, degree)

    def test_add_regular_edges_should_raise_if_degree_is_odd_and_vertex_count_is_odd(self):
        self.arrange()
        degree = len(self.vs) - 2
        self.assertRaises(StandardError, self.graph.add_regular_edges, degree)

    def test_add_regular_edges_degree_is_even_v_count_is_even_check_each_vertex_degree(self):
        self.setup_and_test_regular_edges(4,10)

    def test_add_regular_edges_degree_is_even_v_count_is_odd_check_each_vertex_degree(self):
        self.setup_and_test_regular_edges(4,9)

    def test_add_regular_edges_degree_is_odd_v_count_is_even_check_each_vertex_degree(self):
        self.setup_and_test_regular_edges(5,12)

    def test_is_connected_should_return_false_for_graph_with_one_disconnected_node(self):
        self.create_disconnected_graph(4)
        for i in range(len(self.vs) - 2):
            self.graph.add_edge(Edge(self.vs[i], self.vs[i+1]))
        result = self.graph.is_connected()
        self.assertEqual(False, result)

    def test_is_connected_should_return_true_for_circle_graph_without_one_edge(self):
        self.create_disconnected_graph(4)
        for i in range(len(self.vs) - 1):
            self.graph.add_edge(Edge(self.vs[i], self.vs[i+1]))
        result = self.graph.is_connected()
        self.assertEqual(True, result)

    def test_is_connected_should_return_true_for_circle_graph(self):
        self.create_disconnected_graph(4)
        for i in range(len(self.vs)):
            self.graph.add_edge(Edge(self.vs[i], self.vs[(i+1)%len(self.vs)]))
        result = self.graph.is_connected()
        self.assertEqual(True, result)

    def test_is_connected_should_return_false_for_graph_without_nodes(self):
        self.graph = Graph()
        result = self.graph.is_connected()
        self.assertEqual(False, result)

    def create_disconnected_graph(self, v_count):
        self.vs = [Vertex(str(i)) for i in range(1, v_count + 1)]
        self.graph = Graph(self.vs, [])

    def setup_and_test_regular_edges(self, degree, v_count):
        self.create_disconnected_graph(v_count)
        self.graph.add_regular_edges(degree)
        self.assertFalse(any([v for v in self.graph.vertices() if len(self.graph.out_edges(v)) != degree]))




