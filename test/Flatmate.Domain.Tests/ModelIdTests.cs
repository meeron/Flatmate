using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flatmate.Domain.Tests
{
    public class ModelIdTests
    {
        [Fact]
        public void Empty()
        {
            Assert.Equal("AAAAAAAA", ModelId.Empty.ToString());
        }

        [Fact]
        public void Generate()
        {
            ModelId id = ModelId.NewId();

            Assert.Equal(8, id.ToString().Length);
        }

        [Fact]
        public void Compare()
        {
            ModelId empty = ModelId.Empty;
            ModelId id = ModelId.NewId();

            Assert.True(empty == ModelId.Empty);
            Assert.True(id != ModelId.Empty);
            Assert.True(id > empty);
            Assert.True(id >= empty);
            Assert.True(empty < id);
            Assert.True(empty <= id);
        }

        [Fact]
        public void GetHashCode_Test()
        {
            Dictionary<ModelId, string> _values = new Dictionary<ModelId, string>();
            int count = 1000000;
            ModelId id;
            for (int i = 0; i < count; i++)
            {
                id = ModelId.NewId();
                _values.Add(id, id.ToString());
            }

            Assert.True(true);      
        }

        [Fact]
        public void Uniquenes_Test()
        {
            Dictionary<string, string> _values = new Dictionary<string, string>();
            int count = 1000000;
            ModelId id;
            for (int i = 0; i < count; i++)
            {
                id = ModelId.NewId();
                _values.Add(id.ToString(), id.ToString());
            }

            Assert.True(true);
        }

        [Fact]
        public void Parse()
        {
            ModelId id = ModelId.NewId();

            Assert.Equal(id, ModelId.Parse(id.ToString())); 
        }

        [Fact]
        public void Parse_Error()
        {
            Exception ex = Assert.Throws<FormatException>(() => ModelId.Parse("sdfsdfsdfsdfsdfsdfsdfsdfdf"));
            Assert.Equal("Text 'sdfsdfsdfsdfsdfsdfsdfsdfdf' is invalid for ModelId type.", ex.Message);
        }

        [Fact]
        public void TryParse()
        {
            ModelId id = ModelId.NewId();
            ModelId id2 = ModelId.Empty;
            ModelId id3 = ModelId.Empty;

            bool valid = ModelId.TryParse(id.ToString(), out id2);
            bool invalid = ModelId.TryParse("test", out id3);

            Assert.True(valid);
            Assert.False(invalid);
            Assert.Equal(id, id2);
            Assert.Equal(ModelId.Empty, id3);
        }

        [Fact]
        public void ToByteArray()
        {
            ModelId id = ModelId.NewId();

            Assert.Equal(id, new ModelId(id.ToByteArray()));
        }
    }
}
