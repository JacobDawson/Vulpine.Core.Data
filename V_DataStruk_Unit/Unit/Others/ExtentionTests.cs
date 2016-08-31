using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data;
using Vulpine.Core.Data.Extentions;

namespace Vulpine_Core_Data_Tests.Unit.Others
{
    [TestFixture]
    public class ExtentionTests
    {
        private const double Delta = 1.0e-10;

        [Test]
        public void Median_NormalData_ExpectedValue()
        {
            double mid = DataSets.Normal.Median();
            double ex = 5.5391805597;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Median_UniformData_ExpectedValue()
        {
            double mid = DataSets.Uniform.Median();
            double ex = 0.5558026857;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Median_LogNormalData_ExpectedValue()
        {
            double mid = DataSets.LogNorm.Median();
            double ex = 0.9841876365;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Median_LaplaceData_ExpectedValue()
        {
            double mid = DataSets.Laplace.Median();
            double ex = 2.9738015369;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Median_ExponentialData_ExpectedValue()
        {
            double mid = DataSets.Exponential.Median();
            double ex = 0.1499697864;

            Assert.AreEqual(ex, mid, Delta);
        }



        [Test]
        public void Percentile_NormalData_ExpectedQ1()
        {
            double mid = DataSets.Normal.Percentile(0.25);
            double ex = 4.5640913664;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Percentile_UniformData_ExpectedQ1()
        {
            double mid = DataSets.Uniform.Percentile(0.25);
            double ex = 0.3023233226;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Percentile_LogNormData_ExpectedQ1()
        {
            double mid = DataSets.LogNorm.Percentile(0.25);
            double ex = 0.7490551431;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Percentile_LaplaceData_ExpectedQ1()
        {
            double mid = DataSets.Laplace.Percentile(0.25);
            double ex = 2.6709470573;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Percentile_ExponentialData_ExpectedQ1()
        {
            double mid = DataSets.Exponential.Percentile(0.25);
            double ex = 0.0657662296;

            Assert.AreEqual(ex, mid, Delta);
        }



        [Test]
        public void Percentile_NormalData_ExpectedP90()
        {
            double mid = DataSets.Normal.Percentile(0.9);
            double ex = 6.8787414547;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Percentile_UniformData_ExpectedP90()
        {
            double mid = DataSets.Uniform.Percentile(0.9);
            double ex = 0.9410182009;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Percentile_LogNormData_ExpectedP90()
        {
            double mid = DataSets.LogNorm.Percentile(0.9);
            double ex = 1.8891244752;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Percentile_LaplaceData_ExpectedP90()
        {
            double mid = DataSets.Laplace.Percentile(0.9);
            double ex = 3.6386089505;

            Assert.AreEqual(ex, mid, Delta);
        }

        [Test]
        public void Percentile_ExponentialData_ExpectedP90()
        {
            double mid = DataSets.Exponential.Percentile(0.9);
            double ex = 0.4575910437;

            Assert.AreEqual(ex, mid, Delta);
        }



        [Test]
        public void Quantile_NormalData_ExpectedResults()
        {
            double[] given = DataSets.Normal.Quantile(10).ToArray();
            double[] ex = { 1.9471531576, 3.7372036803, 4.4163649291, 4.846759738, 5.2601610982,
                5.5391805597, 5.7795678141, 6.0507885012, 6.539814936, 6.8787414547, 8.3323329604 };

            for (int i = 0; i < ex.Length; i++)
            {
                Assert.AreEqual(ex[i], given[i], Delta);
            }
        }

        [Test]
        public void Quantile_UniformData_ExpectedResults()
        {
            double[] given = DataSets.Uniform.Quantile(10).ToArray();
            double[] ex = { 0.0041451644, 0.0872877287, 0.2403573845, 0.3297082192, 0.4263023797,
                0.5558026857, 0.6334787078, 0.7146144045, 0.806018323, 0.9410182009, 0.9990476144};

            for (int i = 0; i < ex.Length; i++)
            {
                Assert.AreEqual(ex[i], given[i], Delta);
            }
        }

        [Test]
        public void Quantile_LogNormData_ExpectedResults()
        {
            double[] given = DataSets.LogNorm.Quantile(10).ToArray();
            double[] ex = { 0.3257033841, 0.5470554969, 0.7133648778, 0.7869946747, 0.8953519742,
                0.9841876365, 1.1283032652, 1.2568812238, 1.4939766968, 1.8891244752, 3.5548327624};

            for (int i = 0; i < ex.Length; i++)
            {
                Assert.AreEqual(ex[i], given[i], Delta);
            }
        }

        [Test]
        public void Quantile_LaplaceData_ExpectedResults()
        {
            double[] given = DataSets.Laplace.Quantile(10).ToArray();
            double[] ex = { 0.7547431959, 2.2797139531, 2.5814151298, 2.7729640822, 2.8712442853,
                2.9738015369, 3.0799898801, 3.1904370038, 3.3551932032, 3.6386089505, 5.2710437217};

            for (int i = 0; i < ex.Length; i++)
            {
                Assert.AreEqual(ex[i], given[i], Delta);
            }
        }

        [Test]
        public void Quantile_ExponentialData_ExpectedResults()
        {
            double[] given = DataSets.Exponential.Quantile(10).ToArray();
            double[] ex = { 0.0008863178, 0.0254339279, 0.0518783853, 0.0816200755, 0.1165567929,
                0.1499697864, 0.1917066891, 0.2309901621, 0.3210801626, 0.4575910437, 1.2888835818};

            for (int i = 0; i < ex.Length; i++)
            {
                Assert.AreEqual(ex[i], given[i], Delta);
            }
        }

    }
}
