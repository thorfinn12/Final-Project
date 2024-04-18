using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ATMProject
{
    public class ATM
    {
        private List<User> users;
        private string jsonFilePath;

        The path to the JSON file that stores user information.
       
        public ATM(string jsonFilePath)
        {
            this.jsonFilePath = jsonFilePath;
            LoadUsersFromJson();
        }

        
        private void LoadUsersFromJson()
        {
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            else
            {
                users = new List<User>();
            }
        }

        private void SaveUsersToJson()
        {
            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText(jsonFilePath, json);
        }

        
        public int RegisterUser(string name, string surname, string username, int age, string email)
        {
            int id = users.Count + 1;
            User newUser = new User(id, name, surname, username, age, email);
            users.Add(newUser);
            SaveUsersToJson();
            return id;
        }

        public User AuthenticateUser(string username)
        {
            User authenticatedUser = users.Find(user => user.Username == username);
            return authenticatedUser;
        }

        public class User
        {
            public int ID { get; private set; }
            public string Name { get; private set; }
            public string Surname { get; private set; }
            public string Username { get; private set; }
            public int Age { get; private set; }
            public string Email { get; private set; }

           
            public User(int id, string name, string surname, string username, int age, string email)
            {
                ID = id;
                Name = name;
                Surname = surname;
                Username = username;
                Age = age;
                Email = email;
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            string jsonFilePath = "users.json";
            var atm = new ATM(jsonFilePath);

            int userId = atm.RegisterUser("John", "Doe", "johndoe", 25, "johndoe@example.com");
            Console.WriteLine($"Registered user with ID: {userId}");

     
            string username = "johndoe";
            ATM.User authenticatedUser = atm.AuthenticateUser(username);
            if (authenticatedUser != null)
            {
                Console.WriteLine($"Authenticated user: {authenticatedUser.Name} {authenticatedUser.Surname}");
            }
            else
            {
                Console.WriteLine("Authentication failed. Invalid username.");
            }
        }
    }
}
