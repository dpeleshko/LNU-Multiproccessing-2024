public class Task3 {
    public static void main(String[] args) throws InterruptedException {
        final int N = 30; 

        int[] X = new int[N];
        int[] Y = new int[N];
        int[] Q = new int[N];

        
        Thread threadX = new Thread(() -> {
            for (int i = 0; i < X.length; i++) {
                X[i] = 1;
            }
            System.out.println("Ініціалізація вектора X завершена.");
        });

        
        Thread threadY = new Thread(() -> {
            for (int i = 0; i < Y.length; i++) {
                Y[i] = i + 1;
            }
            System.out.println("Ініціалізація вектора Y завершена.");
        });

        
        threadX.start();
        threadY.start();

        
        threadX.join();
        threadY.join();

        
        Thread computationThread = new Thread(() -> {
            for (int i = 0; i < Q.length; i++) {
                Q[i] = 2 * X[i] + 3 * Y[i];
            }
            System.out.println("Обчислення вектора Q завершене.");
        });

        
        computationThread.start();
        computationThread.join();

        
        System.out.println("Результуючий вектор Q:");
        for (int i = 0; i < N; i++) {
            System.out.println("Q[" + i + "] = " + Q[i]);
        }
    }
}
