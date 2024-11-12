#include <iostream>
#include <iomanip>
#include <thread>
#include <vector>
#include <chrono>
#include <mutex>
#include <boost/multiprecision/cpp_dec_float.hpp>

using namespace std;
using boost::multiprecision::cpp_dec_float_100;
mutex mtx;

// Функція для обчислення частини ряду Лейбніца з довільною точністю
cpp_dec_float_100 calculatePartialSum(long long start, long long end) {
    cpp_dec_float_100 partialSum = 0.0;
    for (long long i = start; i < end; i++) {
        cpp_dec_float_100 denominator = 2 * i + 1;
        if (i % 2 == 0) {
            partialSum += 1.0 / denominator;
        }
        else {
            partialSum -= 1.0 / denominator;
        }
    }
    return partialSum;
}

// Функція для паралельного обчислення з довільною точністю
void parallelCalculation(long long start, long long end, cpp_dec_float_100& result) {
    cpp_dec_float_100 localSum = calculatePartialSum(start, end);

    lock_guard<std::mutex> lock(mtx);
    result += localSum;
}

// Послідовне обчислення π з довільною точністю
cpp_dec_float_100 calculatePiSequential(long long iterations) {
    cpp_dec_float_100 pi = calculatePartialSum(0, iterations) * 4;
    return pi;
}

// Паралельне обчислення π з довільною точністю
cpp_dec_float_100 calculatePiParallel(long long iterations, int numThreads) {
    vector<thread> threads;
    cpp_dec_float_100 result = 0.0;

    long long iterationsPerThread = iterations / numThreads;

    for (int i = 0; i < numThreads; i++) {
        long long start = i * iterationsPerThread;
        long long end = (i == numThreads - 1) ? iterations : (i + 1) * iterationsPerThread;

        threads.emplace_back(parallelCalculation, start, end, ref(result));
    }

    for (auto& thread : threads) {
        thread.join();
    }

    return result * 4;
}

int main() {
    const long long iterations = 1000000; // Кількість ітерацій (менше для демонстрації)
    const int numThreads = thread::hardware_concurrency(); // Кількість доступних потоків

    cout << "Amount of threads: " << numThreads << endl;
    cout << fixed << setprecision(100);  // Точність виведення до 100 знаків після коми

    // Послідовне обчислення
    auto start = chrono::high_resolution_clock::now();
    cpp_dec_float_100 piSequential = calculatePiSequential(iterations);
    auto end = chrono::high_resolution_clock::now();
    auto durationSequential = chrono::duration_cast<chrono::milliseconds>(end - start);

    cout << "\nSequential calculation:" << endl;
    cout << "pi = " << piSequential << endl;
    cout << "Execution time: " << durationSequential.count() << " ms" << endl;

    // Паралельне обчислення
    start = chrono::high_resolution_clock::now();
    cpp_dec_float_100 piParallel = calculatePiParallel(iterations, numThreads);
    end = chrono::high_resolution_clock::now();
    auto durationParallel = chrono::duration_cast<chrono::milliseconds>(end - start);

    cout << "\nParallel computing:" << endl;
    cout << "pi = " << piParallel << endl;
    cout << "Execution time: " << durationParallel.count() << " ms" << endl;

    // Порівняння швидкодії
    cpp_dec_float_100 speedup = static_cast<cpp_dec_float_100>(durationSequential.count()) / durationParallel.count();
    cout << "\nAcceleration: " << speedup << "x" << endl;

    return 0;
}
