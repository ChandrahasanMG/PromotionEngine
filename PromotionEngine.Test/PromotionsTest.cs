using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PromotionEngine.Base;
using PromotionEngine.Child;
using PromotionEngine.Constants;

namespace PromotionEngine.Test
{
    [TestClass]
    public class PromotionsTest
    {
        #region variables

        private StockKeepingUnit _stockA;
        private StockKeepingUnit _stockB;
        private StockKeepingUnit _stockC;
        private StockKeepingUnit _stockD;

        #endregion

        #region TestCases

        #region Postive usecases

        [TestMethod]
        public void ValidComboOffer()
        {
            try
            {
                const int expectedOutput = 50;
                _stockA = new StockKeepingUnit("A", PromotionEngineConstants.SkuAPricePerUnit, 1);
                _stockA.CurrentPromotions = new ComboOffer(_stockA, 130, 3);
                Assert.AreEqual(expectedOutput, _stockA.CurrentPromotions.GetPrice());
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception raised :" + ex.Message);
            }
        }

        [TestMethod]
        public void ValidComboOfferPlus()
        {
            try
            {
                const int expectedOutput = 310;
                _stockA = new StockKeepingUnit("A", PromotionEngineConstants.SkuAPricePerUnit, 1);
                _stockA = new StockKeepingUnit("A", _stockA.PricePerUnit, 7);
                _stockA.CurrentPromotions = new ComboOffer(_stockA, 130, 3);
                Assert.AreEqual(_stockA.CurrentPromotions.GetPrice(), expectedOutput);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception raised :" + ex.Message);
            }
        }

        [TestMethod]
        public void ValidPairOfferPlus()
        {
            try
            {
                const int expectedOutput = 50;
                _stockD = new StockKeepingUnit("D", PromotionEngineConstants.SkuDPricePerUnit, 1);
                _stockC = new StockKeepingUnit("C", PromotionEngineConstants.SkuCPricePerUnit, 2);
                _stockD.CurrentPromotions = new PairOffer(_stockD, 30, _stockC);
                Assert.AreEqual(_stockD.CurrentPromotions.GetPrice(), expectedOutput);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception raised :" + ex.Message);
            }
        }


        [TestMethod]
        public void ValidPairOffer()
        {
            try
            {
                const int expectedOutput = 110;
                _stockC = new StockKeepingUnit("C", PromotionEngineConstants.SkuCPricePerUnit, 5);
                _stockD = new StockKeepingUnit("D", PromotionEngineConstants.SkuDPricePerUnit, 1);
                _stockD.CurrentPromotions = new PairOffer(_stockD, 30, _stockC);
                Assert.AreEqual(_stockD.CurrentPromotions.GetPrice(), expectedOutput);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception raised :" + ex.Message);
            }
        }

        #endregion

        #region Neagative usecases 

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "An exception is raised due to invalid SKU object which is to be thrown ")]
        public void DuplicatePairObject()
        {
            _stockA = new StockKeepingUnit("A", PromotionEngineConstants.SkuAPricePerUnit, 5);
            _stockA.CurrentPromotions = new PairOffer(_stockA, 30, _stockA);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "An exception is raised due to invalid SKU object which is to be thrown ")]
        public void DuplicatePairOfferRegistration()
        {
            _stockC = new StockKeepingUnit("C", PromotionEngineConstants.SkuCPricePerUnit, 5);
            _stockD = new StockKeepingUnit("D", PromotionEngineConstants.SkuDPricePerUnit, 5);
            _stockD.CurrentPromotions = new PairOffer(_stockD, 30, _stockC);
            _stockC.CurrentPromotions = new PairOffer(_stockC, 30, _stockD);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "An exception is raised due to invalid SKU object which is to be thrown ")]
        public void DuplicateOffer()
        {
            _stockA = new StockKeepingUnit("A", PromotionEngineConstants.SkuAPricePerUnit, 3);
            _stockA.CurrentPromotions = new ComboOffer(_stockA, 130, 3);
            _stockB = new StockKeepingUnit("B", PromotionEngineConstants.SkuBPricePerUnit, 5);
            _stockB.CurrentPromotions = new ComboOffer(_stockA, 48, 2);
        }

        #endregion

        #endregion
    }
}
