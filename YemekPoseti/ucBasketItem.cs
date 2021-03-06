﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YemekPoşeti
{
	partial class ucBasketItem : UserControl
	{
		public int FoodID { get; set; }
		public int QTY = 1;
		public float Price { get; set; }
		public string FoodName { get; set; }
		public string FoodDesc { get; set; }
		public float SumPrice { get; set; }

		public MainScreen ms;

		public ucBasketItem()
		{
			InitializeComponent();
		}
		public void UpdateBasketItem()
		{
			this.SumPrice = Price * this.QTY;
			this.lblFoodPrice.Text = this.SumPrice.ToString("0.00") + " TL";
			this.lblFoodName.Text = this.FoodName + " x" + this.QTY;
			this.lblDeleteFood.Location = new Point(this.lblFoodName.Location.X + 5 + this.lblFoodName.Width, this.lblDeleteFood.Location.Y);
			this.lblFoodDesc.Text = this.FoodDesc;
            ms.CurrentOrder.PrintFoods(ms.lboxUrunler);
        }

		private void lblDeleteFood_Click(object sender, EventArgs e)
		{
            ms.CurrentOrder.Basket.RemoveFood(this);
		}
	}
}
