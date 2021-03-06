﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace YemekPoşeti
{
    public class Restaurant
    {
        public int ID{ get; set; }

        public int OwnerID { get; set; }

        public string Name { get; set; }
        public int LocationID { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public float MinOrderPrice { get; set; }
        private DB db;
        public ucFoodItem GetFoodList(MySqlDataReader dr,int j)
        {
            
            ucFoodItem ucTempFoodList = new ucFoodItem(dr);
            /* Event Controls */
            /* BG Color */
            if (j % 2 == 0)
                ucTempFoodList.BackColor = Color.FromArgb(255, 245, 255);
            return ucTempFoodList;
        }


        public void GetProperties(int id)
        {
            db = new DB();
            db.Connect();
            this.OwnerID = id;
            string query = String.Format("SELECT * FROM Restaurants WHERE UserID = {0}", this.OwnerID);
            MySqlDataReader dr = db.GetQuery(query);
             
            //set
            if(dr.Read())
            {
                this.ID = Convert.ToInt32(dr["RestaurantID"]);
                this.Name = dr["RestaurantName"].ToString();
                this.LocationID = Convert.ToInt32(dr["LocationID"]);
                this.Rating = Convert.ToSingle(dr["RestaurantRating"]);
                this.MinOrderPrice = Convert.ToSingle(dr["MinOrderPrice"]);
            }
            db.Close();
        }
        public bool SaveProperties(string name, string minOrderPrice, int locationID)
        {
            db = new DB();
            db.Connect();
            this.Name = name;
            this.MinOrderPrice = Convert.ToSingle(minOrderPrice);
            this.LocationID = locationID;

            string query = String.Format("UPDATE Restaurants SET RestaurantName = '{0}', LocationID = {1}," +
                " MinOrderPrice = '{2}' WHERE RestaurantID = {3} ", this.Name, this.LocationID, minOrderPrice, this.ID);

            if(db.SetQuery(query)>0)
            {
                db.Close();
                return true;
            }
            db.Close();
            return false;
        }


        public bool AddFoodToMenu(string name, string foodDesc, string foodPrice)
        {
            db = new DB();
            db.Connect();
            string query = String.Format("INSERT INTO Foods (FoodName,FoodPrice,FoodDesc,FoodCategoryID,RestaurantID)" +
                "VALUES('{0}','{1}','{2}',1,{3})", name, foodPrice, foodDesc, this.ID);

            if (db.SetQuery(query) > 0)
            {
                db.Close();
                return true;
            }
            db.Close();
            return false;

        }


    }
}
