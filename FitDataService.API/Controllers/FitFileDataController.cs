using AutoMapper;
using FitDataService.API.DTOs;
using FitDataService.API.DTOs.RecordData;
using FitDataService.Application.DTOs;
using FitDataService.Application.DTOs.RecordData;
using FitDataService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitDataService.API.Controllers;

[ApiController]
[Route("api/fit-file-data")]
[Produces("application/json")]
public class FitFileDataController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IFitFileDataService _fitFileDataService;

    public FitFileDataController(
        IMapper mapper,
        IFitFileDataService fitFileDataService)
    {
        _mapper = mapper;
        _fitFileDataService = fitFileDataService;
    }

    [HttpGet("file-id/{hikingTrailCode:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<FileIdDto>> GetFileId([FromRoute] Guid hikingTrailCode)
    {
        FiledIdEntityDto fileId = 
            await _fitFileDataService.GetFileIdByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<FileIdDto>(fileId));
    }
    
    [HttpGet("activity/{hikingTrailCode:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ActivityDto>> GetActivity([FromRoute] Guid hikingTrailCode)
    {
        ActivityEntityDto activity = 
            await _fitFileDataService.GetActivityByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<ActivityDto>(activity));
    }
    
    [HttpGet("sessions/{hikingTrailCode:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<SessionEntityDto> sessions = 
            await _fitFileDataService.GetSessionsByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<SessionDto>>(sessions));
    }
    
    [HttpGet("laps/{hikingTrailCode:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LapDto>>> GetLaps([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<LapEntityDto> laps = 
            await _fitFileDataService.GetLapsByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<LapDto>>(laps));
    }
    
    [HttpGet("records/{hikingTrailCode:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RecordDto>>> GetRecords([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<RecordEntityDto> records = 
            await _fitFileDataService.GetRecordsByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<RecordDto>>(records));
    }
    
    [HttpGet("records/{hikingTrailCode:guid}/coordinates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CoordinateDto>>> GetCoordinates([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<CoordinateEntityDto> coordinates = 
            await _fitFileDataService.GetCoordinatesByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<CoordinateDto>>(coordinates));
    }
    
    [HttpGet("records/{hikingTrailCode:guid}/graphics-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GraphicsDataDto>>> GetGraphicsData([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<GraphicsDataEntityDto> graphicsData = 
            await _fitFileDataService.GetGraphicsDataByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<GraphicsDataDto>>(graphicsData));
    }
    
    [HttpGet("records/{hikingTrailCode:guid}/altitude")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GraphicsDataDto>>> GetAltitudes([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<GraphicsDataEntityDto> altitudes = 
            await _fitFileDataService.GetAltitudesByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<GraphicsDataDto>>(altitudes));
    }
    
    [HttpGet("records/{hikingTrailCode:guid}/hearth-rate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GraphicsDataDto>>> GetHearthRates([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<GraphicsDataEntityDto> hearthRates = 
            await _fitFileDataService.GetHearthRatesByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<GraphicsDataDto>>(hearthRates));
    }
    
    [HttpGet("records/{hikingTrailCode:guid}/cadence")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GraphicsDataDto>>> GetCadences([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<GraphicsDataEntityDto> cadences = 
            await _fitFileDataService.GetCadencesByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<GraphicsDataDto>>(cadences));
    }
    
    [HttpGet("records/{hikingTrailCode:guid}/distance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GraphicsDataDto>>> GetDistances([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<GraphicsDataEntityDto> distances = 
            await _fitFileDataService.GetDistancesByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<GraphicsDataDto>>(distances));
    }
    
    [HttpGet("records/{hikingTrailCode:guid}/speed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GraphicsDataDto>>> GetSpeed([FromRoute] Guid hikingTrailCode)
    {
        IEnumerable<GraphicsDataEntityDto> speeds = 
            await _fitFileDataService.GetSpeedByHikingTrailCodeAsync(hikingTrailCode);

        return Ok(_mapper.Map<IEnumerable<GraphicsDataDto>>(speeds));
    }
    
}