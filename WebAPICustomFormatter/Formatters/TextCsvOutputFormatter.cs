using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebAPICustomFormatter.DTOs;

namespace WebAPICustomFormatter.Formatters;

public class TextCsvOutputFormatter : TextOutputFormatter
{
    public TextCsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanWriteType(Type type)
    {
        return type == typeof(PersonDTO);
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;
        var person = (PersonDTO)context.Object;

        var output = $"{person.Id}-{person.Fullname}-{person.SeriaNo}-{person.Age}-{person.Score}";
        await response.WriteAsync(output, selectedEncoding);
    }
}
