using System.Net;
using System.Text.Json;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using secureFunctions.Authorization;

namespace secureFunctions.Functions;

[Authorize(
    Scopes = new[] { Scopes.FunctionsAccess },
    UserRoles = new[] { UserRoles.User, UserRoles.Moderator, UserRoles.Admin })]
public static class UserFunction
{
    [Function("User")]
    public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "GET")] HttpRequestData req,
        FunctionContext executionContext)
    {
        using var connection = new SqlConnection(Environment.GetEnvironmentVariable("DbConnectionString"));
        var data = connection.Query("SELECT * FROM dbo.secfunc").ToList();
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/json; charset=utf-8");
        response.WriteString(JsonSerializer.Serialize(data));
        return response;
    }
}