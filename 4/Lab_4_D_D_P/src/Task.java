
public class Task {

    static final int N = 30;  // Define size of vectors

    public static void main(String[] args) throws InterruptedException {
        // Ініціалізація векторів
        IVector xVector = new Array(N);
        IVector yVector = new Array(N);
        IVector resultVector = new Array(N);

        System.out.print("[Test] xVector: ");
        xVector.print(); // Ініціалізація вектора X одиницями
        WriteThread xInitThread = new WriteThread(xVector, 1);

        System.out.print("[Test] yVector: ");
        yVector.print(); // Ініціалізація вектора Y числами від 1 до N
        WriteThread yInitThread = new WriteThread(yVector, 2);

        xInitThread.start();
        yInitThread.start();
        
        // Очікування завершення ініціалізації
        xInitThread.join();
        yInitThread.join();

        // Обчислення вектора Q = X + 9Y
        ComputationThread computationThread = new ComputationThread(xVector, yVector, resultVector);
        computationThread.start();
        computationThread.join();

        System.out.print("[Test] resultVector: ");
        resultVector.print(); // Вивід результату
    }

    public static class WriteThread extends Thread {
        private final IVector vector;
        private final int type;

        public WriteThread(IVector vector, int type) {
            this.vector = vector;
            this.type = type;
        }

        public void run() {
            for (int i = 0; i < vector.size(); i++) {
                double value = (type == 1) ? 1 : i + 1;
                vector.set(i, value);
                System.out.println("Write: " + value + " to position " + i);
            }
        }
    }

    public static class ComputationThread extends Thread {
        private final IVector xVector, yVector, resultVector;

        public ComputationThread(IVector xVector, IVector yVector, IVector resultVector) {
            this.xVector = xVector;
            this.yVector = yVector;
            this.resultVector = resultVector;
        }

        public void run() {
            for (int i = 0; i < xVector.size(); i++) {
                double result = xVector.get(i) + 9 * yVector.get(i);
                resultVector.set(i, result);
                System.out.println("Compute: Q[" + i + "] = " + result);
            }
        }
    }
}
