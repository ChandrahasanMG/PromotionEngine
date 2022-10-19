using PromotionEngine.Interface;

namespace PromotionEngine.Base
{
    public class StockKeepingUnit
    {
        /// <summary>
        ///     StockKeepingUnit Constructor
        /// </summary>
        /// <param name="id">SKU ID</param>
        /// <param name="pricePerUnit">Price per unit</param>
        /// <param name="quantity"></param>
        public StockKeepingUnit(string id, long pricePerUnit, long quantity)
        {
            Id = id;
            PricePerUnit = pricePerUnit;
            Quantity = quantity;
        }


        /// <summary>
        ///     Promotion interface object
        ///     In-case of pair offer , assign value to any one of SKU object only
        /// </summary>
        public iPromotions CurrentPromotions { get; set; }

        #region Variables

        public readonly string Id;
        public readonly long PricePerUnit;
        public readonly long Quantity;
        public bool IsOfferApplied;

        #endregion
    }
}