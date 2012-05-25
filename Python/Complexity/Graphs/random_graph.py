import random
import itertools
from unittest import TestCase
import GraphWorld
from graph import Graph, Edge, Vertex

class RandomGraph(Graph):
    def add_random_edges(self, p):
        if p >= 1:
            raise StandardError('Probability must be in [0,1)')
        v_list = self.vertices()
        combinations = list(itertools.combinations(range(len(v_list)), 2))
        for i,j in combinations:
            if random.random() <= p:
                self.add_edge(Edge(v_list[i], v_list[j]))
        return float(len(self.edges()))/float(len(combinations))


class RandomGraphTests(TestCase):
    def setUp(self):
        self.results = []
        self.prob = random.random()

    def build_graph(self, v_count):
        graph = RandomGraph([Vertex(str(i)) for i in range(1, v_count +1)], [])
        self.results.append(graph.add_random_edges(self.prob))

    def test_avg_ratio_should_be_equal_to_probability(self):
        max_count = 60
        for i in range(3, max_count + 1):
            self.build_graph(i)
        avg = float(sum(self.results)) / float(len(self.results))
        print 'Average probability = %.2f for graph probability = %.2f'%(avg, self.prob)
        self.assertTrue(abs(avg - self.prob) <= 0.01)

    def test_should_raise_when_probability_greater_than_1(self):
        graph = RandomGraph([], [])
        self.assertRaises(StandardError, graph.add_random_edges, 1.1)
