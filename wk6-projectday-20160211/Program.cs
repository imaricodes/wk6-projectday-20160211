using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


namespace wk6_projectday_20160211
{
    class Program
    {

        static void PrintMeetingSummary(string typeOfMeeting, string meetingLeader, string meetingRecorder, string meetingDate, List<string>notesToWrite) //need notes to write here
        {
            Console.Clear();
            Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
            Console.WriteLine("\nWords, Inc");
            Console.WriteLine("One-Two-Three Dictionary Dr.");
            Console.WriteLine("Morewords Falls, OH FourFourOneOneEight");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("MEETING MINUTES");
            Console.WriteLine("-------------------------------------\n");

            Console.WriteLine("Date: " + meetingDate + "\n");
            Console.WriteLine(typeOfMeeting);
            Console.WriteLine("Meeting Leader: " + meetingLeader);
            Console.WriteLine("Meeting Recorder: " + meetingRecorder + "\n");


            foreach (string item in notesToWrite) 
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nPress Any Key To Return To Main Menu");
            Console.ReadKey();
            Console.Clear();
        }
        static List<string> TopicNotes()
        {
            
            List<string> notes = new List<string>();
            bool addNew = true;
            do
            {
                Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");

                Console.Write("Topic: ");
                string topic = Console.ReadLine();
                notes.Add("\nTopic: " + topic + "\n");

                Console.Clear();
                Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");

                Console.WriteLine("**Pressing enter twice will exit this note**\n");
                Console.WriteLine();
                Console.WriteLine("Topic: " + topic + "\n");
                Console.WriteLine("Write your notes: ");
                while (true)
                {
                    string line = Console.ReadLine();
                    if (line == "")
                        break;
                    notes.Add(line);
                }
                while (true)
                {
                    Console.WriteLine("Would you like to enter notes on another topic? (Enter Y/N): ");
                    string input = Console.ReadLine().ToLower();
                    if (input == "n")
                    {
                        addNew = false;
                        break;
                    }
                    else if (input == "y")
                    {
                        addNew = true;
                        Console.Clear();
                        break;
                    }
                }
            }
            while (addNew == true);

        
            return notes;
        }
        static void ViewAllMembers(Dictionary<string,string>teamMembers)
        {
            Console.Clear();
            Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
            Console.WriteLine("All Team Members\n");
            foreach (KeyValuePair<string, string> pair in teamMembers)
            {
                Console.WriteLine("{0,-25}  ({1})", pair.Key, pair.Value);
            }

        } 
        static void ViewByTeam(Dictionary<string, string> teamMembers)
        {

            //Create Menu  *ideally, this menu creation process would be it's own method, but I had trouble with that
            List<string> teamsList = new List<string>(teamMembers.Values.Distinct()); //copy distinct values to a list

            


            //Convert user input for use as integer in switch case
            bool intResultTryParse;
            string input;
            int teamSelected;
            do
            {

                Console.Clear();
                Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
                Console.WriteLine("Which team would you like to view?\n");

                //Write teams to console
                int counter = 0;
                foreach (string item in teamsList)
                {
                    counter++;
                    Console.WriteLine(counter + ". " + item);
                }
                Console.WriteLine((counter + 1) + ". All Company Members");
                //User input: select team
                input = Console.ReadLine();
                intResultTryParse = int.TryParse(input, out teamSelected);
                if (intResultTryParse)
                {
                    break;
                }
                Console.WriteLine("Choose a menu item.");
            } while (!intResultTryParse);

 
            switch (input)
            {
                case "1":
                case "2":
                case "3":    
                    Console.Clear();
                    Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
                    Console.WriteLine("{0} Team\n", teamsList[teamSelected - 1].ToString());
                    foreach (KeyValuePair<string, string> pair in teamMembers)
                    {
                        if (pair.Value == teamsList[teamSelected -1].ToString())
                        {

                        Console.WriteLine(pair.Key);
                        }
                    }
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
                    Console.WriteLine("All Team Members\n");
                    foreach (KeyValuePair<string, string> pair in teamMembers)
                    {
                        Console.WriteLine("{0,-25}  ({1})", pair.Key, pair.Value);
                    }
                    break;

                default: //any input other than a digit...
                    {
                        Console.Clear();
                        Console.WriteLine("Choose a number from the list.\n");
                        ViewByTeam(teamMembers); //restart method
                        break;
                    }

                }
            Console.WriteLine("\nPress Any Key To Return To Main Menu");
            Console.ReadKey();
            Console.Clear();
            return;

        } 
        static string MeetingType()
        {
            List<string> meetingTypes = new List<string>() { "Dictionary Entries", "Word Problems", "Word Sorting", "General Word Issues", "New Word Announcements", "Special Topics" };
            Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
            Console.WriteLine("CHOOSE A MEETING TYPE:");
            for (int i = 0; i < meetingTypes.Count; i++)
            {
                Console.Write((i + 1) + ". "); Console.WriteLine(meetingTypes[i]);
            }
            
            string input = Console.ReadLine(); //changed this to input
            string meetingChoice = "";


            switch (input)
            {
                case "1":
                    meetingChoice = "Dictionary Entries";
                    
                    break;                
                case "2":
                    meetingChoice = "Word Problems";
                    break;
                case "3":
                    meetingChoice = "Word Sorting";
                    break;
                case "4":
                    meetingChoice = "General Word Issue";
                    break;
                case "5":
                    meetingChoice = "New Word";
                    break;
                case "6":
                    meetingChoice = "Special Topic";
                    break;
                default: //any input other than a digit...
                    {
                        Console.Clear();
                        Console.WriteLine("Choose a number from the list.\n");
                        MeetingType(); //restart method

                        break;

                    }

            }

            return meetingChoice;
        } 
        static void CreateMeeting()
        {
            //User Input
            Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
            Console.WriteLine("Enter Date (MMDDYY)");
            string meetingDate = Console.ReadLine();
            Console.WriteLine("Who is recording meeting (first last)");
            string meetingRecorder = Console.ReadLine();
            Console.WriteLine("Who is leading the meeting: (first last)");
            string meetingLeader = Console.ReadLine();

            //User Input: What type of meeting?
            Console.Clear();
            string meetingType = MeetingType();

            //User Input: topic and notes
            Console.Clear();
            
            List<string> notesToWrite = new List<string>(TopicNotes());

            //create new file
            string fileName = @"MeetingMinutes" + meetingDate + ".txt";

            StreamWriter sw = new StreamWriter(fileName);


            //write header and notes to file
            using (sw)
            {
                sw.WriteLine("Words, Inc");
                sw.WriteLine("ONe-Two-Three Dictionary Dr.");
                sw.WriteLine("Morewords Falls, OH FourFourOneOneEight");
                sw.WriteLine("\n-----------------------------------------");
                sw.WriteLine("MEETING MINUTES");
                sw.WriteLine("-----------------------------------------");

                sw.WriteLine("Date: " + meetingDate + "\n");
                sw.WriteLine(meetingType);
                sw.WriteLine("Meeting Leader: " + meetingLeader);
                sw.WriteLine("Meeting Recorder: " + meetingRecorder + "\n\n");


                sw.WriteLine(" ");
                sw.WriteLine("\n\nNOTES:\n\n");
                sw.WriteLine(" ");
                foreach (string note in notesToWrite)
                {
                
                    sw.WriteLine(note);
                }

                PrintMeetingSummary(meetingType, meetingLeader, meetingRecorder, meetingDate, notesToWrite);
                

            }



        }
        static void mainMenu(Dictionary<string,string>teamMembers)
        {
            Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("1. Create Meeting\n2. View Team\n3. Exit\n");

            Console.WriteLine();

            string input = Console.ReadLine();

            //string selection = "";


            switch (input)
            {
                case "1":
                    Console.Clear();
                    CreateMeeting();
                    break;
                case "2":
                    Console.Clear();
                    ViewByTeam(teamMembers);
                    break;
                case "3":
                    Exit();
                    break;
             
                default: //any input other than a digit...
                    {
                        Console.Clear();
                        Console.WriteLine("Choose a number from the list only.\n");
                        mainMenu(teamMembers); //restart method

                        break;

                    }

            }


            mainMenu(teamMembers);
        }
        static void Exit()
        {
            Console.Clear();
            Console.WriteLine("**********  Meeting Minutes Management Software  **********\n\n");
            Console.WriteLine("Goodbye.");
            Thread.Sleep(2000);
            Environment.Exit(0);

        }


        static void Main(string[] args)
        {

            Dictionary<string, string> teamMembers = new Dictionary<string, string>();
            teamMembers.Add("Marcus Motherlover", "Administration");
            teamMembers.Add("Sugarpie McGee", "Administration");
            teamMembers.Add("Heavyfeather Willblow", "Administration");
            teamMembers.Add("Marco Rubio", "Special Bee Ess");
            teamMembers.Add("Carly Fiorina", "Special Bee Ess");
            teamMembers.Add("Georgia Bushit", "Special Bee Ess");
            teamMembers.Add("Herby Trustingham", "Spelling");
            teamMembers.Add("Helpy Yelperson", "Spelling");
            teamMembers.Add("Friendly Farquartington", "Spelling");

            List<string> meetingTypes = new List<string>() { "Dictionary Entries", "Word Problems", "Word Sorting", "General Word Issues", "New Word Announcements", "Special Topics" };
            
            mainMenu(teamMembers);
            

          
            




        }
    }
}
