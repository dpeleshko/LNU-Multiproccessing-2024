#include <iostream>
#include <mutex>
#include <thread>
#include <iomanip>
#include <cmath>
#include <vector>

using namespace std;

const double pi0 = 3.141592653589793238462643;
double pi = 0.0;
mutex m;
long intervals = 5000000;
int numThreads = 2;
long t1, t2;

void computePi(int num) {
    double x, width, localSum = 0.0;
    width = 1.0 / intervals;
    for (int i = num; i < intervals; i += numThreads) {
        x = (i + 0.5) * width;
        localSum += 4.0 / (1.0 + x * x);
    }
    localSum *= width;

    lock_guard<mutex> lock(m); 
    pi += localSum;
}

void computePi1() {
    double x, width;
    width = 1.0 / intervals;
    for (int i = 0; i < intervals; i++) {
        x = (i + 0.5) * width;
        pi += 4.0 / (1.0 + x * x);
    }
    pi *= width;
}

double runExperiment(int numThreads) {
    pi = 0.0;
    vector<thread> th(numThreads);
    t1 = clock();
    for (int i = 0; i < numThreads; i++)
        th[i] = thread(computePi, i);
    for (int i = 0; i < numThreads; i++)
        th[i].join();
    t2 = clock();
    double timeMultithreaded = (t2 - t1) / (double)CLOCKS_PER_SEC;
    cout << "Threads: " << numThreads << ", PI: " << setprecision(16) << pi
        << ", Error: " << setprecision(4) << fabs(pi0 - pi)
        << ", Time: " << timeMultithreaded << " sec" << endl;
    return timeMultithreaded;
}

int main() {
    double timeSingle, timeMultithreaded2, timeMultithreaded4;

    pi = 0.0;
    t1 = clock();
    computePi1();
    t2 = clock();
    timeSingle = (t2 - t1) / (double)CLOCKS_PER_SEC;
    cout << "Single thread PI: " << setprecision(16) << pi
        << ", Error: " << setprecision(4) << fabs(pi0 - pi)
        << ", Time: " << timeSingle << " sec" << endl;

    numThreads = 2;
    timeMultithreaded2 = runExperiment(numThreads);

    numThreads = 4;
    timeMultithreaded4 = runExperiment(numThreads);

    double acceleration2 = timeSingle / timeMultithreaded2;
    double acceleration4 = timeSingle / timeMultithreaded4;
    cout << "Acceleration (2 threads): " << setprecision(4) << acceleration2 << endl;
    cout << "Acceleration (4 threads): " << setprecision(4) << acceleration4 << endl;

    return 0;
}
