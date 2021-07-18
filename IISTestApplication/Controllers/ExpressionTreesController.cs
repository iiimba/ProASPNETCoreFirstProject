using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpressionTreesController : ControllerBase
    {
        [HttpPost("1")]
        public IActionResult Post1()
        {
            Func<int, bool> func1 = num => num < 5;
            var res1 = func1(2);
            res1 = func1(15);

            Expression<Func<int, bool>> expression1 = num => num < 5;
            var func2 = expression1.Compile();
            var res2 = func2(2);
            res2 = func2(15);

            ParameterExpression numParameter = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThenFive = Expression.LessThan(numParameter, five);
            Expression<Func<int, bool>> expression2 = Expression.Lambda<Func<int, bool>>(numLessThenFive, new ParameterExpression[] { numParameter });
            var func3 = expression2.Compile();
            var res3 = func3(2);
            res3 = func3(15);

            return Ok();
        }
    }
}
