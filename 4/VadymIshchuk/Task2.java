class Keeper {
    private boolean writeTurn = true;

    public synchronized void write() throws InterruptedException {
        while (!writeTurn) {
            wait();
        }
        System.out.println("Writing...");
        writeTurn = false;
        notifyAll();
    }

    public synchronized void read() throws InterruptedException {
        while (writeTurn) {
            wait();
        }
        System.out.println("Reading...");
        writeTurn = true;
        notifyAll();
    }
}

class SequentalWriter implements Runnable {
    private Keeper keeper;

    public SequentalWriter(Keeper keeper) {
        this.keeper = keeper;
    }

    @Override
    public void run() {
        try {
            for (int i = 0; i < 10; i++) {
                keeper.write();
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}

class SequentalReader implements Runnable {
    private Keeper keeper;

    public SequentalReader(Keeper keeper) {
        this.keeper = keeper;
    }

    @Override
    public void run() {
        try {
            for (int i = 0; i < 10; i++) {
                keeper.read();
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}

public class Task2 {
    public static void main(String[] args) {
        Keeper keeper = new Keeper();
        Thread writerThread = new Thread(new SequentalWriter(keeper));
        Thread readerThread = new Thread(new SequentalReader(keeper));

        writerThread.start();
        readerThread.start();
    }
}