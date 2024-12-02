public class Task3 {
    private static final int N = 30;
    private static final int[] X = new int[N];
    private static final int[] Y = new int[N];
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

        initX.start();
        initY.start();

        initX.join();
        initY.join();

        Thread calcQ = new Thread(() -> {
            for (int i = 0; i < N; i++) {
                Q[i] = X[i] + 9 * Y[i];
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
