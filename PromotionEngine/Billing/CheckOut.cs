using System;
using System.Collections.Generic;
using System.Diagnostics;
using PromotionEngine.Base;

namespace PromotionEngine.Billing
{
    public class CheckOut
    {
        /// <summary>
        ///     Print Current cart bill
        /// </summary>
        /// <param name="listOfItems"></param>
        public void PrintBill(List<StockKeepingUnit> listOfItems)
        {
            try
            {
                long total = 0;
                foreach (var item in listOfItems)
                    if (item.Quantity > 0)
                    {
                        var currentItemPrice = GetSkuPrice(item);
                        Console.Write($"{item.Quantity} * {item.Id}");
                        Console.Write($"      {currentItemPrice}");
                        Console.WriteLine(string.Empty);
                        total += currentItemPrice;
                    }

                Console.WriteLine("=======");
                Console.WriteLine(total);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex.Message} occurred in CheckOut.PrintBill() API");
            }
        }

        /// <summary>
        ///     Get Sku Price
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public long GetSkuPrice(StockKeepingUnit sku)
        {
            var currentItemPrice = sku.CurrentPromotions?.GetPrice() ?? (sku.IsOfferApplied
                ? 0
                : sku.PricePerUnit * sku.Quantity);

            return currentItemPrice;
        }
    }
}