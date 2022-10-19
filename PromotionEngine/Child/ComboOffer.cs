using System;
using System.Diagnostics;
using PromotionEngine.Base;
using PromotionEngine.Constants;
using PromotionEngine.Interface;

namespace PromotionEngine.Child
{
    public class ComboOffer : Offers, iPromotions
    {
        #region variables

        private readonly long _minimumCount;

        #endregion

        /// <summary>
        ///     ComboOffer Constructor
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="minimumCount"></param>
        /// <param name="offerPrice"></param>
        public ComboOffer(StockKeepingUnit sku, long offerPrice, long minimumCount) : base(sku, offerPrice)
        {
            if (!Sku.IsOfferApplied)
            {
                _minimumCount = minimumCount;
                Sku.IsOfferApplied = true;
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
            long priceAfterDiscount = 0;
            try
            {
                if (Sku != null)
                {
                    if (Sku.Quantity != 0 && Sku.Quantity >= _minimumCount)
                    {
                        var discountPair = Sku.Quantity / _minimumCount;
                        var discountPrice = OfferPrice * discountPair;
                        priceAfterDiscount = discountPrice +
                                             (Sku.Quantity - discountPair * _minimumCount) * Sku.PricePerUnit;
                    }
                    else
                    {
                        priceAfterDiscount = Sku.PricePerUnit * Sku.Quantity;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception {ex.Message} occurred in ComboOffer.GetPrice() API");
            }

            return priceAfterDiscount;
        }
    }
}