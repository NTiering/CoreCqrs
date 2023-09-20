using Core.Commands.Support;
using Core.Queries.Support;

namespace Core;

public static class Formatters
{
    public static IResult Format(this IQueryResult result)
    {
        if (result == null) return TypedResults.Empty;
        return TypedResults.Ok(result);
    }
    public static IResult Format(this ICommandResult result, string method)
    {

        if (result == null) return TypedResults.Empty;
        if (result.Success)
        {
            if (IsPost(method)) return TypedResults.Created(result.Id.ToString(),result);
            if (IsPut(method)) return TypedResults.Accepted(result.Id.ToString(),result);
            if (IsPatch(method)) return TypedResults.Accepted(result.Id.ToString(), result);
            if (IsDelete(method)) return TypedResults.Accepted(result.Id.ToString(), result);
            return TypedResults.Ok(result);
        }
        else
        {
            var rtn = new Dictionary<string, string[]>
            {
                ["_errors"] = result.Errors.ToArray()
            };
            return TypedResults.ValidationProblem(rtn);
        }

    }


    private static bool IsPost(string method) => !string.IsNullOrEmpty(method) && method.ToLower() == "post";
    private static bool IsPut(string method) => !string.IsNullOrEmpty(method) && method.ToLower() == "put";
    private static bool IsPatch(string method) => !string.IsNullOrEmpty(method) && method.ToLower() == "patch";
    private static bool IsDelete(string method) => !string.IsNullOrEmpty(method) && method.ToLower() == "delete";
}
