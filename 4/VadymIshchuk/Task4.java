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

class VectorInitializerZ extends Thread {
    private int[] z;

    public VectorInitializerZ(int[] z) {
        this.z = z;
    }

    @Override
    public void run() {
        for (int i = 0; i < z.length; i++) {
            z[i] = -z.length + i; 
        }
        System.out.println("Vector Z initialized.");
    }
}

class VectorComputation extends Thread {
    private int[] x;
    private int[] y;
    private int[] z;
    private int[] q;

    public VectorComputation(int[] x, int[] y, int[] z, int[] q) {
        this.x = x;
        this.y = y;
        this.z = z;
        this.q = q;
    }

    @Override
    public void run() {
        for (int i = 0; i < q.length; i++) {
            q[i] = 5 * x[i] + 2 * y[i] + z[i];
        }
        System.out.println("Computation complete.");
    }
}

public class Task4 {
    public static void main(String[] args) {
        final int N = 30;
        int[] x = new int[N];
        int[] y = new int[N];
        int[] z = new int[N];
        int[] q = new int[N];

        
        VectorInitializerX initializerX = new VectorInitializerX(x);
        VectorInitializerY initializerY = new VectorInitializerY(y);
        VectorInitializerZ initializerZ = new VectorInitializerZ(z);
        initializerX.start();
        initializerY.start();
        initializerZ.start();

        try {
            initializerX.join(); 
            initializerY.join(); 
            initializerZ.join(); 
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        
        VectorComputation computation = new VectorComputation(x, y, z, q);
        computation.start();

        try {
            computation.join(); 
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        System.out.println("Resulting Q vector: " + Arrays.toString(q));
    }
}

