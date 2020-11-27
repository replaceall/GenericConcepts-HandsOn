using DesignPatterns;
using System;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
        }

        /*
         *  Task Vs Thread
         *  https://stackoverflow.com/questions/4130194/what-is-the-difference-between-task-and-thread
         * */

        /*
         * Atomicity
         * 
         */

        /*
         * Critical Sections:
         * Lock object is useful to access the given Property by single thread only for a given time.
         */

        /*
         * InterLocked: (another approach for Critical sections , but not preferred all the times.)
         * InterLocked.Add(__, ); for Adding
         * InterLocked.Add(__, -x); for Substraction
         * 
         * InterLocked.MemoryBarrior(__, ..); https://stackoverflow.com/questions/3493931/why-do-i-need-a-memory-barrier
         * 
         */

        /* 
         * ConcurrentBag, ConcurrentStack, ConcurrentQueue are treated as  IProducerConsumerCollection
         */
    }
}
