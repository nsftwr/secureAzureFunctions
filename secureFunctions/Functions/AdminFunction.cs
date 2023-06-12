using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using secureFunctions.Authorization;

namespace secureFunctions.Functions;

[Authorize(
    Scopes = new[] { Scopes.FunctionsAccess },
    UserRoles = new[] { UserRoles.Admin })]
public static class AdminFunction
{
    [Function("AdminFunction")]
    public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString("Authorized!");
        return response;
    }
}