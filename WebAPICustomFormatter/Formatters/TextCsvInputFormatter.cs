using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebAPICustomFormatter.DTOs;

namespace WebAPICustomFormatter.Formatters;

public class TextCsvInputFormatter : TextInputFormatter
{
    public TextCsvInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanReadType(Type type)
    {
        return type == typeof(PersonDTO);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        var request = context.HttpContext.Request;
        using (var reader = new StreamReader(request.Body, encoding))
        {
            var content = await reader.ReadToEndAsync();
            var values = content.Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var personDto = new PersonDTO
            {
                Fullname = $"{values[0]} {values[1]}",
                SeriaNo = values[2],
                Age = int.Parse(values[3]),
                Score = int.Parse(values[4])
            };

            return await InputFormatterResult.SuccessAsync(personDto);
        }
    }
}
