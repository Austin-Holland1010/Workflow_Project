using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Assignment3_WorkflowConsoleApp
{
    public sealed class CodeActivity_CheckNumber : CodeActivity
    {
        public InArgument<int> SecretNum { get; set; }

        public OutArgument<string> answer { get; set; }

        
        protected override void Execute(CodeActivityContext context)
        {
            bool correct = false;

            while (correct != true)
            {
                Console.WriteLine("");
                Console.WriteLine("Please enter your guess at the secret number:");
                int userGuess = Int32.Parse(Console.ReadLine());
                //int userGuess = context.GetValue(this.Guess);
                String baseUrl = "http://localhost:56451/Service1.svc/VerifyNum?Num1=";
                baseUrl = baseUrl + userGuess.ToString();


                String test = context.GetValue(this.SecretNum).ToString();
                //Console.WriteLine("test = " + test);

                baseUrl = baseUrl + "&Num2=" + context.GetValue(this.SecretNum).ToString();
                //Console.WriteLine("baseUrl= " + baseUrl);

                Uri completeUri = new Uri(baseUrl);
                WebClient channel = new WebClient();
                byte[] abc = channel.DownloadData(completeUri);
                string str = System.Text.Encoding.Default.GetString(abc);

                //Console.WriteLine("answer = " + str);
                if(str == "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/\">correct</string>")
                {
                    Console.WriteLine("You guessed correctly!");
                    Console.WriteLine("");
                    correct = true;
                }
                else if(str == "<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/\">too small</string>")
                {
                    Console.WriteLine("Your guess was too small. Try agian!");
                    Console.WriteLine("");
                    correct = false;
                }
                else
                {
                    Console.WriteLine("Your guess was too large. Try agian!");
                    Console.WriteLine("");
                    correct = false;
                }
            }

        }
    }
}
