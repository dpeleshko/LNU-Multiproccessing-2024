import java.util.Arrays;

class VectorInitializerX extends Thread {
    private int[] x;

    public VectorInitializerX(int[] x) {
        this.x = x;
    }

    @Override
    public void run() {
        Arrays.fill(x, 1); 
        System.out.println("Vector X initialized.");
    }
}

class VectorInitializerY extends Thread {
    private int[] y;

    public VectorInitializerY(int[] y) {
        this.y = y;
    }

    @Override
    public void run() {
        for (int i = 0; i < y.length; i++) {
            y[i] = i + 1; 
        }
        System.out.println("Vector Y initialized.");
    }
}

class VectorComputation extends Thread {
    private int[] x;
    private int[] y;
    private int[] q;

    public VectorComputation(int[] x, int[] y, int[] q) {
        this.x = x;
        this.y = y;
        this.q = q;
    }

    @Override
    public void run() {
        for (int i = 0; i < q.length; i++) {
            q[i] = 5 * x[i] + 2 * y[i]; 
        }
        System.out.println("Computation complete.");
    }
}

public class Task3 {
    public static void main(String[] args) {
        final int N = 30;
        int[] x = new int[N];
        int[] y = new int[N];
        int[] q = new int[N];

        VectorInitializerX initializerX = new VectorInitializerX(x);
        VectorInitializerY initializerY = new VectorInitializerY(y);
        initializerX.start();
        initializerY.start();

        try {
            initializerX.join();
            initializerY.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        VectorComputation computation = new VectorComputation(x, y, q);
        computation.start();

        try {
            computation.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        System.out.println("Resulting Q vector: " + Arrays.toString(q));
    }
}

