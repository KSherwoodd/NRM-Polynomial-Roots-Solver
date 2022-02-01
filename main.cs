using System;
using System.Text.RegularExpressions;
using System.Linq;

class Program {
  public static char[] NUMBERS = "0123456789".ToCharArray();

  public static string differentiate (string term) {
    string tPrime = "";

    //get coefficient / exp either side of x
    var parts = term.Split('x');
    var coeff = parts[0];
    var exponent = parts[1];

    //Console.WriteLine($"split: {coeff} / {exponent}");

    //Checks the coefficient and exponent aren't empty (e.g. just "x")
    //If they are returns 1 for each
    if (exponent != "") {
      if (exponent[0] == '^') {exponent = exponent.Substring(1);}
    } else {
      exponent = "1";
    }
    if (coeff != "") {} else {
      coeff = "1";
    }

    //Console.WriteLine($"pre-op: {coeff} / {exponent}");

    //Messes with typing to get strings and ints to behave
    int iCoeff = int.Parse(coeff);
    int iExponent = int.Parse(exponent);

    iCoeff *= iExponent;
    iExponent -= 1;

    //Console.WriteLine($"post-op: {iCoeff} / {iExponent}\n");

    coeff = iCoeff.ToString();
    exponent = iExponent.ToString();


    //Builds the differential of the term and returns it
    if (exponent == "0") {
      tPrime += coeff;
    } 
    else if (exponent == "1") {
      tPrime += coeff + "x";
    } 
    else {
      tPrime += (string) coeff + "x^" + exponent;
    }

    return tPrime;
  }

  public static void Main (string[] args) {
    Console.WriteLine($"------------------------------\nNewton-Raphson root finding algorithm\nKingsley Sherwood (2022-02-01)\n------------------------------\n");

    Console.WriteLine("Polynomial: ");
    var f = String.Concat(Console.ReadLine().Where(c => !Char.IsWhiteSpace(c)));
    var fPrime = "";

    /*
    // Only to be used for the other methods of root approximation
    // Neat 1 line trick to get limits
    //
    Console.WriteLine("starting value: ");
    int[] limits = Array.ConvertAll(Console.ReadLine().Split(' '), s => int.Parse(s));
    */

    // Use Regex to split polynomial into useful pieces
    Regex regPolynomialTerm = new Regex(@"[-+]?(\d+(\.\d+)?)(x(\^[-+]?\d+)?)?");
    var terms = regPolynomialTerm.Matches(f);

    // Parse each term with an x component
    // d/dx constant => 0 therefore skipped
    foreach (Match m in terms) {
      var term = String.Concat(m.Value.Where(c => !Char.IsWhiteSpace(c)));
      if (term.Contains('x')) {
        Console.WriteLine(term);
        fPrime += (differentiate(term));
      }
    }

    //Print out the function and its differential, for testing
    Console.WriteLine(f);
    Console.WriteLine(fPrime);
  }
}