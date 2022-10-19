using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.Base;
using System.Collections.Generic;
using PromotionEngine.Billing;
using PromotionEngine.Child;
using PromotionEngine.Constants;

namespace PromotionEngine.Test
{
    /// <summary>
    /// Summary description for CheckOutTest
    /// </summary>
    [TestClass]
    public class CheckOutTest
    {
        #region Variables

        readonly List<StockKeepingUnit> _stocks = new List<StockKeepingUnit>();

        private StockKeepingUnit _stockA;
        private StockKeepingUnit _stockB;
        private StockKeepingUnit _stockC;
        private StockKeepingUnit _stockD;

        #endregion

        #region Helper methods

        /// <summary>
        /// Set default offers 
        /// </summary>
        private void SetOffers()
        {
            if (_stockA != null)
            {
                _stockA.CurrentPromotions = new ComboOffer(_stockA, 130, 3);
            }

            if (_stockB != null)
            {
                _stockB.CurrentPromotions = new ComboOffer(_stockB, 45, 2);
            }

            if (_stockC != null && _stockD != null)
            {
                _stockD.CurrentPromotions = new PairOffer(_stockD, 30, _stockC);
            }
        }

        /// <summary>
        /// Get total of the items in cart (SKU collection)
        /// </summary>
        /// <returns></returns>
        private long GetItemsSum()
        {
            long totalSum = 0;
            CheckOut bill = new CheckOut();

            foreach (var item in _stocks)
            {
                totalSum += bill.GetSkuPrice(item);
            }

            return totalSum;
        }

        #endregion

        #region TestCases

        [TestMethod]
        public void SimpleCart()
        {
            const long expectedSum = 100;

            _stockA = new StockKeepingUnit("A",PromotionEngineConstants.SkuAPricePerUnit, 1);
            _stockB = new StockKeepingUnit("B", PromotionEngineConstants.SkuBPricePerUnit, 1);
            _stockC = new StockKeepingUnit("C", PromotionEngineConstants.SkuCPricePerUnit, 1);
            SetOffers();
            _stocks.Clear();
            _stocks.Add(_stockA);
            _stocks.Add(_stockB);
            _stocks.Add(_stockC);
            Assert.AreEqual(expectedSum, GetItemsSum());
        }

        [TestMethod]
        public void MultipleItemCart()
        {
            const long expectedSum = 370;

            _stockA = new StockKeepingUnit("A", PromotionEngineConstants.SkuAPricePerUnit, 5);
            _stockB = new StockKeepingUnit("B", PromotionEngineConstants.SkuBPricePerUnit, 5);
            _stockC = new StockKeepingUnit("C", PromotionEngineConstants.SkuCPricePerUnit, 1);
            SetOffers();
            _stocks.Clear();
            _stocks.Add(_stockA);
            _stocks.Add(_stockB);
            _stocks.Add(_stockC);
            Assert.AreEqual(expectedSum, GetItemsSum());
        }

        [TestMethod]
        public void ComplexItemCart()
        {
            const long expectedSum = 280;

            _stockA = new StockKeepingUnit("A", PromotionEngineConstants.SkuAPricePerUnit, 3);
            _stockB = new StockKeepingUnit("B", PromotionEngineConstants.SkuBPricePerUnit, 5);
            _stockC = new StockKeepingUnit("C", PromotionEngineConstants.SkuCPricePerUnit, 1);
            _stockD = new StockKeepingUnit("D", PromotionEngineConstants.SkuDPricePerUnit, 1);
            SetOffers();
            _stocks.Clear();
            _stocks.Add(_stockA);
            _stocks.Add(_stockB);
            _stocks.Add(_stockC);
            _stocks.Add(_stockD);
            Assert.AreEqual(expectedSum, GetItemsSum());
        }

        [TestMethod]
        public void ComplexItemCartPlus()
        {
            const long expectedSum = 300;

            _stockA = new StockKeepingUnit("A", PromotionEngineConstants.SkuAPricePerUnit, 3);
            _stockB = new StockKeepingUnit("B", PromotionEngineConstants.SkuBPricePerUnit, 5);
            _stockC = new StockKeepingUnit("C", PromotionEngineConstants.SkuCPricePerUnit, 2);
            _stockD = new StockKeepingUnit("D", PromotionEngineConstants.SkuDPricePerUnit, 1);
            SetOffers();
            _stocks.Clear();
            _stocks.Add(_stockA);
            _stocks.Add(_stockB);
            _stocks.Add(_stockC);
            _stocks.Add(_stockD);
            Assert.AreEqual(expectedSum, GetItemsSum());
        }

        #endregion
    }
}
