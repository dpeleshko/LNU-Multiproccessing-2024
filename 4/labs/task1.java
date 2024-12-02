import java.util.TreeSet;
import java.util.Random;
import java.util.Set;


 
public class task1 {

    public static void main(String[] args) throws InterruptedException {
        Random random = new Random();

        Set<Integer> numbers = new TreeSet<>();

        while (numbers.size() < 50) {
            numbers.add(random.nextInt(101));
        }
    
        OddWriter oddWriter = new OddWriter(numbers);
        NonOddWriter nonOddWriter = new NonOddWriter(numbers);

        oddWriter.start();
        nonOddWriter.start();

        oddWriter.join();
        nonOddWriter.join();
    
    }
}

    
class OddWriter extends Thread {
    private Set<Integer> oddNumbers;

    public OddWriter(Set<Integer> numbers) {
    this.oddNumbers = numbers;
    
    }
    @Override
    public void run() {
        for (int number : oddNumbers) {
            if (number % 2 != 0) {
                System.out.println("OddWriter: " + number);
            }
        }
    }
}

class NonOddWriter extends Thread {
    private Set<Integer> evenNumbers;

    public NonOddWriter(Set<Integer> numbers) {
    this.evenNumbers = numbers;
    }
    @Override
    public void run() {
        for (int number : evenNumbers) {
            if (number % 2 == 0) {
                System.out.println("NonOddWriter: " + number);
            }
        }
    }
}

