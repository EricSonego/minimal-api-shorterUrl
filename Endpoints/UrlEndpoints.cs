using Microsoft.EntityFrameworkCore;
using minimal_api_shorterUrl.Data;
using minimal_api_shorterUrl.Dtos;
using minimal_api_shorterUrl.Models;
using minimal_api_shorterUrl.Services;

namespace minimal_api_shorterUrl.Endpoints;

public static class UrlEndpoints
{
    public static void MapUrlEndpoints(this IEndpointRouteBuilder app)
    {
        // route 1 shorten url
        app.MapPost("/v1/shorten", async (ShortenUrlRequest request, UrlShortenerService service, AppDbContext db, HttpContext httpContext) =>
        {
            // validates if user sent valid url
            if (!Uri.TryCreate(request.LongUrl, UriKind.Absolute, out _))
            {
                return Results.BadRequest("A URL fornecida é inválida.");
            }

            // generate code 6 character
            string shortCode = service.GenerateCode();

            // object into the database
            var urlMapping = new UrlMapping
            {
                IdUrl = Guid.NewGuid(),
                LongUrl = request.LongUrl,
                ShortCode = shortCode,
                CreatedOn = DateTime.UtcNow
            };

            // saved to the db
            db.UrlMappings.Add(urlMapping);
            await db.SaveChangesAsync();

            var req = httpContext.Request;
            var resultUrl = $"{req.Scheme}://{req.Host}/{shortCode}";

            return Results.Ok(new { ShortUrl = resultUrl });
        });

        // route 2 get
        app.MapGet("/{code}", async (string code, AppDbContext db) =>
        {
            // db search
            var mapping = await db.UrlMappings
                .FirstOrDefaultAsync(x => x.ShortCode == code);

            if (mapping == null)
            {
                return Results.NotFound("Link encurtado não encontrado.");
            }

            return Results.Redirect(mapping.LongUrl);
        });
    }
}