from unittest import TestCase
from graph import Vertex, Edge, Graph

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


