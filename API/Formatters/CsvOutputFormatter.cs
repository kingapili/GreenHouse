using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace API.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        private static char _delimiter;
        public CsvOutputFormatter(char delimiter = ',')
        {
            _delimiter = delimiter;
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }


        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var buffer = new StringBuilder();
            
            var response = context.HttpContext.Response;

            if (context.Object is IEnumerable<object> listOfObjects)
            {
                FormatCsvHeaders(buffer, listOfObjects.GetType().GetGenericArguments().Single());
                foreach (var model in listOfObjects)
                {
                    FormatCsvRow(buffer, model);
                }
            }
            else
            {
                FormatCsvHeaders(buffer, context.Object.GetType());
                FormatCsvRow(buffer, context.Object);
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }
        
        public override void WriteResponseHeaders(OutputFormatterWriteContext context)
        {
            context.HttpContext.Response.ContentType = "text/csv";
        }
        
        private static void FormatCsvRow(
            StringBuilder buffer, object classObject)
        {
            List<String> classPropertyValues = new List<string>();
            foreach(var prop in classObject.GetType().GetProperties()) {
                classPropertyValues.Add($"\"{prop.GetValue(classObject, null)}\"");
            }

            buffer.AppendJoin(_delimiter, classPropertyValues);
            buffer.AppendLine();
        }
        
        private static void FormatCsvHeaders(
            StringBuilder buffer, Type type)
        {
            List<String> classProperties = new List<string>();
            foreach(var prop in type.GetProperties()) {
                classProperties.Add($"\"{prop.Name}\"");
            }
            buffer.AppendJoin(_delimiter, classProperties);
            buffer.AppendLine();
        }
        
    }
}