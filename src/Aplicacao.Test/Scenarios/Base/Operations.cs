using System.Net.Http;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Aplicacao.Test.Scenarios.Base.Query
{
    [Order(0)]
    public abstract class Operations  : BaseTest
    {
        public void Configure(string controllers, string id, string fieldOrderBy)
            => base.Configure(controllers, id, fieldOrderBy);

        public bool AssertTest { get; set; } = true;

        private void AssertMethod()
        {
            if (AssertTest)
            {
                Assert.True(IsSuccess);
            }
            else
            {
                Assert.False(IsSuccess);
            }
        }

        [Fact, Order(0)]
        public void GetAllTest()
        {
            base.GetAll();

            AssertMethod();
        }

        [Fact, Order(1)]
        public void GetTest()
        {
            base.Get();

            AssertMethod();
        }
    }
}

namespace Aplicacao.Test.Scenarios.Base.CRUD
{
    [Order(1)]
    public abstract class Operations  : BaseTest
    {
        public bool AssertTest { get; set; } = true;
        public void AssertMethod()
        {
            if (AssertTest)
            {
                Assert.True(IsSuccess);
            }
            else
            {
                Assert.False(IsSuccess);
            }
        }

        [Fact, Order(0)]
        public virtual void GetAllTest()
        {
            base.GetAll();

            AssertMethod();
        }

        [Fact, Order(1)]
        public virtual void GetTest()
        {
            base.Get();

            AssertMethod();
        }

        [Fact, Order(2)]
        public virtual void PostTest()
        {
            base.Post();

            AssertMethod();

            base.Delete();
        }

        [Fact, Order(3)]
        public virtual void PutTest()
        {
            base.Post();

            base.Put();

            AssertMethod();

            base.Delete();
        }

        [Fact, Order(4)]
        public virtual void DeleteTest()
        {
            base.Post();

            base.Put();

            base.Delete();

            AssertMethod();
        }

        internal void SendPut()
        {
            SendTest(HttpMethod.Put);
        }

        internal void SendPost()
        {
            SendTest(HttpMethod.Post);
        }

        internal void SendDelete()
        {
            SendTest(HttpMethod.Delete);
        }
    }
}