#include <iostream> 
#include <cstdlib> 
#include <thread> 
#include <vector> 
#include <cmath> 
#include <climits> 
#include <ctime> 
#include <memory> 

#define ullong unsigned long long 

using namespace std;

void factoring(ullong arr[], int size, vector<ullong> vec[]) {
    ullong x;
    for (int i = 0; i < size; i++) {
        x = arr[i];
        for (int j = 2; j <= sqrt(x); j++) {
            while (x % j == 0) {
                vec[i].push_back(j);
                x /= j;
            }
        }
        if (x != 1)
            vec[i].push_back(x);
    }
}

ullong ullrand() {
    ullong rnd = 0;
    ullong counter = 1E+13;
    while (counter > 0) {
        rnd = (rnd * (RAND_MAX + 1)) + rand();
        counter /= (RAND_MAX + 1);
    }
    return rnd;
}

void runFactoring(int numThreads, int n) {
    srand(time(NULL));
    int size_scatter = n / numThreads;
    vector<unique_ptr<ullong[]>> arr(numThreads); 
    vector<vector<ullong>*> vec(numThreads);

    
    for (int i = 0; i < numThreads; i++) {
        arr[i] = make_unique<ullong[]>(size_scatter);  
        vec[i] = new vector<ullong>[size_scatter];

        for (int j = 0; j < size_scatter; j++) {
            arr[i][j] = ullrand();
            vec[i][j].reserve(10);
        }
    }

    vector<thread> threads(numThreads);
    long t = clock();

    for (int i = 0; i < numThreads; i++) {
        threads[i] = thread(factoring, arr[i].get(), size_scatter, vec[i]);
    }

    for (int i = 0; i < numThreads; i++)
        threads[i].join();

    long time_p = clock() - t;
    cout << "Threads: " << numThreads << ", Time = " << time_p << " ms" << endl;

    for (int i = 0; i < numThreads; i++) {
        for (int j = 0; j < size_scatter; j++) {
            cout << "arr[" << i << "][" << j << "] = " << arr[i][j] << " = 1";
            for (int k = 0; k < vec[i][j].size(); k++)
                cout << " * " << vec[i][j][k];
            cout << endl;
        }
    }

    for (int i = 0; i < numThreads; i++) {
        delete[] vec[i];
    }
}

int main() {
    int n = 8;  

    cout << "One thread:\n";
    runFactoring(1, n); 

    cout << "Two thread:\n";
    runFactoring(2, n); 

    cout << "Four thread:\n";
    runFactoring(4, n); 

    return 0;
}
