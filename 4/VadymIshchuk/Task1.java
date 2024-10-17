import java.util.Set;
import java.util.TreeSet;
import java.util.Random;

class OddWriter extends Thread {
    private Set<Integer> numbers;

    public OddWriter(Set<Integer> numbers) {
        this.numbers = numbers;
    }

    @Override
    public void run() {
        for (Integer number : numbers) {
            if (number % 2 != 0) {
                System.out.println("OddWriter: " + number);
            }
        }
    }
}

class NonOddWriter extends Thread {
    private Set<Integer> numbers;

    public NonOddWriter(Set<Integer> numbers) {
        this.numbers = numbers;
    }

    @Override
    public void run() {
        for (Integer number : numbers) {
            if (number % 2 == 0) {
                System.out.println("NonOddWriter: " + number);
            }
        }
    }
}

public class Task1 {
    public static void main(String[] args) {
        Set<Integer> numbers = new TreeSet<>();
        Random random = new Random();
        
       
        while (numbers.size() < 30) {
            numbers.add(random.nextInt(101));
        }

        OddWriter oddWriter = new OddWriter(numbers);
        NonOddWriter nonOddWriter = new NonOddWriter(numbers);

        oddWriter.start();
        nonOddWriter.start();
    }
}

