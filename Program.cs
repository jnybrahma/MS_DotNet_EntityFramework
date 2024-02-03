using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using Dapper;
using EntityFramework.Data;
using EntityFramework.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;



namespace EntityFramework
{

    public class Program
    {

        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF entityFramework = new DataContextEF(config);


            // Console.WriteLine(rightNow);

            Computer myComputer = new Computer()
            {
                Motherboard = "IntelCore 8",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 5534.87m,
                VideoCard = "Nvidia X60"
            };

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            string sql= @"INSERT INTO TutorialAppSchema.Computer(
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES('" + myComputer.Motherboard 
                    + "' ,'" + myComputer.HasWifi
                    + "' ,'" + myComputer.HasLTE
                    + "' ,'" + myComputer.ReleaseDate
                    + "' ,'" + myComputer.Price
                    + "' ,'" + myComputer.VideoCard 
                    + "')";
           // Console.WriteLine(sql);

            //  int result =    dbConnection.Execute(sql);
            //  Console.WriteLine(result);
            bool result = dapper.ExecuteSql(sql);

            string sqlSelect = @"
        SELECT
              Computer.ComputerId,
              Computer.Motherboard,
              Computer.HasWifi,
              Computer.HasLTE,
              Computer.ReleaseDate,
              Computer.Price,
              Computer.VideoCard
        FROM  TutorialAppSchema.Computer";
           
            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("'ComputerId','Motherboard','HasWifi' ,'HasLTE','ReleaseDate','Price'"
                       + " ,'VideoCard'");
            foreach (Computer singleComputer in computers)
            {
                Console.WriteLine("'" + singleComputer.ComputerId
                        + "' ,'" + singleComputer.Motherboard
                        + "' ,'" + singleComputer.HasWifi
                        + "' ,'" + singleComputer.HasLTE
                        + "' ,'" + singleComputer.ReleaseDate
                        + "' ,'" + singleComputer.Price
                        + "' ,'" + singleComputer.VideoCard + "'");

            }

            Console.WriteLine("---------------------------------------------");
            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();

            if(computersEf != null){

            Console.WriteLine("'ComputerId','Motherboard','HasWifi' ,'HasLTE','ReleaseDate','Price'"
                       + " ,'VideoCard'");
            foreach (Computer iComputer in computersEf)
            {
                Console.WriteLine("'" + iComputer.ComputerId
                         + "' ,'" + iComputer.Motherboard
                        + "' ,'" + iComputer.HasWifi
                        + "' ,'" + iComputer.HasLTE
                        + "' ,'" + iComputer.ReleaseDate
                        + "' ,'" + iComputer.Price
                        + "' ,'" + iComputer.VideoCard + "'");

                    }
            }



        }



    }
}


