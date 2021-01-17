using System;
using Xunit;

namespace Strawberry.Check.Tests
{
    public class VerifyTests
    {
        [Fact]
        public void Object_CreatesVerifyBuilder()
        {
            var o = new {};
            
            var builder = Check.Object(o, nameof(o));
            
            Assert.NotNull(builder);
        }
        
        [Fact]
        public void NotNull_Instance_NoExceptionThrown()
        {
            Exception ex = null;
            var o = new {};
            
            try
            {
                Check.NotNull(o, nameof(o));
            }
            catch (Exception e)
            {
                ex = e;
            }
            
            Assert.Null(ex);
        }

        [Fact]
        public void NotNull_NollObject_ArgumentNullException()
        {
            object o = null;

            Assert.Throws<ArgumentNullException>(() => Check.NotNull(o, nameof(o)));
        }
    }
}