public class Task4 {
    private static final int N = 30;
    private static final int[] f1res = new int[N]; 
    private static final int[] f2res = new int[N]; 
    private static final int[] f3res = new int[N]; 
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
                Z[i] = -(i + 1);
            }
        });

        initX.start();
        initY.start();
        initZ.start();

        initX.join();
        initY.join();
        initZ.join();

        Thread f1 = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                f1res[i] = 7 * X[i]; 
            }
        });

        Thread f2 = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                f2res[i] = 2 * Y[i]; 
            }
        });

        Thread f3 = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                f3res[i] = 3 * Z[i]; 
            }
        });

        f1.start();
        f2.start();
        f3.start();

        f1.join();
        f2.join();
        f3.join();

        Thread finall = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                Q[i] = f1res[i] + f2res[i] + f3res[i];
            }
        });

        finall.start();
        finall.join();

        System.out.println("Vector Q:");
        for (int i = 0; i < N; i++) {
            System.out.println(Q[i]);
        }
    }
}
