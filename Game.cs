using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace game1402_a2_starter
{
    public class GameData
    {
        public string GameName { get; set; } //This is an example of a property; for whatever reason your serializable data objects will need to be written this way
        public string Description { get; set; }
        public List<Room> Rooms { get; set; } //this is only an example. You do not ha

        public string HelpMessage { get; set; }

    }

    public class Game(GameData data)
    {
        private GameData _gameData = data;
        private Room CurrentRoom;

        public void ProcessString(string enteredString)
        {
            enteredString = enteredString.Trim().ToLower(); //trim any white space from the beginning or end of string and convert to lower case
            string[] commands = enteredString.Split(" "); //split based on spaces. The length of this array will tell you whether you have 1, 2, 3, 4, or more commands.
            //modify these functions however you want, but this is where the beginning of calling functions to handle where you are
            string response = "I dont understand"; //you will always do something when processing the string and then give a response

            
            switch (commands.Length)
            {
                case 0:
                    //do nothing
                    
                    break;
                case 1:
                    if (commands[0] == "help")
                    {
                        response = _gameData.HelpMessage;
                    }
                    break;
                case 2:
                    if (commands[0] == "look")
                    {
                        Object obj = IsObject(commands[1]);
                        if (obj != null)
                        {
                            response = obj.Description;
                        }
                        else
                        {
                            response = "no object";
                        }
                    }else if (commands[0] == "go")
                    {
                        if (commands[1] == "forward")
                        {
                            Room nextRoom = _gameData.Rooms[1];
                            SetRoom(nextRoom);
                            SetMessage(nextRoom.Description);
                            ShowObjects(nextRoom);
                            response = nextRoom.Name;
                        }else if (commands[1] == "right")
                        {
                            if(CurrentRoom == _gameData.Rooms[1])
                            {
                                Room nextRoom = _gameData.Rooms[2];
                                SetRoom(nextRoom);
                                SetMessage(nextRoom.Description);
                                response = nextRoom.Name;
                            }
                            else
                            {
                                response = "you must first go to the dining room";
                            }
                           
                        }
                        else 
                        {
                            response = "no room";
                        }
                    }
                    break;

            }


            Console.WriteLine(response); //what you tell the person after what they entered has been processed
        }
        public void SetMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void SetRoom(Room room)
        {
            CurrentRoom = room;
        }

        public Object IsObject(string objectName)
        {
            Object result = null;
            for (int i = 0; i < CurrentRoom.Objects.Count; i++)
            {
                if (CurrentRoom.Objects[i].Name == objectName)
                    result = CurrentRoom.Objects[i];
            }
            return result;
        }

        public void ShowObjects(Room room)
        {
            for (int i = 0; i < room.Objects.Count; i++)
            {
                SetMessage(room.Objects[i].Name);
            }
        }
    }



}
