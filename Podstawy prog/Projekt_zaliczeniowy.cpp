#include <iostream>
#include <chrono>
#include <ctime>
#include <fstream>
#include <random>
#include <string>
#include <sstream>

using namespace std;

//Welcome message function, with current date
void welcomeMesg() {
    //Getting the time from system info
    auto current_time = chrono::system_clock::now();
    time_t current_time_t = chrono::system_clock::to_time_t(current_time);

    tm* time_info = localtime(&current_time_t);

    //Assigning date to the int variables for the welcoming message
    int year = 1900 + time_info->tm_year;
    int month = 1 + time_info->tm_mon;
    int day = time_info->tm_mday;

    //Welcome message
    cout << "Witaj, dzis jest " << year << "-" << month << "-" << day << ". Cena paliwa wynosi 6.30 zł \nCo chcialbys zrobic?\n";
}

// This function checks if file exists
bool exists( const char * file ) {
    //Creating input file stream named ifs and attempt to open file named s
    ifstream ifs( file );
    //If file doesn't exist then stream is in bad state, we check it and if ifs isn't in good state then we return false
    if( !ifs.good() ) return false;
    //Closing file for good practice
    ifs.close();
    //Returning true because ifs is in good state and file exist
    return true;
}

//Self explainatory
void menu() {
    int choice;

    while (true) {
        cout << "\n Wybierz co chcesz zrobic. \n";
        cout << "1 - sprawdz produkty \n";
        cout << "2 - przypisz cene \n";
        cout << "3 - suma produktow \n";
        cout << "4 - cena paliwa za X dni \n";
        cout << "5 - wyjdz. \n \n";

        cin >> choice;

        if (choice == 1) {
            // Creating a text string, which will be used to output the text file
            string Text;

            if (exists("products_with_price.txt")) {
                // Reading from the text file
                ifstream MyReadFile("products_with_price.txt");
                // Using a while loop together with the getline() function to read the file line by line and print it
                while (getline(MyReadFile, Text)) {
                    cout << "\n" << Text << "zl";
                }
                cout << "\n";
                // Closing file
                MyReadFile.close();
            } else if (exists("products.txt")) {
                // Reading from the text file
                ifstream MyReadFile("products.txt");
                // Using a while loop together with the getline() function to read the file line by line and print it
                while (getline(MyReadFile, Text)) {
                    cout << "\n" << Text;
                }
                cout << "\n";
                // Closing file
                MyReadFile.close();
            } else {
                // If both files doesn't exist we need to inform user about lack of text file
                cout << "You need products.txt file with list of products to view them" << endl;
                break;
            }
        }
        else if (choice == 2) {
            // Creating input and output stream
            ifstream MyReadFile("products.txt");
            ofstream MyWriteFile("products_with_price.txt");

            string Text;
            string price;
            
            // Asking user a price for each product and stream it to output file
            while (getline(MyReadFile, Text)) {
                cout << Text << endl;
                cout << "Ile ma kosztowac ten produkt?: ";
                cin >> price;
                cout << endl;
                
                MyWriteFile << Text << " " << price << endl;
            }
            
            // Creating input stream to view products with prices
            ifstream PricedProducts("products_with_price.txt");
            
            // Printing values in loop
            while (getline(PricedProducts, Text)) {
                cout << Text << "zl" << endl;
            }
            
            // Closing files
            MyReadFile.close();
            MyWriteFile.close();
            PricedProducts.close();
        }
        else if (choice == 3) {
            // Creating input stream 
            ifstream MyReadFile("products_with_price.txt");

            string Text;
            int sum = 0;
            // Declaring a counter
            int i = 1;

            // Reading lines but diving each line when space appears so price is seperated from product name
            while (getline(MyReadFile, Text, ' ')) {
                // I know that second element is price so i'm checking if it equals 2
                // This getline don't divide by \n so in next iteration Text will be looking like this: "10\nKlawiatura"
                if (i == 2) {
                    // stoi function convert string to int variable so we receiving only price;
                    int price = stoi(Text);
                    // Appending price to sum
                    sum += price;
                } else {
                    // Appending to counter
                    i++;
                }
            }

            // Printing sum
            cout << "Suma cen wszystkich produktow wynosi: "<< sum << "zl" << endl;

        }
        else if (choice == 4) {
            //Getting the time from system info
            auto current_time = chrono::system_clock::now();
            time_t current_time_t = chrono::system_clock::to_time_t(current_time);

            //user input for the numbers of days added
            int no_day;
            cout << "Podaj liczbe dni, ktora dodamy do dzisiejszej daty:";
            cin >> no_day;

            auto new_time = current_time + chrono::hours(24 * no_day);
            time_t new_time_t = chrono::system_clock::to_time_t(new_time);

            tm* time_info = localtime(&new_time_t);

            //Assigning the new date to the int variables for the welcoming message
            int year = 1900 + time_info->tm_year;
            int month = 1 + time_info->tm_mon;
            int day = time_info->tm_mday;


            //generating a double number between 6 and 8
            double lower_bound = 6;
            double upper_bound = 8;
            uniform_real_distribution<double> unif(lower_bound, upper_bound);
            default_random_engine re;
            double a_random_double = unif(re);

            //used to round up the double value to two decimal points
            cout.precision(3);
            cout << "Cena paliwa dnia " << year << "-" << month << "-" << day << " moze bedzie wynosic: " << a_random_double << " zl." << endl;
        }
        else if (choice == 5) {
            break;  // Stop the while loop, if the user choses 5
        }
        else {
            //If user chooses incorrectly the program will inform him
            cout << "Nieprawidlowy wybor. Sprobuj jeszcze raz.\n";
        }
    }
}

int main() {
    welcomeMesg();
    menu();

    return 0;
}