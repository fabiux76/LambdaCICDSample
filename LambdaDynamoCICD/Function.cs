using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaDynamoCICD
{
    public class Functions
    {
        public static AmazonDynamoDBClient client;

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
            try { client = new AmazonDynamoDBClient(); }
            catch (Exception ex)
            {
                Console.WriteLine("FAILED to create a DynamoDB client; " + ex.Message);
            }
        }


        /// <summary>
        /// A Lambda function to respond to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The list of blogs</returns>
        public async Task<APIGatewayProxyResponse> Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var studentId = request.QueryStringParameters["studentid"];
            context.Logger.Log($"Get Request for studentId : {studentId}");
            Primitive hash = new Primitive(studentId, true);

            Table students = Table.LoadTable(client, "Students");
            var student = await students.GetItemAsync(hash);

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = student.ToJson(),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };

            return response;
        }
    }
}
