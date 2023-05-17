using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Codeblock.Model
{
    public class Node
    {
        public virtual void Compilation(CodeBlock CurrentCodeBlock) { }

        #region LEVAN POLKKA
        ////////////////////////////_Levan Polkka_////////////////////////////
        public static string Calculate(string input, string type, CodeBlock CurrentCodeBlock)
        {
            if (type == "index")
            {
                if (input[0] == '[' && input[input.Length - 1] == ']')
                {
                    string a = string.Empty;
                    for (int i = 1; i < input.Length - 1; i++)
                    {
                        a += input[i];
                    }
                    return Calculate(a, "int", CurrentCodeBlock);
                }
                else
                {
                    Console.WriteLine("Exception: " + input + " is not correct Variable");
                    CurrentCodeBlock.Error();
                    return "None";
                }
            }
            else if (type == "double")
            {
                string output = GetExpression(input, CurrentCodeBlock);

                if (CurrentCodeBlock.CompilationError) return "None";

                string result = CountingDouble(output, CurrentCodeBlock);

                return result;
            }
            else if (type == "int")
            {
                string output = GetExpression(input, CurrentCodeBlock);

                if (CurrentCodeBlock.CompilationError) return "None";

                string result = CountingInt(output, CurrentCodeBlock);

                return result;
            }
            else if (type == "bool")
            {
                DataTable table = new DataTable();
                table.Columns.Add("", typeof(Boolean));

                try
                {
                    table.Columns[0].Expression = PrepareString(input, CurrentCodeBlock);
                }
                catch (SyntaxErrorException)
                {
                    Console.WriteLine("Exception: incorrect bool expression");
                    CurrentCodeBlock.Error();
                    return "None";
                }

                DataRow r = table.NewRow();
                table.Rows.Add(r);
                Boolean result = (Boolean)r[0];
                return result.ToString().ToLower();
            }
            else if (type == "string")
            {
                return CalculateString(input, CurrentCodeBlock);
            }
            else if (type == "char")
            {
                return CalculateChar(input, CurrentCodeBlock);
            }
            else
            {
                Console.WriteLine("Exception: Type is not correct");
                CurrentCodeBlock.Error();
                return "None";
            }
        }
        static string PrepareString(string s, CodeBlock CurrentCodeBlock)
        {
            s = s.Replace("||", "Or");
            s = s.Replace("&&", "And");
            s = s.Replace("!", "Not");
            s = s.Replace("==", "=");

            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] >= 'a' && s[i] <= 'z') || (s[i] >= 'A' && s[i] <= 'Z'))
                {
                    string CurrentWord = "";
                    while ((s[i] >= 'a' && s[i] <= 'z') || (s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= '0' && s[i] <= '9') || s[i] == '[' || s[i] == ']' || s[i] == '.')
                    {
                        CurrentWord += s[i];
                        i++;

                        if (i == s.Length) break;
                    }
                    if (!IsServiceVariable(CurrentWord))
                    {
                        for (int j = CurrentCodeBlock.AreaVariable.Count - 1; j >= 0; j--)
                        {
                            foreach (Variable CurrentVariable in CurrentCodeBlock.AreaVariable[j])
                            {
                                if (CurrentVariable.Name == CurrentWord && (CurrentVariable.Type == "bool" || CurrentVariable.Type == "int" || CurrentVariable.Type == "double")) //TODO:
                                {
                                    i += s.Replace(CurrentVariable.Name, CurrentVariable.Value).Length - s.Length;
                                    s = s.Replace(CurrentVariable.Name, CurrentVariable.Value);
                                    j = -1;
                                    break;
                                }
                            }
                            if (j == 0)
                            {
                                Console.WriteLine("Exception: " + CurrentWord + " is not found");
                                CurrentCodeBlock.Error();
                                return "False";
                            }
                        }
                    }
                }
            }

            return s;
        }
        static bool IsServiceVariable(string CurrentWord)
        {
            if (CurrentWord == "Or" || CurrentWord == "And" || CurrentWord == "Not" || CurrentWord == "true" || CurrentWord == "false")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetExpression(string input, CodeBlock CurrentCodeBlock)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

            string RegularPatternNumber = @"^[0-9]+$";
            string RegularPatternVariable = @"^[A-Za-z_][A-Za-z0-9_\[\]]*$";

            bool UnaryFlag = true;

            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                    continue;

                if (IsDigit(input[i]))
                {
                    string Number = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        Number += input[i];
                        i++;

                        if (i == input.Length) break;
                    }

                    if (!Regex.IsMatch(Number, RegularPatternNumber))
                    {
                        Console.WriteLine("Exception: " + Number + " is incorrect expression");
                        CurrentCodeBlock.Error();
                        return "None";
                    }

                    UnaryFlag = false;

                    output += " ";
                    i--;
                }

                if ((input[i] >= 'a' && input[i] <= 'z') || (input[i] >= 'A' && input[i] <= 'Z'))
                {
                    string Variable = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        Variable += input[i];
                        i++;

                        if (i == input.Length) break;

                        if (input[i] == '[')
                        {
                            while (input[i] != ']')
                            {
                                output += input[i];
                                i++;

                                if (i == input.Length)
                                {
                                    Console.WriteLine("Exception: There isn't ] with [");
                                    CurrentCodeBlock.Error();
                                    return "None";
                                }
                            }
                            break;
                        }
                    }

                    if (!Regex.IsMatch(Variable, RegularPatternVariable))
                    {
                        Console.WriteLine("Exception: " + Variable + " is incorrect expression");
                        CurrentCodeBlock.Error();
                        return "None";
                    }

                    UnaryFlag = false;

                    output += " ";
                    i--;
                }

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                    {
                        operStack.Push(input[i]);
                        UnaryFlag = true;
                    }
                    else if (input[i] == ')')
                    {
                        if (operStack.Count == 0)
                        {
                            Console.WriteLine("Exception: There isn't ( with )");
                            CurrentCodeBlock.Error();
                            return "None";
                        }

                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            if (operStack.Count == 0)
                            {
                                Console.WriteLine("Exception: There isn't ( with )");
                                CurrentCodeBlock.Error();
                                return "None";
                            }

                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                        UnaryFlag = false;
                    }
                    else
                    {
                        char curop = input[i];
                        if (UnaryFlag)
                        {
                            curop = (char)-curop;
                        }
                        if (operStack.Count > 0)
                            if (GetPriority(curop) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";

                        UnaryFlag = true;

                        operStack.Push(char.Parse(curop.ToString()));
                    }
                }
            }

            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output;
        }
        public static string CountingDouble(string input, CodeBlock CurrentCodeBlock)
        {
            double result = 0;
            Stack<double> temp = new Stack<double>();


            for (int i = 0; i < input.Length; i++)
            {
                if (IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (IsDigit(input[i]))
                    {
                        a += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(ParseToDouble(a));
                    i--;
                }
                else if ((input[i] >= 'a' && input[i] <= 'z') || (input[i] >= 'A' && input[i] <= 'Z'))
                {
                    string a = string.Empty;
                    string b = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        if ("[".IndexOf(input[i]) != -1)
                        {
                            while ("]".IndexOf(input[i]) == -1)
                            {
                                b += input[i];
                                i++;
                                if (i == input.Length) break;
                            }

                            if (i == input.Length) break;
                            b += input[i];
                            i++;
                        }
                        else
                        {
                            a += input[i];
                            i++;
                            if (i == input.Length) break;
                        }
                    }

                    for (int j = CurrentCodeBlock.AreaVariable.Count - 1; j >= 0; j--)
                    {
                        foreach (Variable CurrentVariable in CurrentCodeBlock.AreaVariable[j])
                        {
                            if (CurrentVariable.Name == a)
                            {
                                temp.Push(ParseToDouble(CurrentVariable.GetValue(CurrentCodeBlock, b)));
                                j = -1;
                                break;
                            }
                        }
                        if (j == 0)
                        {
                            Console.WriteLine("Exception: " + a + " is undefined");
                            Console.WriteLine("It's me");
                            CurrentCodeBlock.Error();
                            return "None";
                        }
                    }
                    i--;
                }
                else if (IsOperator(input[i]) || input[i] == 65491 || input[i] == 65493)
                {
                    if ((input[i] == 65491 || input[i] == 65493) && temp.Count >= 1) //unary plus and minus
                    {
                        double a = temp.Pop();

                        switch ((char)-input[i])
                        {
                            case '+': result = a; break;
                            case '-': result = -a; break;
                        }

                        temp.Push(result);
                    }
                    else if (temp.Count >= 2)
                    {
                        double a = temp.Pop();
                        double b = temp.Pop();

                        switch (input[i])
                        {
                            case '+': result = b + a; break;
                            case '-': result = b - a; break;
                            case '*': result = b * a; break;
                            case '/': if (a != 0) { result = b / a; break; } else { Console.WriteLine("Exception: attempted to divide by zero"); CurrentCodeBlock.Error(); } break;
                            case '^': result = ParseToDouble(Math.Pow(ParseToDouble(b.ToString()), ParseToDouble(a.ToString())).ToString()); break;
                        }
                        temp.Push(result);
                    }
                    else
                    {
                        Console.WriteLine("Exception: incorrect numeric expression");
                        CurrentCodeBlock.Error();
                        return "None";
                    }
                }
            }
            if (temp.Count != 1)
            {
                Console.WriteLine("Exception: incorrect numeric expression");
                CurrentCodeBlock.Error();
                return "None";
            }
            return temp.Peek().ToString();
        }
        public static string CountingInt(string input, CodeBlock CurrentCodeBlock)
        {
            int result = 0;
            Stack<int> temp = new Stack<int>();


            for (int i = 0; i < input.Length; i++)
            {
                if (IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (IsDigit(input[i]))
                    {
                        a += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(int.Parse(a));
                    i--;
                }
                else if ((input[i] >= 'a' && input[i] <= 'z') || (input[i] >= 'A' && input[i] <= 'Z'))
                {
                    string a = string.Empty;
                    string b = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        if ("[".IndexOf(input[i]) != -1)
                        {
                            while ("]".IndexOf(input[i]) == -1)
                            {
                                b += input[i];
                                i++;
                                if (i == input.Length) break;
                            }

                            if (i == input.Length) break;
                            b += input[i];
                            i++;
                        }
                        else
                        {
                            a += input[i];
                            i++;
                            if (i == input.Length) break;
                        }
                    }

                    for (int j = CurrentCodeBlock.AreaVariable.Count - 1; j >= 0; j--)
                    {
                        foreach (Variable CurrentVariable in CurrentCodeBlock.AreaVariable[j])
                        {
                            if (CurrentVariable.Name == a)
                            {
                                temp.Push(int.Parse(CurrentVariable.GetValue(CurrentCodeBlock, b)));
                                j = -1;
                                break;
                            }
                        }
                        if (j == 0)
                        {
                            Console.WriteLine("Exception: " + a + " is undefined");
                            Console.WriteLine("It's me");
                            CurrentCodeBlock.Error();
                            return "None";
                        }
                    }
                    i--;
                }
                else if (IsOperator(input[i]) || input[i] == 65491 || input[i] == 65493)
                {
                    if ((input[i] == 65491 || input[i] == 65493) && temp.Count >= 1) //unary plus and minus
                    {
                        int a = temp.Pop();

                        switch ((char)-input[i])
                        {
                            case '+': result = a; break;
                            case '-': result = -a; break;
                        }

                        temp.Push(result);
                    }
                    else if (temp.Count >= 2)
                    {
                        int a = temp.Pop();
                        int b = temp.Pop();

                        switch (input[i])
                        {
                            case '+': result = b + a; break;
                            case '-': result = b - a; break;
                            case '*': result = b * a; break;
                            case '/': if (a != 0) { result = b / a; break; } else { Console.WriteLine("Exception: attempted to divide by zero"); CurrentCodeBlock.Error(); } break;
                            case '^': result = int.Parse(Math.Pow(ParseToDouble(b.ToString()), ParseToDouble(a.ToString())).ToString()); break;
                        }
                        temp.Push(result);
                    }
                    else
                    {
                        Console.WriteLine("Exception: incorrect numeric expression");
                        CurrentCodeBlock.Error();
                        return "None";
                    }
                }
            }
            if (temp.Count != 1)
            {
                Console.WriteLine("Exception: incorrect numeric expression");
                CurrentCodeBlock.Error();
                return "None";
            }
            return temp.Peek().ToString();
        }
        public static double ParseToDouble(string value)
        {
            double result = Double.NaN;
            value = value.Trim();
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out result))
            {
                if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
                {
                    return Double.NaN;
                }
            }
            return result;
        }
        public static bool IsDelimeter(char c)
        {
            if (" =".IndexOf(c) != -1)
                return true;
            return false;
        }
        public static bool IsOperator(char с)
        {
            if ("+-/*^()♣".IndexOf(с) != -1)
                return true;
            return false;
        }
        public static bool IsDigit(char c)
        {
            if ("0123456789.".IndexOf(c) != -1)
                return true;
            return false;
        }
        public static byte GetPriority(char s)
        {
            if (s == -'+' || s == -'-')
            {
                return 7;
            }
            else
            {
                switch (s)
                {
                    case '(': return 0;
                    case ')': return 1;
                    case '+': return 2;
                    case '-': return 3;
                    case '*': return 4;
                    case '/': return 4;
                    case '^': return 5;
                    default: return 6;
                }
            }
        }
        public static string CalculateString(string input, CodeBlock CurrentCodeBlock)
        {
            string output = string.Empty;
            bool plus = true;
            bool error = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (error)
                {
                    break;
                }
                else if (input[i] == '+')
                {
                    if (plus)
                    {
                        Console.WriteLine("Exception: used + incorrectly");
                        error = true;
                    }
                    plus = true;
                }
                else if (Char.IsDigit(input[i]))
                {
                    Console.WriteLine("Exception: String + Number is not correct, use \" \" symbols");
                    error = true;
                }
                else if ((input[i] >= 'a' && input[i] <= 'z') || (input[i] >= 'A' && input[i] <= 'Z')) //TODO: Variable find
                {
                    string a = string.Empty;
                    string b = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        if ("[".IndexOf(input[i]) != -1)
                        {
                            while ("]".IndexOf(input[i]) == -1)
                            {
                                b += input[i];
                                i++;
                                if (i == input.Length) break;
                            }

                            b += input[i];
                            i++;
                            if (i == input.Length) break;
                        }
                        else
                        {
                            a += input[i];
                            i++;
                            if (i == input.Length) break;
                        }
                    }

                    for (int j = CurrentCodeBlock.AreaVariable.Count - 1; j >= 0; j--)
                    {
                        foreach (Variable CurrentVariable in CurrentCodeBlock.AreaVariable[j])
                        {
                            if (CurrentVariable.Name == a)
                            {
                                if (plus)
                                {
                                    if (CurrentVariable.GetValue(CurrentCodeBlock, b) == "None")
                                    {
                                        error = true;
                                    }
                                    else
                                    {
                                        output += CurrentVariable.GetValue(CurrentCodeBlock, b);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Exception: don't used +");
                                    error = true;
                                }
                                j = -1;
                                break;
                            }
                        }
                        if (j == 0)
                        {
                            Console.WriteLine("Exception: " + a + " is undefined");
                            Console.WriteLine("It's me");
                            error = true;
                        }
                    }

                    plus = false;
                    i--;
                }
                else if (input[i] == '\"')
                {
                    i++;
                    while (input[i] != '\"')
                    {
                        output += input[i];
                        i++;
                        if (i == input.Length - 1) break;
                    }
                    if (input[i] != '\"')
                    {
                        Console.WriteLine("Exception: Input has incorrect \" symbol");
                        error = true;
                    }
                    else if (!plus)
                    {
                        Console.WriteLine("Exception: don't used +");
                        error = true;
                    }
                    plus = false;
                }
            }
            if (!error)
            {
                return output;
            }
            else
            {
                CurrentCodeBlock.Error();
                return "None";
            }
        }
        public static string CalculateChar(string input, CodeBlock CurrentCodeBlock)
        {
            if (input.Length == 3 && input[0] == '\'' && input[2] == '\'')
            {
                return input[1].ToString();
            }
            else
            {
                if (input.Length > 0 && Char.IsLetter(input[0]))
                {
                    string a = string.Empty;
                    string b = string.Empty;
                    int i = 0;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        if ("[".IndexOf(input[i]) != -1)
                        {
                            while ("]".IndexOf(input[i]) == -1)
                            {
                                b += input[i];
                                i++;
                                if (i == input.Length) break;
                            }

                            b += input[i];
                            i++;
                            if (i == input.Length) break;
                        }
                        else
                        {
                            a += input[i];
                            i++;
                            if (i == input.Length) break;
                        }
                    }

                    for (int j = CurrentCodeBlock.AreaVariable.Count - 1; j >= 0; j--)
                    {
                        foreach (Variable CurrentVariable in CurrentCodeBlock.AreaVariable[j])
                        {
                            if (CurrentVariable.Name == a && (CurrentVariable.Type == "string" || CurrentVariable.Type == "char"))
                            {
                                if (CurrentVariable.GetValue(CurrentCodeBlock, b).Length == 1)
                                {
                                    return CurrentVariable.GetValue(CurrentCodeBlock, b);
                                }
                                else
                                {
                                    Console.WriteLine("Exception: Char " + a + " can't assign string");
                                    CurrentCodeBlock.Error();
                                    return "None";
                                }
                            }
                        }
                    }
                    Console.WriteLine("Exception: " + a + " is undefined");
                    Console.WriteLine("It's me");
                    CurrentCodeBlock.Error();
                    return "None";
                }
                else if (Calculate(input, "int", CurrentCodeBlock) != "None" && int.Parse(Calculate(input, "int", CurrentCodeBlock)) > 0 && int.Parse(Calculate(input, "int", CurrentCodeBlock)) < 65536)
                {
                    return ((char)int.Parse(Calculate(input, "int", CurrentCodeBlock))).ToString();
                }
                else
                {
                    Console.WriteLine("Char's Input is not correct");
                    CurrentCodeBlock.Error();
                    return "None";
                }
            }
        }
    }
    ////////////////////////////_Levan Polkka_////////////////////////////
    #endregion
}