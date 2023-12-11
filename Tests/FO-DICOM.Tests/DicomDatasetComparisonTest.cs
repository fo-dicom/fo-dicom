using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FellowOakDicom.Tests
{
    [Collection(TestCollections.General)]
    public class DicomDatasetComparisonTest
    {
        [Fact]
        public void Equals_SameObject_ShouldReturnTrue()
        {
            var dataset = new DicomDataset();
            Assert.True(dataset.Equals(dataset));
        }

        [Fact]
        public void Equals_NullObject_ShouldReturnFalse()
        {
            var dataset = new DicomDataset();
            Assert.False(dataset.Equals(null));
            Assert.False(dataset == null);
        }

        [Fact]
        public void Equals_DifferentType_ShouldReturnFalse()
        {
            var dataset = new DicomDataset();
            var otherObject = new object();
            Assert.False(dataset.Equals(otherObject));
        }

        [Fact]
        public void Equals_DifferentEmptyDatasets_ShouldReturnTrue()
        {
            var dataset1 = new DicomDataset();
            var dataset2 = new DicomDataset();
            Assert.True(dataset1.Equals(dataset2));
            Assert.True(dataset1 == dataset2);
        }

        [Fact]
        public void Equals_SameDataDatasets_ShouldReturnTrue()
        {
            var dataset1 = new DicomDataset();
            var dataset2 = new DicomDataset();
            var tag = DicomTag.URNCodeValue;
            dataset1.Add<string>(tag);
            dataset2.Add<string>(tag);

            var dataset3 = new DicomDataset { { DicomTag.SOPInstanceUID, "1.2.3" } };
            var dataset4 = new DicomDataset { { DicomTag.SOPInstanceUID, "1.2.3" } };

            Assert.True(dataset1.Equals(dataset2));
            Assert.True(dataset1 == dataset2);
            Assert.True(dataset3.Equals(dataset4));
            Assert.True(dataset3 == dataset4);
        }

        [Fact]
        public void Equals_SameDataDatasetsWithDifferentAddOrder_ShouldReturnTrue()
        {
            var dataset1 = new DicomDataset();
            var dataset2 = new DicomDataset();
            var tag1 = DicomTag.URNCodeValue;
            var tag2 = DicomTag.SelectorLTValue;
            dataset1.Add<string>(tag1);
            dataset1.Add<string>(tag2);

            dataset2.Add<string>(tag2);
            dataset2.Add<string>(tag1);

            Assert.True(dataset1.Equals(dataset2));
            Assert.True(dataset1 == dataset2);
        }

        [Fact]
        public void Equals_DifferentDataDatasets_ShouldReturnFalse()
        {
            var dataset1 = new DicomDataset { { DicomTag.SOPInstanceUID, "1.2.3" } };
            var dataset2 = new DicomDataset { { DicomTag.SOPInstanceUID, "1.2.3" } };
            var dataset3 = new DicomDataset { { DicomTag.SOPInstanceUID, "1.2.3.4.5.6" } };
            var dataset4 = new DicomDataset { { DicomTag.URNCodeValue, "1.2.3" } };

            Assert.True(dataset1.Equals(dataset2));
            Assert.True(dataset1 == dataset2);
            Assert.False(dataset1.Equals(dataset3));
            Assert.False(dataset1 == dataset3);
            Assert.False(dataset1.Equals(dataset4));
            Assert.False(dataset1 == dataset4);
        }

        [Fact]
        public void InequalityOperator_NullObject_ShouldReturnTrue()
        {
            var dataset = new DicomDataset();
            Assert.True(dataset != null);
        }

        [Fact]
        public void InequalityOperator_DifferentData_ShouldReturnTrue()
        {
            var dataset1 = new DicomDataset();
            var dataset2 = new DicomDataset();
            var tag1 = DicomTag.URNCodeValue;
            var tag2 = DicomTag.SelectorLTValue;
            dataset1.Add<string>(tag1);
            dataset2.Add<string>(tag2);

            Assert.True(dataset1 != dataset2);
        }

        [Fact]
        public void InequalityOperator_SameDataDatasets_ShouldReturnFalse()
        {
            var dataset1 = new DicomDataset();
            var dataset2 = new DicomDataset();
            var tag = DicomTag.URNCodeValue;
            dataset1.Add<string>(tag);
            dataset2.Add<string>(tag);

            Assert.False(dataset1 != dataset2);
        }
    }
}
