
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

/*
 * Wade Muncy -
 * 4/16/2021
 * Written for use in the ASP.Net Core 2.1 framework, in AWS Lambda.
 * Has relatively good efficiency. Portions might resemble code referenced in old implimentaitons.
 * See Dynamo and GameData folders for the old revisions. Both GameData and DynamoDB are modified code from tutorials by Daniel Donbavand that proved to not work. 
 * Final code in this controller was entirely written by myself, with no working examples able to be written outside it.
 * Portions of the appsettings and LamdaEntryPoint use settings designed in videos published to the public. 
 * Alternative solutions proved to be impossible to impliment. AWS Lambda documention did not assist in resolving this problem.
 * Lack of C# and ASP.Net 2.1 was assumed to be the culprit but could not be proven.
 * 
*/ 


namespace GizmoCheckersLambda.Controllers
{
    [Route("game")]
    public class MainController : ControllerBase
    {

        public MainController()
        {
        }
        [HttpGet("data/{ID}")]
        public async Task<IActionResult> GetStatus([FromRoute] string ID)
        {
            var item = await GetGameStatus(ID);
            return Ok(item["BoardStatus"].S);
        }

        [HttpPost("data/{ID}/{Data}")]
        public async Task<IActionResult> PutStatus([FromRoute] string ID, [FromRoute] string Data)
        {

            await UpdateGame(ID, Data);
            return Ok();
        }

        [HttpGet("newGame")]

        public async Task<IActionResult> CreateGameAsync()
        {
            var randS = new Random();
            int randomNumber = randS.Next();
            await NewGame(randomNumber, "112114116118121123125127132134136138287285283281272274276278267265263261");
            return Ok(randomNumber.ToString());
        }

        [HttpGet("findGame")]
        public async Task<IActionResult> FindGameAsync()
        {
            var items = await GetNewGame();
            Dictionary<string, AttributeValue> item = items[0];
            return Ok(item["BoardID"].N);
        }


        [HttpDelete("deleteGame/{ID}")]
        public async Task<IActionResult> DeleteGameAsync([FromRoute] string ID)
        {
            await DeleteGame(ID);
            return Ok();
                
        }

        public async Task NewGame(int ID, string GameData)
        {
            using (IAmazonDynamoDB DBClient = new AmazonDynamoDBClient())
            {
                await DBClient.PutItemAsync(new PutItemRequest
                {

                    TableName = "CheckersTable",
                    Item = new Dictionary<string, AttributeValue>
            {
                { "BoardStatus",new AttributeValue { S = GameData} },
                { "BoardID", new AttributeValue { N = ID.ToString() } }
            }
                });
            }
        }

        public async Task<Dictionary<string, AttributeValue>> GetGameStatus(string ID)
        {

            using (IAmazonDynamoDB DBClient = new AmazonDynamoDBClient())
            {
                Dictionary<string, AttributeValue> item =
                    (await DBClient.GetItemAsync(new GetItemRequest
                    {
                        TableName = "CheckersTable",
                        ConsistentRead = true,
                        Key = new Dictionary<string, AttributeValue>
                        {
                            {"BoardID", new AttributeValue{ N = ID} }
                        }
                    })).Item;
                return item;
            }


        }
        // Portions of the UpdateGame are referenced from AWS tutorial videos (C# dynamoDB tutorials)
        public async Task UpdateGame(string ID, string GameData)
        {
            using (IAmazonDynamoDB DBClient = new AmazonDynamoDBClient())
            {
                await DBClient.UpdateItemAsync(new UpdateItemRequest
                {

                    TableName = "CheckersTable",
                    Key = new Dictionary<string, AttributeValue>
            {
                { "BoardID", new AttributeValue { N = ID.ToString() } }
            },
                    ExpressionAttributeNames = new Dictionary<string, string>
                    {
                        {"#B","BoardStatus" }
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        {
                            ":newboard", new AttributeValue
                            {
                                S = GameData
                            }
                        }
                    },
                    UpdateExpression = "SET #B = :newboard"
                });
            }
        }

        public async Task DeleteGame(string ID)
        {
            using (IAmazonDynamoDB DBClient = new AmazonDynamoDBClient())
            {
                await DBClient.DeleteItemAsync(new DeleteItemRequest
                {


                    TableName = "CheckersTable",
                    Key = new Dictionary<string, AttributeValue>
            {
                { "BoardID", new AttributeValue { N = ID.ToString() } }

                    }
                });
            }
        }
        public async Task<List<Dictionary<string, AttributeValue>>> GetNewGame()
        {

            using (IAmazonDynamoDB DBClient = new AmazonDynamoDBClient())
            {
                List<Dictionary<string, AttributeValue>> item =
                    (await DBClient.QueryAsync(new QueryRequest
                    {
                        TableName = "CheckersTable",
                        ConsistentRead = true

                    })).Items;
                return item;
            }


        }

    }
}
