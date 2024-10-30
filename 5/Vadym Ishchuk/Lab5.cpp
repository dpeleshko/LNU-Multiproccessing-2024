#include <iostream>
#include <thread>
#include <array>
#include <random>
#include <string>
#include <mutex>

std::mutex console_mutex;

void displayNameOrSurname(const std::string& name, const std::string& surname, int number) {
    std::lock_guard<std::mutex> lock(console_mutex);
    if (number % 2 == 0) {
        std::cout << "Name: " << name << std::endl;
    }
    else {
        std::cout << "Surname: " << surname << std::endl;
    }
}

int main() {

    std::array<std::string, 10> names = { "Alice", "Bob", "Carol", "Dave", "Eve", "Frank", "Grace", "Hank", "Ivy", "John" };
    std::array<std::string, 10> surnames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };
    std::array<std::thread, 10> threads;

    std::random_device rd;
    std::mt19937 gen(rd());
    std::uniform_int_distribution<> dis(0, 9);

    for (int i = 0; i < 10; ++i) {
        int random_number = dis(gen);
        threads[i] = std::thread(displayNameOrSurname, names[i], surnames[i], random_number);
    }

    for (auto& th : threads) {
        th.join();
    }

    return 0;
}




