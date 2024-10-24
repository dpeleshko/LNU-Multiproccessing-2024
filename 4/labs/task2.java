class Keeper {
    private boolean canWrite = true; 


    public synchronized void write(String message) {

        while (!canWrite) {
            try {
                wait();
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                System.out.println("Write thread interrupted");
            }
        }
        System.out.println("Write: " + message);
        canWrite = false; 
        notifyAll();
    }


    public synchronized void read(String message) {
        while (canWrite) {
            try {
                wait();
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                System.out.println("Read thread interrupted");
            }
        }
        System.out.println("Read: " + message);
        canWrite = true;  
        notifyAll();
    }
}

class SequentalWriter implements Runnable {
    private final Keeper keeper;
    private final String[] messages;

    public SequentalWriter(Keeper keeper, String[] messages) {
        this.keeper = keeper;
        this.messages = messages;
    }

    @Override
    public void run() {
        for (String message : messages) {
            keeper.write(message); 
        }
    }
}

class SequentalReader implements Runnable {
    private final Keeper keeper;
    private final String[] messages;

    public SequentalReader(Keeper keeper, String[] messages) {
        this.keeper = keeper;
        this.messages = messages;
    }

    @Override
    public void run() {
        for (String message : messages) {
            keeper.read(message); 
        }
    }
}

public class task2 {
    public static void main(String[] args) {
        Keeper keeper = new Keeper();

        String[] messages = {"Message 1", "Message 2", "Message 3", "Message 4"};

        Thread writerThread = new Thread(new SequentalWriter(keeper, messages));
        Thread readerThread = new Thread(new SequentalReader(keeper, messages));

        
        writerThread.start();
        readerThread.start();

        
        try {
            writerThread.join();
            readerThread.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}