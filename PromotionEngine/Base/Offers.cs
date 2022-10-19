using System;
using PromotionEngine.Constants;

namespace PromotionEngine.Base
{
    public abstract class Offers
    {
        /// <summary>
        ///     Offers Constructor
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="offerPrice"></param>
        protected Offers(StockKeepingUnit sku, long offerPrice)
        {
            if (sku != null)
            {
                Sku = sku;
                OfferPrice = offerPrice;
            }
            else
            {
                throw new ArgumentException(PromotionEngineConstants.SkuObjectExceptionNullMessage);
            }
        }

        #region Varibles

        protected long OfferPrice;
        protected StockKeepingUnit Sku;

        #endregion
    }
}