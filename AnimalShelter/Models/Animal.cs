using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AnimalShelter;

namespace AnimalShelter.Models
{
    public class AnimalClass
    {
        private string _name;
        private string _type;
        private string _sex;
        private string _date;
        private int _age;
        private int _id;

        public AnimalClass(string name, string type, string sex, string date, int age, int id = 0)
        {
            _name = name;
            _type = type;
            _sex = sex;
            _date = date;
            _age = age;
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetType()
        {
            return _type;
        }

        public string GetSex()
        {
            return _sex;
        }

        public string GetDate()
        {
            return _date;
        }

        public int GetAge()
        {
            return _age;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetType(string type)
        {
            _type = type;
        }

        public void SetSex(string sex)
        {
            _sex = sex;
        }

        public void SetDate(string date)
        {
            _date = date;
        }

        public void SetAge(int age)
        {
            _age = age;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO animals (name, type, sex, date, age) VALUES (@AnimalName , @AnimalType , @AnimalSex , @AnimalDate , @AnimalAge);";
            
            MySqlParameter name = new MySqlParameter();
            MySqlParameter type = new MySqlParameter();
            MySqlParameter sex = new MySqlParameter();
            MySqlParameter date = new MySqlParameter();
            MySqlParameter age = new MySqlParameter();

            name.ParameterName = "@AnimalName";
            type.ParameterName = "@AnimalType";
            sex.ParameterName = "@AnimalSex";
            date.ParameterName = "@AnimalDate";
            age.ParameterName = "@AnimalAge";

            name.Value = this._name;
            type.Value = this._type;
            sex.Value = this._sex;
            date.Value = this._date;
            age.Value = this._age;

            cmd.Parameters.Add(name);
            cmd.Parameters.Add(type);
            cmd.Parameters.Add(sex);
            cmd.Parameters.Add(date);
            cmd.Parameters.Add(age);
            cmd.ExecuteNonQuery();
            // _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<AnimalClass> GetAll()
        {
            List<AnimalClass> allAnimals = new List<AnimalClass> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM animals;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string name = rdr.GetString(0);
                int id = rdr.GetInt32(1);
                string type = rdr.GetString(2);
                string sex = rdr.GetString(3);
                DateTime date = (DateTime) rdr.GetDateTime(4);
                int age = rdr.GetInt32(5);
                AnimalClass newAnimal = new AnimalClass(name, type, sex, date.ToString("MM/d/yyyy"), age, id);
                allAnimals.Add(newAnimal);
                
            }
            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
            return allAnimals;
        }
    }

    public class AnimalType
    {

        private string _type;

        public AnimalType(string type)
        {
            _type = type;
        }

        public string GetType()
        {
            return _type;
        }

        public static List<AnimalType> GetTypes()
        {
            List<AnimalType> allTypes = new List<AnimalType> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT DISTINCT Type FROM animals;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string type = rdr.GetString(0);
                AnimalType newType = new AnimalType(type);
                allTypes.Add(newType);
                
            }
            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
            return allTypes;
        }
    }
}
