using System;
using System.Diagnostics;
using PromotionEngine.Base;
using PromotionEngine.Constants;
using PromotionEngine.Interface;

namespace PromotionEngine.Child
{
    public class PairOffer : Offers, iPromotions
    {
        #region Variables

        private readonly StockKeepingUnit _pairStockKeepingUnit;

        #endregion

        /// <summary>
        ///     Pair offer , to add offer to one of the SKU class only
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="offerPrice"></param>
        /// <param name="pairSku"></param>
        public PairOffer(StockKeepingUnit sku, long offerPrice, StockKeepingUnit pairSku) : base(sku, offerPrice)
        {
            if (sku != pairSku && !pairSku.IsOfferApplied)
            {
                _pairStockKeepingUnit = pairSku;
                _pairStockKeepingUnit.IsOfferApplied = true;
                _pairStockKeepingUnit.IsOfferApplied =
                    Sku.IsOfferApplied = Sku.Quantity > 0 && _pairStockKeepingUnit.Quantity > 0;
            }
            else
            {
                throw new ArgumentException(PromotionEngineConstants.SkuObjectExceptionMessage);
            }
        }


        /// <summary>
        ///     Calculate product price
        /// </summary>
        /// <returns>price</returns>
        public long GetPrice()
        {
            long price = 0;
            try
            {
                if (Sku != null && _pairStockKeepingUnit != null)
                {
                    if (Sku?.Quantity > 0 && _pairStockKeepingUnit?.Quantity > 0)
                    {
                        var minValue = Math.Min(Sku.Quantity, _pairStockKeepingUnit.Quantity);
                        var maxValue = Math.Max(Sku.Quantity, _pairStockKeepingUnit.Quantity);
                        var pairPrice = minValue * OfferPrice;

                        price = pairPrice;

                        if (minValue != maxValue)
                        {
                            var difference = maxValue - minValue;

                            price += maxValue == Sku.Quantity
                                ? difference * Sku.PricePerUnit
                                : difference * _pairStockKeepingUnit.PricePerUnit;
                        }
                    }
                    else
                    {
                        price = Sku.PricePerUnit * Sku.Quantity;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex.Message} occurred in PairOffer.GetPrice() API");
            }

            return price;
        }
    }
}