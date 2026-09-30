


namespace OOP02_SmartDelivery_Classes
{

    #region PART 01: THEORETICAL QUESTIONS

    #region  Question 1
    /*
     Question 1:
    a) What is the difference between a class and a struct?
       - Class is a Reference Type(stored in the Heap), Struct is a Value Type(stored in the Stack).
       - Class supports Inheritance, Struct does not.
       - Class variables hold pointers to memory, so copying a class copies the pointer.Structs copy the entire data.

    b) Why are classes more suitable than structs for large applications?
       - Because copying large structs continuously between methods fills up the Stack and slows down performance.
         Classes pass pointers, which is extremely efficient.Classes also support polymorphism and inheritance
         which are the backbone of large software architectures. 
    */
    #endregion



    #endregion












    class Program
    {

        static void Main(string[] args)
        {


            
        }

    }



}