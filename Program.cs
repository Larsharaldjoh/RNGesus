using DiscordBot;
using DiscordSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordSharp_Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            // First of all, a DiscordClient will be created, and the email and password will be defined.
            DiscordClient client = new DiscordClient();
            client.ClientPrivateInformation.Email = "b3043020@trbvn.com";
            client.ClientPrivateInformation.Password = "Cool00wow";

            // Then, we are going to set up our events before connecting to discord, to make sure nothing goes wrong.

            client.Connected += (sender, e) => // Client is connected to Discord
            {
                Console.WriteLine("Connected! User: " + e.user.Username);
                // If the bot is connected, this message will show.
                // Changes to client, like playing game should be called when the client is connected,
                // just to make sure nothing goes wrong.
                client.UpdateCurrentGame("with your fate"); // This will display at "Playing: "
            };


            client.PrivateMessageReceived += (sender, e) => // Private message has been received
            {
				if (e.message == "help" || e.message == "?")
				{
					e.author.SendMessage("Insert helpful information:");
					// Because this is a private message, the bot should send a private message back
					// A private message does NOT have a channel
					Console.WriteLine("Sent private help too: " + e.author.Username);
				}
				else if (e.message.StartsWith("join "))
				{
						string inviteID = e.message.Substring(e.message.LastIndexOf('/') + 1);
						// Thanks to LuigiFan (Developer of DiscordSharp) for this line of code!
						client.AcceptInvite(inviteID);
						e.author.SendMessage("Joined your discord server!");
						e.author.SendMessage("To send me commands in a channel prefix every command with !, ; or ?");
						e.author.SendMessage("In private chat this is not neccesary");
						Console.WriteLine("Got join request from: " + e.author.Username + " Too join: " + inviteID);
				}
				else
				{
					e.author.SendMessage("No command recognized, for a list of commands please type 'help' in chat");
				}
            };


            client.MessageReceived += (sender, e) => // Channel message has been received
			{
				Console.WriteLine(e.message_text);
				String commandPattern = @"(^[!;?.&%#][A-Za-z]{1,})";
				String diceReturn;

				if (Regex.IsMatch(e.message_text, commandPattern))
				{
					diceReturn = Dice.SimpleDice(e.message_text);
					if (diceReturn.Length > 0)
						e.Channel.SendMessage(diceReturn);
					else if (e.message_text.Contains("help") || e.message_text.Contains("?"))
					{
						e.Channel.SendMessage("Insert helpful information:");
						// Because this is a public message, 
						// the bot should send a message to the channel the message was received.
						Console.WriteLine("Sent public help too: " + e.author.Username + " In channel:" + e.Channel.Name);
					}
					else
					{
						e.Channel.SendMessage("No command recognized, for a list of commands please type 'help' in chat");
					}
				}
            };


            // Now, try to connect to Discord.
            try{ 
                // Make sure that IF something goes wrong, the user will be notified.
                // The SendLoginRequest should be called after the events are defined, to prevent issues.
                client.SendLoginRequest();
                client.Connect(); // Login request, and then connect using the discordclient i just made.
            }catch(Exception e){
                Console.WriteLine("Something went wrong!\n" + e.Message + "\nPress any key to close this window.");
            }

            // Done! your very own Discord bot is online!


            // Now to make sure the console doesnt close:
            Console.ReadKey(); // If the user presses a key, the bot will shut down.
        }

        public int stringnumber(string name, int min, int max)
        {
            // Bonus code: returns number based of bytes of string.
            // If something goes wrong, (eg: too long int) it return the min value.
            // This is fun for commands to "rate" a user or anything else, and make sure the same string returns the same number
            try
            {
                byte[] namebt = Encoding.UTF8.GetBytes(name);
                string namebtstring = namebt.ToString();
                int namebtint = Int32.Parse(namebtstring);
                Random rnd = new Random(namebtint);
                int returnint = rnd.Next(min, max);
                return returnint;
            }
            catch (Exception)
            {
                return min;
            }
        }
    }
}