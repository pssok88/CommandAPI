using System.Collections.Generic;
using Models;

namespace Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
               new Command
            {
                Id = 0,
                HowTo = "command instruction 0",
                Line = "Press buttons 00",
                Platform = "visual studio code 0"
            },
            new Command
            {
                Id = 1,
                HowTo = "command instruction 1",
                Line = "Press buttons 1",
                Platform = "visual studio code 1"
            },new Command
            {
                Id = 2,
                HowTo = "command instruction 2",
                Line = "Press buttons 2",
                Platform = "visual studio code 2"
            }
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                HowTo = "command instruction",
                Line = "Press buttons",
                Platform = "visual studio code"
            };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}