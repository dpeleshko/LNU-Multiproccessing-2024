
public class Array implements IVector {
    private final double[] array;

    public Array(int size) {
        array = new double[size];
    }

    @Override
    public int size() {
        return array.length;
    }

    @Override
    public void set(int index, double value) {
        array[index] = value;
    }

    @Override
    public double get(int index) {
        return array[index];
    }

    @Override
    public void print() {
        for (double v : array) {
            System.out.print(v + " ");
        }
        System.out.println();
    }
}
