from matplotlib import pyplot

def show_plot(xs, ys):
    pyplot.plot(xs, ys)
    scale = 'log'
    pyplot.xscale(scale)
    pyplot.yscale(scale)
    pyplot.show()

show_plot([1,2,3,4], [1,2,3,4])
