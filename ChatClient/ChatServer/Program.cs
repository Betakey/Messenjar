using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatServer.IO;
using ChatServer.Net;
using NetDLL.Utils;

namespace ChatServer
{
    class Program
    {
        public static Program Instance { get; private set; }

        static void Main(string[] args)
        {
            new Program();
        }

        public NetworkServer NetworkServer { get; private set; }

        public Config Config { get; private set; }

        public UserDataManager UserDataManager { get; private set; }

        public BackUpServerHandler BackUpServerHandler { get; private set; }

        public Program()
        {
            Instance = this;
            PreInit();
            Init();
            PostInit();
        }

        private void PreInit()
        {
            Console.Title = "MessenJar Sever";
            Console.WindowHeight = 38;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("          HHHHHHHHHHHHHHH");
            Console.WriteLine("          HHHHHHHHHHHHHHH");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("           IIIIIIIIIIIII ");
            Console.WriteLine("          IIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine("         IIIIIIIIIIIIIIIII");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("         MMMMMMMMMMMMMMMMM   AAAAAAAAAAA  RRRRRRRRR");
            Console.WriteLine("                       MMM   AA       AA  RR      RR");
            Console.WriteLine("                       MMM   AA       AA  RR      RR");
            Console.WriteLine("                       MMM   AA       AA  RR      RR");
            Console.WriteLine("                       MMM   AA       AA  RRRRRRRRR");
            Console.WriteLine("                       MMM   AAAAAAAAAAA  RRR");
            Console.WriteLine("                       MMM   AA       AA  RR RR");
            Console.WriteLine("                      MMM    AA       AA  RR  RR");
            Console.WriteLine("                     MMM     AA       AA  RR   RR");
            Console.WriteLine("                   MMM       AA       AA  RR    RR");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(" Messen  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("MMMMMMMMMMMM        AA       AA  RR     RR");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("<> Loading Config...");
            Config = new Config();
            Console.WriteLine("<> Config loaded!");
            Console.CursorVisible = false;
            Console.WriteLine("<> Loading User Data...");
            UserDataManager = new UserDataManager();
            Console.WriteLine("<> User Data loaded!");
            BackUpServerHandler = new BackUpServerHandler();
        }

        private void Init()
        {
            string ip = "";
            if (Config.AsString(ConfigKey.IPType).ToLower() == "localhost")
            {
                ip = "127.0.0.1";
            }
            else if (Config.AsString(ConfigKey.IPType).ToLower() == "external")
            {
                ip = NetUtils.GetExternalIPAddress().ToString();
            }
            NetworkServer = new NetworkServer(ip, 34563);
            NetworkServer.Start();
        }

        private void PostInit()
        {
            Console.WriteLine("If you want to stop the Server and exit the Program. Just press ESCAPE [ESC]");
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Do you really want to exit the Program? (y/n)");
                    ConsoleKey actionKey = Console.ReadKey().Key;
                    if (actionKey == ConsoleKey.Y || actionKey == ConsoleKey.J)
                    {
                        if (NetworkServer.IsAlive)
                            NetworkServer.Stop();
                        break;
                    }
                    Console.WriteLine("");
                }
            }
        }
    }
}
