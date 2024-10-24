class Keeper {
    private boolean isWriterTurn = true; 

    public synchronized void write(String message) {
        while (!isWriterTurn) {
            try {
                wait(); 
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                System.out.println("Writer interrupted");
            }
        }
        System.out.println("Writer: " + message);
        isWriterTurn = false; 
        notifyAll(); 
    }

    public synchronized void read() {
        while (isWriterTurn) {
            try {
                wait();
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                System.out.println("Reader interrupted");
            }
        }
        System.out.println("Reader: Читання завершене");
        isWriterTurn = true; 
        notifyAll(); 
    }
}

class SequentialWriter implements Runnable {
    private final Keeper keeper;
    private final String[] messages;

    public SequentialWriter(Keeper keeper, String[] messages) {
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

class SequentialReader implements Runnable {
    private final Keeper keeper;
    private final int messageCount; 
    public SequentialReader(Keeper keeper, int messageCount) {
        this.keeper = keeper;
        this.messageCount = messageCount;
    }

    @Override
    public void run() {
        for (int i = 0; i < messageCount; i++) {
            keeper.read();
        }
    }
}

public class Task2 {
    public static void main(String[] args) {
        Keeper keeper = new Keeper();
        String[] messages = {"Повідомлення 1", "Повідомлення 2", "Повідомлення 3", "Повідомлення 4"};

        Thread writerThread = new Thread(new SequentialWriter(keeper, messages));
        Thread readerThread = new Thread(new SequentialReader(keeper, messages.length));

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
