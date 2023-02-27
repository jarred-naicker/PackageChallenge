using Microsoft.AspNetCore.Mvc;

namespace com.mobiquity.packer.api.Controllers;

[ApiController]
[Route("[controller]")]
public class PackerController : ControllerBase
{
    private readonly Packer _packer;

    public PackerController(Packer packer)
    {
        _packer = packer;
    }

    [HttpGet("{input}")]
    public IActionResult Pack(string input)
    {
        var packed = _packer.pack(input);
        return Ok(packed);
    }
}