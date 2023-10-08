using System.Text;

namespace jwtStore.core.Context.SharedContext.Extensions;


public static class StringExtension
{


    public static string ToBase64(this string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

}