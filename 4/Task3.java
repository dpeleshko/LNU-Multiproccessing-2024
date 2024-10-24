public class Task3 {
    public static void main(String[] args) throws InterruptedException {
        final int N = 30; 

        int[] X = new int[N];
        int[] Y = new int[N];
        int[] Q = new int[N];

        // Ініціалізація вектора X
        Thread threadX = new Thread(() -> {
            for (int i = 0; i < X.length; i++) {
                X[i] = 1;
            }
            System.out.println("Ініціалізація вектора X завершена.");
        });

        // Ініціалізація вектора Y
        Thread threadY = new Thread(() -> {
            for (int i = 0; i < Y.length; i++) {
                Y[i] = i + 1;
            }
            System.out.println("Ініціалізація вектора Y завершена.");
        });

        // Запуск потоків ініціалізації X і Y
        threadX.start();
        threadY.start();

        // Очікування завершення потоків
        threadX.join();
        threadY.join();

        // Обчислення вектора Q
        Thread computationThread = new Thread(() -> {
            for (int i = 0; i < Q.length; i++) {
                Q[i] = 2 * X[i] + 3 * Y[i];
            }
            System.out.println("Обчислення вектора Q завершене.");
        });

        // Запуск потоку для обчислення Q
        computationThread.start();
        computationThread.join();

        // Виведення результатів
        System.out.println("Результуючий вектор Q:");
        for (int i = 0; i < N; i++) {
            System.out.println("Q[" + i + "] = " + Q[i]);
        }
    }
}
