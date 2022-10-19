using System;
using System.Collections.Generic;
using PromotionEngine.Base;
using PromotionEngine.Billing;
using PromotionEngine.Child;

namespace PromotionEngine
{
    internal class Program
    {
        private static void Main()
        {
            var stocks = new List<StockKeepingUnit>();

            var stockA = new StockKeepingUnit("A", 50, 3);
            var stockB = new StockKeepingUnit("B", 30, 5);
            var stockC = new StockKeepingUnit("C", 20, 5);
            var stockD = new StockKeepingUnit("D", 15, 1);

            stockA.CurrentPromotions = new ComboOffer(stockA, 130, 3);
            stockB.CurrentPromotions = new ComboOffer(stockB, 45, 2);
            stockD.CurrentPromotions = new PairOffer(stockD, 30, stockC);
            stocks.Add(stockA);
            stocks.Add(stockB);
            stocks.Add(stockC);
            stocks.Add(stockD);

            var bill = new CheckOut();
            bill.PrintBill(stocks);

            Console.ReadLine();
        }
    }
}