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

class VectorInitializerZ implements Runnable {
    private final int[] Z;
    private final int N;

    public VectorInitializerZ(int[] Z, int N) {
        this.Z = Z;
        this.N = N;
    }

    @Override
    public void run() {
        for (int i = 0; i < Z.length; i++) {
            Z[i] = -N + i;
        }
        System.out.println("Ініціалізація вектора Z завершена.");
    }
}

class VectorComputation implements Runnable {
    private final int[] X;
    private final int[] Y;
    private final int[] Z;
    private final int[] Q;

    public VectorComputation(int[] X, int[] Y, int[] Z, int[] Q) { 
        this.X = X;
        this.Y = Y;
        this.Z = Z;
        this.Q = Q;
    }

    @Override
    public void run() {
        for (int i = 0; i < Q.length; i++) {
            Q[i] = 3 * X[i] + 5 * Y[i] + 4 * Z[i]; 
        }
        System.out.println("Обчислення вектора Q завершене.");
    }
}


public class Task4 {
    public static void main(String[] args) throws InterruptedException {
        final int N = 30; 

        int[] X = new int[N];
        int[] Y = new int[N];
        int[] Z = new int[N];
        int[] Q = new int[N];

        Thread threadX = new Thread(new VectorInitializerX(X));
        Thread threadY = new Thread(new VectorInitializerY(Y));
        Thread threadZ = new Thread(new VectorInitializerZ(Z, N));

        threadX.start();
        threadY.start();
        threadZ.start();

        threadX.join();
        threadY.join();
        threadZ.join();

        Thread computationThread = new Thread(new VectorComputation(X, Y, Z, Q));
        computationThread.start();

        computationThread.join();

        System.out.println("Результуючий вектор Q:");
        for (int i = 0; i < N; i++) {
            System.out.println("Q[" + i + "] = " + Q[i]);
        }
    }
}
