using System;
using MPAPI;

namespace DistributedPrimeCalculatorApp
{
  public class PrimeFinder
  {
    public void CountPrimes(long min, long max, WorkerAddress returnAddress)
    {
      long count = 0;
      for (long primeCandidate = min; primeCandidate <= max; primeCandidate++)
        if (IsPrime(primeCandidate))
          count++;
      Worker.Current.Send(returnAddress, MessageTypes.Result, count);
    }

    public bool IsPrime(long primeCandidate)
    {
      //two is by definition a prime number, 1 is not however
      if (primeCandidate == 2)
        return true;
      //throw an exception if the number is less than 2
      if (primeCandidate < 2)
        throw new ArgumentException("Prime candidates cannot be less than 2");
      //throw away even numbers
      if ((primeCandidate % 2) == 0)
        return false;

      long max = (long)(Math.Sqrt(primeCandidate) + 1);
      for (long divisor = 3; divisor < max; divisor += 2)
        if ((primeCandidate % divisor) == 0)
          return false;
      return true; //it is a prime
    }
  }
}
