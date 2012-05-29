#sorting by keys
import string

a = [{23:100}, {3:103}, {2:102}, {36:103}, {43:123}]
print sorted(a, key=lambda x: x.values()[0], reverse=True)

#iterators
d = dict(a=2, b=3)
it = d.iteritems()
print it.next()
print it.next()

#infinite iterator
class AllTrue(object):
    def __iter__(self):
        return self

    def next(self):
        return True

print zip('abc', AllTrue())

#generators
def generate_letters():
    letters = 'abc'
    for l in letters:
        yield l

iter = generate_letters()
print iter
print iter.next()
print iter.next()

def alphabet_cycle():
    while True:
        for c in string.lowercase:
            yield c

itr = alphabet_cycle()
print itr.next()
print itr.next()
print itr.next()

def infinite_seq():
    i = 1
    while True:
        for c in string.lowercase:
            yield c+str(i)
        i += 1

gen = infinite_seq()
for k in range(60):
    print gen.next()

