public class task4 {
    private static final int N = 30;
    private static final int[] X = new int[N];
    private static final int[] Y = new int[N];
    private static final int[] Z = new int[N];
    private static final int[] Q = new int[N];

    public static void main(String[] args) throws InterruptedException {
        Thread initX = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                X[i] = 1;
            }
        });

        Thread initY = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                Y[i] = i + 1;
            }
        });

        Thread initZ = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                Z[i] = i + 2;
            }
        });

        initX.start();
        initY.start();
        initZ.start();

        initX.join();
        initY.join();
        initZ.join();

        Thread calcQ = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                Q[i] = 7 * X[i] + 2 * Y[i] + 3 * Z[i];
            }
        });

        calcQ.start();
        calcQ.join();

        System.out.println("Vector Q:");
        for (int i = 0; i < N; i++) {
            System.out.println(Q[i]);
        }
    }
}