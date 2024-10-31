import java.util.Arrays;

public class VectorComputation {

    public static void main(String[] args) throws InterruptedException {
        int N = 30;
        double[] X = new double[N];
        double[] Y = new double[N];
        double[] Q = new double[N];

        Thread initX = new Thread(new InitVectorX(X));
        Thread initY = new Thread(new InitVectorY(Y));

        initX.start();
        initY.start();
        initX.join();
        initY.join();

        Thread computeQ = new Thread(new ComputeVectorQ(X, Y, Q));
        computeQ.start();
        computeQ.join();

        System.out.println("Vector X: " + Arrays.toString(X));
        System.out.println("Vector Y: " + Arrays.toString(Y));
        System.out.println("Vector Q (3X + 5Y): " + Arrays.toString(Q));
    }

    static class InitVectorX implements Runnable {
        private final double[] X;

        public InitVectorX(double[] X) {
            this.X = X;
        }

        @Override
        public void run() {
            Arrays.fill(X, 1.0);
            System.out.println("Vector X initialized with ones.");
        }
    }

    static class InitVectorY implements Runnable {
        private final double[] Y;

        public InitVectorY(double[] Y) {
            this.Y = Y;
        }

        @Override
        public void run() {
            for (int i = 0; i < Y.length; i++) {
                Y[i] = i + 1;
            }
            System.out.println("Vector Y initialized with values from 1 to N.");
        }
    }

    static class ComputeVectorQ implements Runnable {
        private final double[] X;
        private final double[] Y;
        private final double[] Q;

        public ComputeVectorQ(double[] X, double[] Y, double[] Q) {
            this.X = X;
            this.Y = Y;
            this.Q = Q;
        }

        @Override
        public void run() {
            for (int i = 0; i < Q.length; i++) {
                Q[i] = 3 * X[i] + 5 * Y[i];
                System.out.println("Computed Q[" + i + "] = " + Q[i]);
            }
            System.out.println("Vector Q computed successfully.");
        }
    }
}
