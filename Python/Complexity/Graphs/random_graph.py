import random
import itertools
from unittest import TestCase
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

def find_phase_transition_probability(n):
    g_count = 10
    p_step = 0.1
    prob_list = []
    p = 0.0
    while p <= 1:
        connected_list = []
        for i in range(g_count):
            graph = RandomGraph([Vertex(str(i)) for i in range(1, n +1)], [])
            graph.add_random_edges(p)
            connected_list.append(graph.is_connected())
        prob_list.append(float(sum(connected_list))/float(g_count))
        p += p_step
    diff = []
    for i in range(len(prob_list) - 1):
        diff.append(prob_list[i+1] - prob_list[i])
    m = max(diff)
    return (diff.index(m) + 1)*p_step

print find_phase_transition_probability(5)

a = [{23:100}, {3:103}, {2:102}, {36:103}, {43:123}]
print sorted(a, key=lambda x: x.values()[0], reverse=True)



