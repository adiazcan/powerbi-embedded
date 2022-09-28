using System.Text.Json;

namespace API.Services;

public class Embed 
{
    private readonly PbiEmbedService pbiService;

    public Embed(PbiEmbedService pbiService)
    {
        this.pbiService = pbiService;
    }

    public string GetReportEmbedToken(Guid reportId, Guid workspaceId)
    {
        var token = pbiService.GetEmbedToken(reportId, workspaceId);

        return JsonSerializer.Serialize(token);
    }
}