public class task3 {
    private static final int N = 30;
    private static final int[] X = new int[N];
    private static final int[] Y = new int[N];
    private static final int[] Q = new int[N];

    public static void main(String[] args) throws InterruptedException {
        Thread initX = new Thread(() -> initializeVectorX());
        Thread initY = new Thread(() -> initializeVectorY());

        initX.start();
        initY.start();

        initX.join();
        initY.join();

        Thread calcQ = new Thread(() -> calculateQ());

        calcQ.start();
        calcQ.join();

        printResult(Q);
    }

    private static void initializeVectorX() {
        for (int i = 0; i < N; i++) {
            X[i] = 1;
        }
    }

    private static void initializeVectorY() {
        for (int i = 0; i < N; i++) {
            Y[i] = i + 1;
        }
    }

    private static void calculateQ() {
        for (int i = 0; i < N; i++) {
            Q[i] = 7 * X[i] + 2 * Y[i];
        }
    }

    private static void printResult(int[] Q) {
        System.out.println("Vector Q:");
        for (int i = 0; i < N; i++) {
            System.out.println("Q[" + i + "] = " + Q[i]);
        }
    }
}