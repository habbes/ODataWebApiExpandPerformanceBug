using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using TestExpandPerformanceBug.Models;

namespace TestExpandPerformanceBug.Controllers
{
    public class DummyObjectController : ODataController
    {
        [EnableQuery(EnableCorrelatedSubqueryBuffering = true)]
        public virtual IActionResult Get(ODataQueryOptions<DummyObject1Sub> queryOptions)
        {
            IList<DummyObject1Sub> list = new List<DummyObject1Sub>();
            for (int i = 0; i < 2000; i++)
            {
                DummyObject2 d2 = new DummyObject2()
                {
                    Id = i,
                    Description = $"desc {i}",
                    Field1 = "aaa",
                    Field2 = "aaa",
                    Field3 = "aaa",
                    Field4 = "aaa",
                    Field5 = "aaa",
                    Field6 = "aaa",
                    Field7 = "aaa",
                };

                DummyObject1Sub d1 = new DummyObject1Sub()
                {
                    Id = i,
                    Description = $"desc {i}",
                    Field1 = "aaa",
                    Field2 = "aaa",
                    Field3 = "aaa",
                    Field4 = "aaa",
                    Field5 = "aaa",
                    Field6 = "aaa",
                    Field7 = "aaa",
                    Field8 = d2
                };

                list.Add(d1);
            }

            return Ok(list.AsQueryable());
        }
    }
}