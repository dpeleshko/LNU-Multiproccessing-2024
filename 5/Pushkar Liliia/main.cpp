#include <iostream>
#include <mutex>
#include <thread>
#include <iomanip>
#include <cmath>
#include <vector>

using namespace std;

const double pi0 = 3.141592653589793238462643; // просто константа пі
mutex m;
long intervals = 5000000;

void computePi(int num, double& pi, int numThreads) {
    double x, width, localSum = 0.0;
    width = 1.0 / intervals;
    for (int i = num; i < intervals; i += numThreads) {
        x = (i + 0.5) * width;
        localSum += 4.0 / (1.0 + x * x);
    }
    localSum *= width;

    m.lock();
    pi += localSum;
    m.unlock();
}

void run_experiment(int numThreads, double& average_time) {
    double pi = 0.0;
    clock_t total_time = 0;

    for (int experiment = 0; experiment < 10; experiment++) { // 10 experiments
        // Use a vector to hold threads
        vector<thread> th(numThreads);
        clock_t t1 = clock();

        for (int i = 0; i < numThreads; i++) {
            th[i] = thread(computePi, i, ref(pi), numThreads);
        }
        for (int i = 0; i < numThreads; i++) {
            th[i].join();
        }

        clock_t t2 = clock();
        total_time += t2 - t1; 
        pi = 0.0; 
    }

    average_time = (double)total_time / 10.0; // Average execution time
}

int main() {
    double average_time_2_threads, average_time_4_threads;

    run_experiment(1, average_time_2_threads);
    cout << "Average execution time for 2 threads: " << average_time_2_threads << " clock ticks" << endl;

    run_experiment(4, average_time_4_threads);
    cout << "Average execution time for 4 threads: " << average_time_4_threads << " clock ticks" << endl;

    double speedup = average_time_2_threads / average_time_4_threads;
    cout << "Speedup from 2 threads to 4 threads: " << speedup << endl;

    return 0;
}
