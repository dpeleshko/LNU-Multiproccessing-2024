import java.util.TreeSet;
import java.util.Random;

public class Task1 {
    public static void main(String[] args) throws InterruptedException {
        OddWriter oddWriter = new OddWriter();
        NonOddWriter nonOddWriter = new NonOddWriter();
        
        oddWriter.start();
        nonOddWriter.start();
        
        oddWriter.join();
        nonOddWriter.join();
    }

    
    public static class OddWriter extends Thread {
        private final TreeSet<Integer> oddNumbers = new TreeSet<>();

        @Override
        public void run() {
            Random random = new Random();
            while (oddNumbers.size() < 50) {  
                int number = random.nextInt(100);
                if (number % 2 != 0) {
                    synchronized (oddNumbers) {
                        if (!oddNumbers.contains(number)) {
                            oddNumbers.add(number);
                            System.out.println("OddWriter: " + number);
                        }
                    }
                }
            }
        }
    }

    public static class NonOddWriter extends Thread {
        private final TreeSet<Integer> evenNumbers = new TreeSet<>();

        @Override
        public void run() {
            Random random = new Random();
            while (evenNumbers.size() < 50) {  
                int number = random.nextInt(100);
                if (number % 2 == 0) {
                    synchronized (evenNumbers) {
                        if (!evenNumbers.contains(number)) {
                            evenNumbers.add(number);
                            System.out.println("NonOddWriter: " + number);
                        }
                    }
                }
            }
        }
    }
}
