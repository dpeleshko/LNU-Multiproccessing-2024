public class VectorCalculation {
    private static final int N = 30;
    private static double[] X = new double[N];
    private static double[] Y = new double[N];
    private static double[] Q = new double[N];
    
    static class InitXThread extends Thread {
        @Override
        public void run() {
            System.out.println("Initializing vector X with ones...");
            for (int i = 0; i < N; i++) {
                X[i] = 1.0;
            }
        }
    }
    
    static class InitYThread extends Thread {
        @Override
        public void run() {
            System.out.println("Initializing vector Y with numbers 1 to N...");
            for (int i = 0; i < N; i++) {
                Y[i] = i + 1;
            }
        }
    }
    
    static abstract class CalculationThread extends Thread {
        protected int startIndex;
        protected int endIndex;
        
        public CalculationThread(int start, int end) {
            this.startIndex = start;
            this.endIndex = end;
        }
    }
    
    static class CalculationThread1 extends CalculationThread {
        public CalculationThread1() {
            super(0, N/3);
        }
        
        @Override
        public void run() {
            System.out.println("Calculation thread 1 working on indices " + startIndex + " to " + (endIndex-1));
            for (int i = startIndex; i < endIndex; i++) {
                Q[i] = 7 * X[i] + 2 * Y[i];
            }
        }
    }
    
    static class CalculationThread2 extends CalculationThread {
        public CalculationThread2() {
            super(N/3, 2*N/3);
        }
        
        @Override
        public void run() {
            System.out.println("Calculation thread 2 working on indices " + startIndex + " to " + (endIndex-1));
            for (int i = startIndex; i < endIndex; i++) {
                Q[i] = 7 * X[i] + 2 * Y[i];
            }
        }
    }
    
    static class CalculationThread3 extends CalculationThread {
        public CalculationThread3() {
            super(2*N/3, N);
        }
        
        @Override
        public void run() {
            System.out.println("Calculation thread 3 working on indices " + startIndex + " to " + (endIndex-1));
            for (int i = startIndex; i < endIndex; i++) {
                Q[i] = 7 * X[i] + 2 * Y[i];
            }
        }
    }
    
    public static void main(String[] args) {
        try {
            InitXThread initX = new InitXThread();
            InitYThread initY = new InitYThread();
            
            initX.start();
            initY.start();
            
            initX.join();
            initY.join();
            
            System.out.println("\nInitialization completed. Starting calculation...\n");
            
            CalculationThread1 calc1 = new CalculationThread1();
            CalculationThread2 calc2 = new CalculationThread2();
            CalculationThread3 calc3 = new CalculationThread3();
            
            calc1.start();
            calc2.start();
            calc3.start();
            
            calc1.join();
            calc2.join();
            calc3.join();
            
            System.out.println("\nCalculation completed. Results:");
            System.out.println("Vector X:");
            printVector(X);
            System.out.println("\nVector Y:");
            printVector(Y);
            System.out.println("\nVector Q = 7X + 2Y:");
            printVector(Q);
            
        } catch (InterruptedException e) {
            System.out.println("Thread interrupted: " + e.getMessage());
        }
    }
    
    private static void printVector(double[] vector) {
        for (int i = 0; i < vector.length; i++) {
            System.out.printf("%.2f ", vector[i]);
            if ((i + 1) % 10 == 0) System.out.println();
        }
        System.out.println();
    }
}