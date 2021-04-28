/*using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

// Mostly Referenced from https://www.youtube.com/watch?v=FNUBBsmV6Co. 
// Code below almost entirely written by Daniel Donbavand.
// Portions written differently are changes made by me.
// Was the only reference found on C# coding with AWS DynamoDB.
// 
// DynamoDB.cs and IDynamoDB.cs are both included in this. 


namespace GizmoCheckersLambda.Dynamo
{
    public class DynamoDB : IDynamoDB
    {
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

        public async Task UpdateGame(int ID, string GameData)
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




        /*
        private readonly IAmazonDynamoDB _dynamoClient;
        public async Task NewGame(int ID, string GameData)
        {
            var queryRequest = request(ID, GameData);

            await PutItemAsync(queryRequest);
        }
        /*
        private PutItemRequest request(int ID, String GameData)
        {

            var item = new Dictionary<string, AttributeValue>
            {
                { "BoardStatus",new AttributeValue { S = GameData} },
                { "BoardID", new AttributeValue { N = ID.ToString() } }
            };

            return new PutItemRequest
            {
                TableName = "CheckersTable",
                Item = item
            };
        }

        private async Task PutItemAsync(PutItemRequest request)
        {
            await _dynamoClient.PutItemAsync(request);
        }
        
    }
}
*/