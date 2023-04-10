using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class BuggyController : BaseApiController
{
    private readonly StoreContext context;

    public BuggyController(StoreContext context)
    {
        this.context = context;
    }
    [HttpGet("notfound")]
    public ActionResult GetNotFoundError()
    {
        var thing = context.Products.Find(404);
        if(thing == null)
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("servererror")]
    public ActionResult getServerError()
    {
        var thing = context.Products.Find(444);
        var thingReturn = thing.ToString();
        return Ok();    
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
        return BadRequest(new ApiResponse(400));
    }
}