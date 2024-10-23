public class Task2 {
    public static void main(String[] args) throws InterruptedException {
        Keeper keeper = new Keeper();

        SequentalWriter writer = new SequentalWriter(keeper);
        SequentalReader reader = new SequentalReader(keeper);

        Thread writerThread = new Thread(writer);
        Thread readerThread = new Thread(reader);

        writerThread.start();
        readerThread.start();

        writerThread.join();
        readerThread.join();
    }

    static class Keeper {
        private boolean isWriteTurn = true;

        public synchronized void write() throws InterruptedException {
            while (!isWriteTurn) {
                wait();
            }
            System.out.println("Write operation");
            isWriteTurn = false;
            notify();
        }

        public synchronized void read() throws InterruptedException {
            while (isWriteTurn) {
                wait();
            }
            System.out.println("Read operation");
            isWriteTurn = true;
            notify();
        }
    }

    static class SequentalWriter implements Runnable {
        private final Keeper keeper;

        public SequentalWriter(Keeper keeper) {
            this.keeper = keeper;
        }

        @Override
        public void run() {
            for (int i = 0; i < 10; i++) {
                try {
                    keeper.write();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    static class SequentalReader implements Runnable {
        private final Keeper keeper;

        public SequentalReader(Keeper keeper) {
            this.keeper = keeper;
        }

        @Override
        public void run() {
            for (int i = 0; i < 10; i++) {
                try {
                    keeper.read();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
