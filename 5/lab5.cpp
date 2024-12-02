#include <iostream>
#include <mutex>
#include <thread>
#include <iomanip>
#include <cmath>
#include <mutex>

using namespace std;

const double pi0 = 3.141592653589793238462643;
double pi = 0.0;
mutex m;
mutex printMutex;  
long intervals = 5000000;
constexpr int numThreads = 100;
long t1, t2;


const int maxIntervalsToShow = 100;

void computePi(int num) {
    double x, width, localSum = 0.0;
    width = 1.0 / intervals;

    for (int i = num; i < intervals; i += numThreads) {
        x = (i + 0.5) * width;
        localSum += 4.0 / (1.0 + x * x);

      
        if (i < maxIntervalsToShow) {
            printMutex.lock();
            cout << "Thread " << num << " processing interval: " << i << endl;
            printMutex.unlock();
        }
    }
    localSum *= width;

    m.lock();
    pi += localSum;
    m.unlock();
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

int main() {
    thread th[numThreads];
    t1 = clock();
    for (int i = 0; i < numThreads; i++)
        th[i] = thread(computePi, i);
    for (int i = 0; i < numThreads; i++)
        th[i].join();
    t2 = clock();
    cout << setprecision(16) << pi << endl;
    cout << setprecision(4) << "err: " << fabs(pi0 - pi) << endl;
    cout << "time: " << t2 - t1 << endl;

    pi = 0.0;
    t1 = clock();
    computePi1();
    t2 = clock();
    cout << setprecision(16) << pi << endl;
    cout << setprecision(4) << "err: " << fabs(pi0 - pi) << endl;
    cout << "time: " << t2 - t1 << endl;

    return 0;
}