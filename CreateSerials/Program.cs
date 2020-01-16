using System;

namespace CreateSerials
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set Variables
            DateTime date = DateTime.Now;
            int numberofRacks = 0;
            //Restart Checkpoint
        Restart: string[] contRackSerials = new string[100];
            string[] rackSerials = new string[numberofRacks];
            Console.Clear();
            //Initial Display
            Console.WriteLine("To start over simply type restart in any line.");
            Console.WriteLine();
            //Start CheckPoint
        Start: Console.WriteLine("Would you like to use today's date?    ");
            //Verify Answer
            var answerDate = Console.ReadLine().ToLower();
                //Restart
            if (answerDate == "restart")
                goto Restart;
                //No
            else if (answerDate == "no")
            {
                //Allow User To Enter New Date
            EnterDate: Console.WriteLine("Please enter in a new date.    ");
                var newDate = Console.ReadLine().ToLower();
                    //Restart CheckPoint
                if (newDate == "restart")
                    goto Restart;
                    //Correct Date Entered
                if (DateTime.TryParse(newDate, out date))
                    goto NumberRacks;
                    //Incorrect Date Entered
                else
                {
                    Console.WriteLine("Date was not found please try again.");
                    goto EnterDate;
                }
            }
                //Invalid Answer
            else if (answerDate != "yes")
                goto Start;
            //User Did Not Enter New Date
            date = DateTime.Now;
            //Set Racks CheckPoint
            NumberRacks: Console.WriteLine("How many racks would you like to create?    ");
            var racksAnswer = Console.ReadLine().ToLower();
            //Check User Restart
            if (racksAnswer == "restart")
                goto Restart;
            //Verify Answer
            if (int.TryParse(racksAnswer, out numberofRacks))
            {
                //Value Too High
                if (numberofRacks > 99)
                {
                    Console.WriteLine("Max number of racks is 99. Please enter in a smaller number.");
                        goto NumberRacks;
                }
                //Enter Gap
                Console.WriteLine();
                //Set Serial Month
                var month = date.Month.ToString();
                if (month.Length == 1)
                    month = "0" + month;
                //Set Serial Day
                var day = date.Day.ToString();
                if (day.Length == 1)
                    day = "0" + day;
                //Set Serial Year
                var year = date.Year.ToString();
                //Check If contRackSerials Value Is > 99
                if (contRackSerials[99] != null)
                {
                    Console.WriteLine("The number of racks able to be created is full.......restarting");
                    goto Start;
                }
                //Check If User Has Continued Entering Values
                if (rackSerials.Length != 0)
                {
                    //Find First Empty Value To Be Entered
                    var index = Array.IndexOf(contRackSerials, null);
                    //Start Entering In Last Serials Generated To Continued Array
                    for (int i = 0; i < rackSerials.Length; i++)
                    {
                        contRackSerials[index + i] = rackSerials[i];
                    }
                    //Display Continued Serials First
                    foreach (var serial in contRackSerials)
                    {
                        if (serial == null)
                            break;
                        Console.WriteLine(serial);
                    }
                    //Enter Gap
                    Console.WriteLine();
                }
                //Reset Serial Generation
                rackSerials = new string[numberofRacks];
                var last = string.Empty;
                //Iterate Through Number Of Racks
                for (int i = 0; i < numberofRacks; i++)
                {
                    //Set End Value Of Serial
                    var index = (i + 1).ToString();
                    if (index.Length == 1)
                        index = "0" + index;
                    //Check If Value Of Serial Is Generated On Same Day
                        //Serial Generation From Different Day
                    if (last == string.Empty) 
                        rackSerials[i] = $"CIA-{month}{day}{year}{index}";
                        //Serial Generation From Same Day
                    else
                    {
                            //Last Variable Was Already Set
                        var temp = (int.Parse(last) + 1).ToString();
                        if (temp.Length == 1)
                            temp = "0" + temp;
                        rackSerials[i] = $"CIA-{month}{day}{year}{temp}";
                            //Continue Increasing The Last Generated Index
                        last = (int.Parse(last) + 1).ToString();
                    }
                    //Check If Serial Number Is Continuation From Same Day
                    if (Array.IndexOf(contRackSerials, rackSerials[i]) != -1)
                    {
                        //Find The Last Entered Serial Number Of The Day
                        var temp = int.Parse(index);
                    FindLast: index = (temp++).ToString();
                        if (index.Length == 1)
                            index = "0" + index;
                            //Check If Serial Number Exists
                        if (Array.Exists(contRackSerials, x => x == $"CIA-{month}{day}{year}{index}"))
                            goto FindLast;
                            //Serial Does Not Exist Set Current Serial Number And Last Value
                        else
                            last = index;
                        rackSerials[i] = $"CIA-{month}{day}{year}{last}";
                    }
                }
                //Display Current Generated Serials
                foreach (var serial in rackSerials)
                {
                    Console.WriteLine(serial);
                }
                //Enter Gap
                Console.WriteLine();
                //Enter More CheckPoint
            EnterMore: Console.WriteLine("Would you like to enter in more racks?");
                //Verify Answer
                var startAgain = Console.ReadLine().ToLower();
                    //User Wants To Continue
                if (startAgain == "yes")
                    goto Start;
                    //User Wants To Quit
                else if (startAgain == "no")
                    Environment.Exit(0);
                    //User Wants To Restart
                else if (startAgain == "restart")
                    goto Restart;
                    //Unknown Answer
                else
                    goto EnterMore;
            }
            //Incorrect Rack Value Entered
            else
            {
                Console.WriteLine("Number invalid please try again.");
                goto NumberRacks;
            }
        }
    }
}