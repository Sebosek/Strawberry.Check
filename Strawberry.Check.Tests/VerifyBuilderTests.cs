using System;

using Moq;

using Strawberry.Check.Interfaces;

using Xunit;

namespace Strawberry.Check.Tests
{
    public class VerifyBuilderTests
    {
        [Fact]
        public void Ctor_NullObject_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new CheckBuilder<object>(null, nameof(Object)));
        }
        
        [Fact]
        public void Ctor_NullMessage_ThrowsArgumentNullException()
        {
            var o = new object();
            
            Assert.Throws<ArgumentNullException>(() => 
                new CheckBuilder<object>(o, null));
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" \n")]
        public void Ctor_InvalidMessage_ThrowsArgumentNullException(string name)
        {
            var o = new object();
            
            Assert.Throws<ArgumentException>(() => 
                new CheckBuilder<object>(o, name));
        }
        
        [Fact]
        public void Ctor_ValidInput_Success()
        {
            var o = new object();
            
            var builder = new CheckBuilder<object>(o, nameof(o));

            Assert.NotNull(builder.Obj);
            Assert.Empty(builder.Exceptions());
        }
        
        [Fact]
        public void AddVerification_MockVerifyWithException_ContainsException()
        {
            BuilderWithVerification(builder =>
            {
                Assert.NotEmpty(builder.Exceptions());
            }, new Exception());
        }
        
        [Fact]
        public void AddVerification_MockVerifyWithoutException_NoException()
        {
            BuilderWithVerification(builder =>
            {
                Assert.Empty(builder.Exceptions());
            }, new Exception[] { null });
        }
        
        [Fact]
        public void Check_SingleException_GivenException()
        {
            BuilderWithVerification(builder =>
            {
                Assert.Throws<Exception>(builder.Verify);
            }, new Exception());
        }
        
        [Fact]
        public void Check_MultipleExceptions_AggregateException()
        {
            BuilderWithVerification(builder =>
            {
                Assert.Throws<AggregateException>(builder.Verify);
            }, new Exception(), new Exception());
        }
        
        [Fact]
        public void Check_MultipleExceptions_CorrectNumberOfException()
        {
            BuilderWithVerification(builder =>
            {
                try
                {
                    builder.Verify();
                }
                catch (AggregateException e)
                {
                    Assert.Equal(2, e.InnerExceptions.Count);
                }
            }, new Exception(), new Exception());
        }
        
        private static void BuilderWithVerification(
            Action<CheckBuilder<object>> fnc, 
            params Exception[] exceptions)
        {
            var o = new object();
            var builder = new CheckBuilder<object>(o, nameof(o));
            foreach (var e in exceptions)
            {
                var mock = new Mock<ICheck>();
                
                mock.Setup(s => s.GetException()).Returns(e);
                builder.AddVerification(mock.Object);
            }
            
            fnc(builder);
        }
    }
}