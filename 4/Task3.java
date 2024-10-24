class VectorInitializerX implements Runnable {
    private final int[] X;

    public VectorInitializerX(int[] X) {
        this.X = X;
    }

    @Override
    public void run() {
        for (int i = 0; i < X.length; i++) {
            X[i] = 1; 
        }
        System.out.println("Ініціалізація вектора X завершена.");
    }
}

class VectorInitializerY implements Runnable {
    private final int[] Y;

    public VectorInitializerY(int[] Y) {
        this.Y = Y;
    }

    @Override
    public void run() {
        for (int i = 0; i < Y.length; i++) {
            Y[i] = i + 1; 
        }
        System.out.println("Ініціалізація вектора Y завершена.");
    }
}

class VectorComputation implements Runnable {
    private final int[] X;
    private final int[] Y;
    private final int[] Q;

    public VectorComputation(int[] X, int[] Y, int[] Q) {
        this.X = X;
        this.Y = Y;
        this.Q = Q;
    }

    @Override
    public void run() {
        for (int i = 0; i < Q.length; i++) {
            Q[i] = 2 * X[i] + 3 * Y[i]; 
        }
        System.out.println("Обчислення вектора Q завершене.");
    }
}

public class Task3 {
    public static void main(String[] args) throws InterruptedException {
        final int N = 30; 

        int[] X = new int[N];
        int[] Y = new int[N];
        int[] Q = new int[N];

        Thread threadX = new Thread(new VectorInitializerX(X));
        Thread threadY = new Thread(new VectorInitializerY(Y));

        threadX.start();
        threadY.start();

        threadX.join();
        threadY.join();

        Thread computationThread = new Thread(new VectorComputation(X, Y, Q));
        computationThread.start();

        computationThread.join();

        System.out.println("Результуючий вектор Q:");
        for (int i = 0; i < N; i++) {
            System.out.println("Q[" + i + "] = " + Q[i]);
        }
    }
}
