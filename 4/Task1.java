import java.util.Random;
import java.util.Set;
import java.util.TreeSet;

class OddWriter extends Thread {
    private final Set<Integer> numbers;

    public OddWriter(Set<Integer> numbers) {
        this.numbers = numbers;
    }

    @Override
    public void run() {
        synchronized (numbers) {
            for (Integer num : numbers) {
                if (num % 2 != 0) {
                    System.out.println("OddWriter: " + num);
                }
            }
        }
    }
}

class NonOddWriter extends Thread {
    private final Set<Integer> numbers;

    public NonOddWriter(Set<Integer> numbers) {
        this.numbers = numbers;
    }

    @Override
    public void run() {
        synchronized (numbers) {
            for (Integer num : numbers) {
                if (num % 2 == 0) {
                    System.out.println("NonOddWriter: " + num);
                }
            }
        }
    }
}

public class Task1 {
    public static void main(String[] args) throws InterruptedException {
        Set<Integer> numbers = generateUniqueRandomNumbers();

        OddWriter oddWriter = new OddWriter(numbers);
        NonOddWriter nonOddWriter = new NonOddWriter(numbers);

        oddWriter.start();
        nonOddWriter.start();

        oddWriter.join();
        nonOddWriter.join();
    }

    private static Set<Integer> generateUniqueRandomNumbers() {
        Set<Integer> numbers = new TreeSet<>();
        Random random = new Random();

        while (numbers.size() < 101) {  
            int num = random.nextInt(101);
            numbers.add(num);
        }

        return numbers;
    }
}
